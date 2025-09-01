using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020006AB RID: 1707
	internal class RequestStream : Stream
	{
		// Token: 0x06003689 RID: 13961 RVA: 0x000BF7F3 File Offset: 0x000BD9F3
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length) : this(stream, buffer, offset, length, -1L)
		{
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000BF802 File Offset: 0x000BDA02
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length, long contentlength)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.offset = offset;
			this.length = length;
			this.remaining_body = contentlength;
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06003690 RID: 13968 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x06003691 RID: 13969 RVA: 0x000BF82F File Offset: 0x000BDA2F
		public override void Close()
		{
			this.disposed = true;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000BF838 File Offset: 0x000BDA38
		private int FillFromBuffer(byte[] buffer, int off, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			int num = buffer.Length;
			if (off > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (off > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.remaining_body == 0L)
			{
				return -1;
			}
			if (this.length == 0)
			{
				return 0;
			}
			int num2 = Math.Min(this.length, count);
			if (this.remaining_body > 0L)
			{
				num2 = (int)Math.Min((long)num2, this.remaining_body);
			}
			if (this.offset > this.buffer.Length - num2)
			{
				num2 = Math.Min(num2, this.buffer.Length - this.offset);
			}
			if (num2 == 0)
			{
				return 0;
			}
			Buffer.BlockCopy(this.buffer, this.offset, buffer, off, num2);
			this.offset += num2;
			this.length -= num2;
			if (this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num2;
			}
			return num2;
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000BF950 File Offset: 0x000BDB50
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num == -1)
			{
				return 0;
			}
			if (num > 0)
			{
				return num;
			}
			num = this.stream.Read(buffer, offset, count);
			if (num > 0 && this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000BF9C0 File Offset: 0x000BDBC0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num > 0 || num == -1)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.Callback = cback;
				httpStreamAsyncResult.State = state;
				httpStreamAsyncResult.SynchRead = Math.Max(0, num);
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (this.remaining_body >= 0L && (long)count > this.remaining_body)
			{
				count = (int)Math.Min(2147483647L, this.remaining_body);
			}
			return this.stream.BeginRead(buffer, offset, count, cback, state);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000BFA74 File Offset: 0x000BDC74
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			if (ares == null)
			{
				throw new ArgumentNullException("async_result");
			}
			if (ares is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)ares;
				if (!ares.IsCompleted)
				{
					ares.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SynchRead;
			}
			int num = this.stream.EndRead(ares);
			if (this.remaining_body > 0L && num > 0)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000044FA File Offset: 0x000026FA
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000044FA File Offset: 0x000026FA
		public override void EndWrite(IAsyncResult async_result)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001FD7 RID: 8151
		private byte[] buffer;

		// Token: 0x04001FD8 RID: 8152
		private int offset;

		// Token: 0x04001FD9 RID: 8153
		private int length;

		// Token: 0x04001FDA RID: 8154
		private long remaining_body;

		// Token: 0x04001FDB RID: 8155
		private bool disposed;

		// Token: 0x04001FDC RID: 8156
		private Stream stream;
	}
}
