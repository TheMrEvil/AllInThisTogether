using System;

namespace System.IO.Compression
{
	// Token: 0x02000038 RID: 56
	internal sealed class SubReadStream : Stream
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000093B8 File Offset: 0x000075B8
		public SubReadStream(Stream superStream, long startPosition, long maxLength)
		{
			this._startInSuperStream = startPosition;
			this._positionInSuperStream = startPosition;
			this._endInSuperStream = startPosition + maxLength;
			this._superStream = superStream;
			this._canRead = true;
			this._isDisposed = false;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000093EC File Offset: 0x000075EC
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._endInSuperStream - this._startInSuperStream;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009401 File Offset: 0x00007601
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009416 File Offset: 0x00007616
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._positionInSuperStream - this._startInSuperStream;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009428 File Offset: 0x00007628
		public override bool CanRead
		{
			get
			{
				return this._superStream.CanRead && this._canRead;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000943F File Offset: 0x0000763F
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000929C File Offset: 0x0000749C
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009460 File Offset: 0x00007660
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			if (this._superStream.Position != this._positionInSuperStream)
			{
				this._superStream.Seek(this._positionInSuperStream, SeekOrigin.Begin);
			}
			if (this._positionInSuperStream + (long)count > this._endInSuperStream)
			{
				count = (int)(this._endInSuperStream - this._positionInSuperStream);
			}
			int num = this._superStream.Read(buffer, offset, count);
			this._positionInSuperStream += (long)num;
			return num;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009416 File Offset: 0x00007616
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000094DF File Offset: 0x000076DF
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000094F1 File Offset: 0x000076F1
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000094F1 File Offset: 0x000076F1
		public override void Flush()
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009503 File Offset: 0x00007703
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._canRead = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001F4 RID: 500
		private readonly long _startInSuperStream;

		// Token: 0x040001F5 RID: 501
		private long _positionInSuperStream;

		// Token: 0x040001F6 RID: 502
		private readonly long _endInSuperStream;

		// Token: 0x040001F7 RID: 503
		private readonly Stream _superStream;

		// Token: 0x040001F8 RID: 504
		private bool _canRead;

		// Token: 0x040001F9 RID: 505
		private bool _isDisposed;
	}
}
