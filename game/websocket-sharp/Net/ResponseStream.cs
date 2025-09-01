using System;
using System.IO;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x02000029 RID: 41
	internal class ResponseStream : Stream
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00014C87 File Offset: 0x00012E87
		static ResponseStream()
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00014CC0 File Offset: 0x00012EC0
		internal ResponseStream(Stream innerStream, HttpListenerResponse response, bool ignoreWriteExceptions)
		{
			this._innerStream = innerStream;
			this._response = response;
			if (ignoreWriteExceptions)
			{
				this._write = new Action<byte[], int, int>(this.writeWithoutThrowingException);
				this._writeChunked = new Action<byte[], int, int>(this.writeChunkedWithoutThrowingException);
			}
			else
			{
				this._write = new Action<byte[], int, int>(innerStream.Write);
				this._writeChunked = new Action<byte[], int, int>(this.writeChunked);
			}
			this._bodyBuffer = new MemoryStream();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00014D44 File Offset: 0x00012F44
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00014D58 File Offset: 0x00012F58
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00014D6C File Offset: 0x00012F6C
		public override bool CanWrite
		{
			get
			{
				return !this._disposed;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F39A File Offset: 0x0000D59A
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014D88 File Offset: 0x00012F88
		private bool flush(bool closing)
		{
			bool flag = !this._response.HeadersSent;
			if (flag)
			{
				bool flag2 = !this.flushHeaders();
				if (flag2)
				{
					return false;
				}
				this._response.HeadersSent = true;
				this._sendChunked = this._response.SendChunked;
				this._writeBody = (this._sendChunked ? this._writeChunked : this._write);
			}
			this.flushBody(closing);
			return true;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014E04 File Offset: 0x00013004
		private void flushBody(bool closing)
		{
			using (this._bodyBuffer)
			{
				long length = this._bodyBuffer.Length;
				bool flag = length > 2147483647L;
				if (flag)
				{
					this._bodyBuffer.Position = 0L;
					int num = 1024;
					byte[] array = new byte[num];
					for (;;)
					{
						int num2 = this._bodyBuffer.Read(array, 0, num);
						bool flag2 = num2 <= 0;
						if (flag2)
						{
							break;
						}
						this._writeBody(array, 0, num2);
					}
				}
				else
				{
					bool flag3 = length > 0L;
					if (flag3)
					{
						this._writeBody(this._bodyBuffer.GetBuffer(), 0, (int)length);
					}
				}
			}
			bool flag4 = !closing;
			if (flag4)
			{
				this._bodyBuffer = new MemoryStream();
			}
			else
			{
				bool sendChunked = this._sendChunked;
				if (sendChunked)
				{
					this._write(ResponseStream._lastChunk, 0, 5);
				}
				this._bodyBuffer = null;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00014F14 File Offset: 0x00013114
		private bool flushHeaders()
		{
			bool flag = !this._response.SendChunked;
			if (flag)
			{
				bool flag2 = this._response.ContentLength64 != this._bodyBuffer.Length;
				if (flag2)
				{
					return false;
				}
			}
			string statusLine = this._response.StatusLine;
			WebHeaderCollection fullHeaders = this._response.FullHeaders;
			MemoryStream memoryStream = new MemoryStream();
			Encoding utf = Encoding.UTF8;
			using (StreamWriter streamWriter = new StreamWriter(memoryStream, utf, 256))
			{
				streamWriter.Write(statusLine);
				streamWriter.Write(fullHeaders.ToStringMultiValue(true));
				streamWriter.Flush();
				int num = utf.GetPreamble().Length;
				long num2 = memoryStream.Length - (long)num;
				bool flag3 = num2 > (long)ResponseStream._maxHeadersLength;
				if (flag3)
				{
					return false;
				}
				this._write(memoryStream.GetBuffer(), num, (int)num2);
			}
			this._response.CloseConnection = (fullHeaders["Connection"] == "close");
			return true;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00015040 File Offset: 0x00013240
		private static byte[] getChunkSizeBytes(int size)
		{
			string s = string.Format("{0:x}\r\n", size);
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00015070 File Offset: 0x00013270
		private void writeChunked(byte[] buffer, int offset, int count)
		{
			byte[] chunkSizeBytes = ResponseStream.getChunkSizeBytes(count);
			this._innerStream.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
			this._innerStream.Write(buffer, offset, count);
			this._innerStream.Write(ResponseStream._crlf, 0, 2);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000150B8 File Offset: 0x000132B8
		private void writeChunkedWithoutThrowingException(byte[] buffer, int offset, int count)
		{
			try
			{
				this.writeChunked(buffer, offset, count);
			}
			catch
			{
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000150EC File Offset: 0x000132EC
		private void writeWithoutThrowingException(byte[] buffer, int offset, int count)
		{
			try
			{
				this._innerStream.Write(buffer, offset, count);
			}
			catch
			{
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00015124 File Offset: 0x00013324
		internal void Close(bool force)
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				this._disposed = true;
				bool flag = !force;
				if (flag)
				{
					bool flag2 = this.flush(true);
					if (flag2)
					{
						this._response.Close();
						this._response = null;
						this._innerStream = null;
						return;
					}
					this._response.CloseConnection = true;
				}
				bool sendChunked = this._sendChunked;
				if (sendChunked)
				{
					this._write(ResponseStream._lastChunk, 0, 5);
				}
				this._bodyBuffer.Dispose();
				this._response.Abort();
				this._bodyBuffer = null;
				this._response = null;
				this._innerStream = null;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000151D1 File Offset: 0x000133D1
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			this._write(buffer, offset, count);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000151E4 File Offset: 0x000133E4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			return this._bodyBuffer.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00015226 File Offset: 0x00013426
		public override void Close()
		{
			this.Close(false);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00015231 File Offset: 0x00013431
		protected override void Dispose(bool disposing)
		{
			this.Close(!disposing);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00015240 File Offset: 0x00013440
		public override void EndWrite(IAsyncResult asyncResult)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			this._bodyBuffer.EndWrite(asyncResult);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001527C File Offset: 0x0001347C
		public override void Flush()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				bool flag = this._sendChunked || this._response.SendChunked;
				bool flag2 = !flag;
				if (!flag2)
				{
					this.flush(false);
				}
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000152C0 File Offset: 0x000134C0
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			this._bodyBuffer.Write(buffer, offset, count);
		}

		// Token: 0x0400012E RID: 302
		private MemoryStream _bodyBuffer;

		// Token: 0x0400012F RID: 303
		private static readonly byte[] _crlf = new byte[]
		{
			13,
			10
		};

		// Token: 0x04000130 RID: 304
		private bool _disposed;

		// Token: 0x04000131 RID: 305
		private Stream _innerStream;

		// Token: 0x04000132 RID: 306
		private static readonly byte[] _lastChunk = new byte[]
		{
			48,
			13,
			10,
			13,
			10
		};

		// Token: 0x04000133 RID: 307
		private static readonly int _maxHeadersLength = 32768;

		// Token: 0x04000134 RID: 308
		private HttpListenerResponse _response;

		// Token: 0x04000135 RID: 309
		private bool _sendChunked;

		// Token: 0x04000136 RID: 310
		private Action<byte[], int, int> _write;

		// Token: 0x04000137 RID: 311
		private Action<byte[], int, int> _writeBody;

		// Token: 0x04000138 RID: 312
		private Action<byte[], int, int> _writeChunked;
	}
}
