using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x020006AC RID: 1708
	internal class ResponseStream : Stream
	{
		// Token: 0x0600369C RID: 13980 RVA: 0x000BFAFF File Offset: 0x000BDCFF
		internal ResponseStream(Stream stream, HttpListenerResponse response, bool ignore_errors)
		{
			this.response = response;
			this.ignore_errors = ignore_errors;
			this.stream = stream;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x060036A2 RID: 13986 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x060036A3 RID: 13987 RVA: 0x000BFB1C File Offset: 0x000BDD1C
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryStream headers = this.GetHeaders(true);
				bool sendChunked = this.response.SendChunked;
				if (this.stream.CanWrite)
				{
					try
					{
						if (headers != null)
						{
							long position = headers.Position;
							if (sendChunked && !this.trailer_sent)
							{
								byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(0, true);
								headers.Position = headers.Length;
								headers.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
							}
							this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
							this.trailer_sent = true;
						}
						else if (sendChunked && !this.trailer_sent)
						{
							byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(0, true);
							this.InternalWrite(chunkSizeBytes, 0, chunkSizeBytes.Length);
							this.trailer_sent = true;
						}
					}
					catch (IOException)
					{
					}
				}
				this.response.Close();
			}
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000BFBF8 File Offset: 0x000BDDF8
		private MemoryStream GetHeaders(bool closing)
		{
			object headers_lock = this.response.headers_lock;
			MemoryStream result;
			lock (headers_lock)
			{
				if (this.response.HeadersSent)
				{
					result = null;
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					this.response.SendHeaders(closing, memoryStream);
					result = memoryStream;
				}
			}
			return result;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000BFC60 File Offset: 0x000BDE60
		private static byte[] GetChunkSizeBytes(int size, bool final)
		{
			string s = string.Format("{0:x}\r\n{1}", size, final ? "\r\n" : "");
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000BFC98 File Offset: 0x000BDE98
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			if (this.ignore_errors)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000BFCE0 File Offset: 0x000BDEE0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (count == 0)
			{
				return;
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
				}
				int num = Math.Min(count, 16384 - (int)headers.Position + (int)position);
				headers.Write(buffer, offset, num);
				count -= num;
				offset += num;
				this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
				headers.SetLength(0L);
				headers.Capacity = 0;
			}
			else if (sendChunked)
			{
				byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(chunkSizeBytes, 0, chunkSizeBytes.Length);
			}
			if (count > 0)
			{
				this.InternalWrite(buffer, offset, count);
			}
			if (sendChunked)
			{
				this.InternalWrite(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000BFDD8 File Offset: 0x000BDFD8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
				}
				headers.Write(buffer, offset, count);
				buffer = headers.GetBuffer();
				offset = (int)position;
				count = (int)(headers.Position - position);
			}
			else if (sendChunked)
			{
				byte[] chunkSizeBytes = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(chunkSizeBytes, 0, chunkSizeBytes.Length);
			}
			return this.stream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000BFE8C File Offset: 0x000BE08C
		public override void EndWrite(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.ignore_errors)
			{
				try
				{
					this.stream.EndWrite(ares);
					if (this.response.SendChunked)
					{
						this.stream.Write(ResponseStream.crlf, 0, 2);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.EndWrite(ares);
			if (this.response.SendChunked)
			{
				this.stream.Write(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000044FA File Offset: 0x000026FA
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000044FA File Offset: 0x000026FA
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000044FA File Offset: 0x000026FA
		public override int EndRead(IAsyncResult ares)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000BFF28 File Offset: 0x000BE128
		// Note: this type is marked as 'beforefieldinit'.
		static ResponseStream()
		{
		}

		// Token: 0x04001FDD RID: 8157
		private HttpListenerResponse response;

		// Token: 0x04001FDE RID: 8158
		private bool ignore_errors;

		// Token: 0x04001FDF RID: 8159
		private bool disposed;

		// Token: 0x04001FE0 RID: 8160
		private bool trailer_sent;

		// Token: 0x04001FE1 RID: 8161
		private Stream stream;

		// Token: 0x04001FE2 RID: 8162
		private static byte[] crlf = new byte[]
		{
			13,
			10
		};
	}
}
