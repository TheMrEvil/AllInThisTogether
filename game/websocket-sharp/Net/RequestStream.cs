using System;
using System.IO;

namespace WebSocketSharp.Net
{
	// Token: 0x02000028 RID: 40
	internal class RequestStream : Stream
	{
		// Token: 0x06000317 RID: 791 RVA: 0x000147AE File Offset: 0x000129AE
		internal RequestStream(Stream innerStream, byte[] initialBuffer, int offset, int count, long contentLength)
		{
			this._innerStream = innerStream;
			this._initialBuffer = initialBuffer;
			this._offset = offset;
			this._count = count;
			this._bodyLeft = contentLength;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000147E0 File Offset: 0x000129E0
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000147F8 File Offset: 0x000129F8
		internal byte[] InitialBuffer
		{
			get
			{
				return this._initialBuffer;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00014810 File Offset: 0x00012A10
		internal int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00014828 File Offset: 0x00012A28
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001483C File Offset: 0x00012A3C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00014850 File Offset: 0x00012A50
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000F39A File Offset: 0x0000D59A
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000F39A File Offset: 0x0000D59A
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

		// Token: 0x06000321 RID: 801 RVA: 0x00014864 File Offset: 0x00012A64
		private int fillFromInitialBuffer(byte[] buffer, int offset, int count)
		{
			bool flag = this._bodyLeft == 0L;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = this._count == 0;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = count > this._count;
					if (flag3)
					{
						count = this._count;
					}
					bool flag4 = this._bodyLeft > 0L && this._bodyLeft < (long)count;
					if (flag4)
					{
						count = (int)this._bodyLeft;
					}
					Buffer.BlockCopy(this._initialBuffer, this._offset, buffer, offset, count);
					this._offset += count;
					this._count -= count;
					bool flag5 = this._bodyLeft > 0L;
					if (flag5)
					{
						this._bodyLeft -= (long)count;
					}
					result = count;
				}
			}
			return result;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001492C File Offset: 0x00012B2C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				string message = "A negative value.";
				throw new ArgumentOutOfRangeException("offset", message);
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				string message2 = "A negative value.";
				throw new ArgumentOutOfRangeException("count", message2);
			}
			int num = buffer.Length;
			bool flag4 = offset + count > num;
			if (flag4)
			{
				string message3 = "The sum of 'offset' and 'count' is greater than the length of 'buffer'.";
				throw new ArgumentException(message3);
			}
			bool flag5 = count == 0;
			IAsyncResult result;
			if (flag5)
			{
				result = this._innerStream.BeginRead(buffer, offset, 0, callback, state);
			}
			else
			{
				int num2 = this.fillFromInitialBuffer(buffer, offset, count);
				bool flag6 = num2 != 0;
				if (flag6)
				{
					HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
					httpStreamAsyncResult.Buffer = buffer;
					httpStreamAsyncResult.Offset = offset;
					httpStreamAsyncResult.Count = count;
					httpStreamAsyncResult.SyncRead = ((num2 > 0) ? num2 : 0);
					httpStreamAsyncResult.Complete();
					result = httpStreamAsyncResult;
				}
				else
				{
					bool flag7 = this._bodyLeft > 0L && this._bodyLeft < (long)count;
					if (flag7)
					{
						count = (int)this._bodyLeft;
					}
					result = this._innerStream.BeginRead(buffer, offset, count, callback, state);
				}
			}
			return result;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00014A85 File Offset: 0x00012C85
		public override void Close()
		{
			this._disposed = true;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00014A90 File Offset: 0x00012C90
		public override int EndRead(IAsyncResult asyncResult)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = asyncResult == null;
			if (flag)
			{
				throw new ArgumentNullException("asyncResult");
			}
			bool flag2 = asyncResult is HttpStreamAsyncResult;
			int result;
			if (flag2)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)asyncResult;
				bool flag3 = !httpStreamAsyncResult.IsCompleted;
				if (flag3)
				{
					httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
				}
				result = httpStreamAsyncResult.SyncRead;
			}
			else
			{
				int num = this._innerStream.EndRead(asyncResult);
				bool flag4 = num > 0 && this._bodyLeft > 0L;
				if (flag4)
				{
					this._bodyLeft -= (long)num;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00014B49 File Offset: 0x00012D49
		public override void Flush()
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00014B4C File Offset: 0x00012D4C
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0;
			if (flag2)
			{
				string message = "A negative value.";
				throw new ArgumentOutOfRangeException("offset", message);
			}
			bool flag3 = count < 0;
			if (flag3)
			{
				string message2 = "A negative value.";
				throw new ArgumentOutOfRangeException("count", message2);
			}
			int num = buffer.Length;
			bool flag4 = offset + count > num;
			if (flag4)
			{
				string message3 = "The sum of 'offset' and 'count' is greater than the length of 'buffer'.";
				throw new ArgumentException(message3);
			}
			bool flag5 = count == 0;
			int result;
			if (flag5)
			{
				result = 0;
			}
			else
			{
				int num2 = this.fillFromInitialBuffer(buffer, offset, count);
				bool flag6 = num2 == -1;
				if (flag6)
				{
					result = 0;
				}
				else
				{
					bool flag7 = num2 > 0;
					if (flag7)
					{
						result = num2;
					}
					else
					{
						bool flag8 = this._bodyLeft > 0L && this._bodyLeft < (long)count;
						if (flag8)
						{
							count = (int)this._bodyLeft;
						}
						num2 = this._innerStream.Read(buffer, offset, count);
						bool flag9 = num2 > 0 && this._bodyLeft > 0L;
						if (flag9)
						{
							this._bodyLeft -= (long)num2;
						}
						result = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F39A File Offset: 0x0000D59A
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000128 RID: 296
		private long _bodyLeft;

		// Token: 0x04000129 RID: 297
		private int _count;

		// Token: 0x0400012A RID: 298
		private bool _disposed;

		// Token: 0x0400012B RID: 299
		private byte[] _initialBuffer;

		// Token: 0x0400012C RID: 300
		private Stream _innerStream;

		// Token: 0x0400012D RID: 301
		private int _offset;
	}
}
