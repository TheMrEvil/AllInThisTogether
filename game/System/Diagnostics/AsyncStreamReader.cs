using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200023B RID: 571
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x0004B590 File Offset: 0x00049790
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding) : this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004B5A2 File Offset: 0x000497A2
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004B5D0 File Offset: 0x000497D0
		private void Init(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.process = process;
			this.stream = stream;
			this.encoding = encoding;
			this.userCallBack = callback;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.cancelOperation = false;
			this.eofEvent = new ManualResetEvent(false);
			this.sb = null;
			this.bLastCarriageReturn = false;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004B665 File Offset: 0x00049865
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004B66E File Offset: 0x0004986E
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0004B680 File Offset: 0x00049880
		protected virtual void Dispose(bool disposing)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (disposing && this.stream != null)
				{
					if (this.asyncReadResult != null && !this.asyncReadResult.IsCompleted && this.stream is FileStream)
					{
						SafeHandle safeFileHandle = ((FileStream)this.stream).SafeFileHandle;
						MonoIOError monoIOError;
						while (!this.asyncReadResult.IsCompleted && (MonoIO.Cancel(safeFileHandle, out monoIOError) || monoIOError != MonoIOError.ERROR_NOT_SUPPORTED))
						{
							this.asyncReadResult.AsyncWaitHandle.WaitOne(200);
						}
					}
					this.stream.Close();
				}
				if (this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
				}
				if (this.eofEvent != null)
				{
					this.eofEvent.Close();
					this.eofEvent = null;
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004B77C File Offset: 0x0004997C
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004B784 File Offset: 0x00049984
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004B78C File Offset: 0x0004998C
		internal void BeginReadLine()
		{
			if (this.cancelOperation)
			{
				this.cancelOperation = false;
			}
			if (this.sb == null)
			{
				this.sb = new StringBuilder(1024);
				this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				return;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004B7F4 File Offset: 0x000499F4
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004B800 File Offset: 0x00049A00
		private void ReadBuffer(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = this.syncObject;
				lock (obj)
				{
					this.asyncReadResult = null;
					if (this.stream == null)
					{
						num = 0;
					}
					else
					{
						num = this.stream.EndRead(ar);
					}
				}
			}
			catch (IOException)
			{
				num = 0;
			}
			catch (OperationCanceledException)
			{
				num = 0;
			}
			for (;;)
			{
				object obj;
				if (num == 0)
				{
					Queue obj2 = this.messageQueue;
					lock (obj2)
					{
						if (this.sb.Length != 0)
						{
							this.messageQueue.Enqueue(this.sb.ToString());
							this.sb.Length = 0;
						}
						this.messageQueue.Enqueue(null);
					}
					try
					{
						this.FlushMessageQueue();
						break;
					}
					finally
					{
						obj = this.syncObject;
						lock (obj)
						{
							if (this.eofEvent != null)
							{
								try
								{
									this.eofEvent.Set();
								}
								catch (ObjectDisposedException)
								{
								}
							}
						}
					}
				}
				obj = this.syncObject;
				lock (obj)
				{
					if (this.decoder == null)
					{
						num = 0;
						continue;
					}
					int chars = this.decoder.GetChars(this.byteBuffer, 0, num, this.charBuffer, 0);
					this.sb.Append(this.charBuffer, 0, chars);
				}
				this.GetLinesFromStringBuilder();
				obj = this.syncObject;
				lock (obj)
				{
					if (this.stream == null)
					{
						num = 0;
						continue;
					}
					this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				}
				break;
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004BA24 File Offset: 0x00049C24
		private void GetLinesFromStringBuilder()
		{
			int i = this.currentLinePos;
			int num = 0;
			int length = this.sb.Length;
			if (this.bLastCarriageReturn && length > 0 && this.sb[0] == '\n')
			{
				i = 1;
				num = 1;
				this.bLastCarriageReturn = false;
			}
			while (i < length)
			{
				char c = this.sb[i];
				if (c == '\r' || c == '\n')
				{
					string obj = this.sb.ToString(num, i - num);
					num = i + 1;
					if (c == '\r' && num < length && this.sb[num] == '\n')
					{
						num++;
						i++;
					}
					Queue obj2 = this.messageQueue;
					lock (obj2)
					{
						this.messageQueue.Enqueue(obj);
					}
				}
				i++;
			}
			if (this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				if (num == 0)
				{
					this.currentLinePos = i;
				}
				else
				{
					this.sb.Remove(0, num);
					this.currentLinePos = 0;
				}
			}
			else
			{
				this.sb.Length = 0;
				this.currentLinePos = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004BB6C File Offset: 0x00049D6C
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				Queue obj = this.messageQueue;
				lock (obj)
				{
					if (this.messageQueue.Count > 0)
					{
						string data = (string)this.messageQueue.Dequeue();
						if (!this.cancelOperation)
						{
							this.userCallBack(data);
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004BBE8 File Offset: 0x00049DE8
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x04000A1B RID: 2587
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000A1C RID: 2588
		private const int MinBufferSize = 128;

		// Token: 0x04000A1D RID: 2589
		private Stream stream;

		// Token: 0x04000A1E RID: 2590
		private Encoding encoding;

		// Token: 0x04000A1F RID: 2591
		private Decoder decoder;

		// Token: 0x04000A20 RID: 2592
		private byte[] byteBuffer;

		// Token: 0x04000A21 RID: 2593
		private char[] charBuffer;

		// Token: 0x04000A22 RID: 2594
		private int _maxCharsPerBuffer;

		// Token: 0x04000A23 RID: 2595
		private Process process;

		// Token: 0x04000A24 RID: 2596
		private UserCallBack userCallBack;

		// Token: 0x04000A25 RID: 2597
		private bool cancelOperation;

		// Token: 0x04000A26 RID: 2598
		private ManualResetEvent eofEvent;

		// Token: 0x04000A27 RID: 2599
		private Queue messageQueue;

		// Token: 0x04000A28 RID: 2600
		private StringBuilder sb;

		// Token: 0x04000A29 RID: 2601
		private bool bLastCarriageReturn;

		// Token: 0x04000A2A RID: 2602
		private int currentLinePos;

		// Token: 0x04000A2B RID: 2603
		private object syncObject = new object();

		// Token: 0x04000A2C RID: 2604
		private IAsyncResult asyncReadResult;
	}
}
