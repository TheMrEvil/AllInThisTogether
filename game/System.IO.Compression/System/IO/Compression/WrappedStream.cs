using System;

namespace System.IO.Compression
{
	// Token: 0x02000037 RID: 55
	internal sealed class WrappedStream : Stream
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000091B3 File Offset: 0x000073B3
		internal WrappedStream(Stream baseStream, bool closeBaseStream) : this(baseStream, closeBaseStream, null, null)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000091BF File Offset: 0x000073BF
		private WrappedStream(Stream baseStream, bool closeBaseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
		{
			this._baseStream = baseStream;
			this._closeBaseStream = closeBaseStream;
			this._onClosed = onClosed;
			this._zipArchiveEntry = entry;
			this._isDisposed = false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000091EB File Offset: 0x000073EB
		internal WrappedStream(Stream baseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed) : this(baseStream, false, entry, onClosed)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000091F7 File Offset: 0x000073F7
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Length;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000920A File Offset: 0x0000740A
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000921D File Offset: 0x0000741D
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfCantSeek();
				this._baseStream.Position = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009237 File Offset: 0x00007437
		public override bool CanRead
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanRead;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000924E File Offset: 0x0000744E
		public override bool CanSeek
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanSeek;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009265 File Offset: 0x00007465
		public override bool CanWrite
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanWrite;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000927C File Offset: 0x0000747C
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000929C File Offset: 0x0000749C
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000092B1 File Offset: 0x000074B1
		private void ThrowIfCantWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000092C6 File Offset: 0x000074C6
		private void ThrowIfCantSeek()
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000092DB File Offset: 0x000074DB
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000092F7 File Offset: 0x000074F7
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			return this._baseStream.Seek(offset, origin);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009312 File Offset: 0x00007512
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			this.ThrowIfCantWrite();
			this._baseStream.SetLength(value);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009332 File Offset: 0x00007532
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000934E File Offset: 0x0000754E
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Flush();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009368 File Offset: 0x00007568
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				Action<ZipArchiveEntry> onClosed = this._onClosed;
				if (onClosed != null)
				{
					onClosed(this._zipArchiveEntry);
				}
				if (this._closeBaseStream)
				{
					this._baseStream.Dispose();
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001EF RID: 495
		private readonly Stream _baseStream;

		// Token: 0x040001F0 RID: 496
		private readonly bool _closeBaseStream;

		// Token: 0x040001F1 RID: 497
		private readonly Action<ZipArchiveEntry> _onClosed;

		// Token: 0x040001F2 RID: 498
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x040001F3 RID: 499
		private bool _isDisposed;
	}
}
