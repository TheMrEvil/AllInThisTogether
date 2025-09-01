using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x02000028 RID: 40
	internal sealed class PositionPreservingWriteOnlyStreamWrapper : Stream
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000063BC File Offset: 0x000045BC
		public PositionPreservingWriteOnlyStreamWrapper(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000063CB File Offset: 0x000045CB
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000063CE File Offset: 0x000045CE
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000063D6 File Offset: 0x000045D6
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._position += (long)count;
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000063F5 File Offset: 0x000045F5
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this._position += (long)count;
			return this._stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006418 File Offset: 0x00004618
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006426 File Offset: 0x00004626
		public override void WriteByte(byte value)
		{
			this._position += 1L;
			this._stream.WriteByte(value);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006443 File Offset: 0x00004643
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this._position += (long)count;
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006464 File Offset: 0x00004664
		public override bool CanTimeout
		{
			get
			{
				return this._stream.CanTimeout;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006471 File Offset: 0x00004671
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000647E File Offset: 0x0000467E
		public override int ReadTimeout
		{
			get
			{
				return this._stream.ReadTimeout;
			}
			set
			{
				this._stream.ReadTimeout = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000648C File Offset: 0x0000468C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006499 File Offset: 0x00004699
		public override int WriteTimeout
		{
			get
			{
				return this._stream.WriteTimeout;
			}
			set
			{
				this._stream.WriteTimeout = value;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000064A7 File Offset: 0x000046A7
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000064B4 File Offset: 0x000046B4
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000064C2 File Offset: 0x000046C2
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000064CF File Offset: 0x000046CF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._stream.Dispose();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000036BB File Offset: 0x000018BB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000036BB File Offset: 0x000018BB
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000036BB File Offset: 0x000018BB
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x04000169 RID: 361
		private readonly Stream _stream;

		// Token: 0x0400016A RID: 362
		private long _position;
	}
}
