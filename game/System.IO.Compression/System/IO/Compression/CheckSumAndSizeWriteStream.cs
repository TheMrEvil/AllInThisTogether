using System;

namespace System.IO.Compression
{
	// Token: 0x02000039 RID: 57
	internal sealed class CheckSumAndSizeWriteStream : Stream
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00009528 File Offset: 0x00007728
		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, ZipArchiveEntry entry, EventHandler onClose, Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes)
		{
			this._baseStream = baseStream;
			this._baseBaseStream = baseBaseStream;
			this._position = 0L;
			this._checksum = 0U;
			this._leaveOpenOnClose = leaveOpenOnClose;
			this._canWrite = true;
			this._isDisposed = false;
			this._initialPosition = 0L;
			this._zipArchiveEntry = entry;
			this._onClose = onClose;
			this._saveCrcAndSizes = saveCrcAndSizes;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000958D File Offset: 0x0000778D
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000959F File Offset: 0x0000779F
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000958D File Offset: 0x0000778D
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._position;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00002289 File Offset: 0x00000489
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000095AD File Offset: 0x000077AD
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000095B5 File Offset: 0x000077B5
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000095D5 File Offset: 0x000077D5
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000958D File Offset: 0x0000778D
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000095E7 File Offset: 0x000077E7
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000095FC File Offset: 0x000077FC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return;
			}
			if (!this._everWritten)
			{
				this._initialPosition = this._baseBaseStream.Position;
				this._everWritten = true;
			}
			this._checksum = Crc32Helper.UpdateCrc32(this._checksum, buffer, offset, count);
			this._baseStream.Write(buffer, offset, count);
			this._position += (long)count;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000096AD File Offset: 0x000078AD
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._baseStream.Flush();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000096C0 File Offset: 0x000078C0
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (!this._everWritten)
				{
					this._initialPosition = this._baseBaseStream.Position;
				}
				if (!this._leaveOpenOnClose)
				{
					this._baseStream.Dispose();
				}
				Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes = this._saveCrcAndSizes;
				if (saveCrcAndSizes != null)
				{
					saveCrcAndSizes(this._initialPosition, this.Position, this._checksum, this._baseBaseStream, this._zipArchiveEntry, this._onClose);
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001FA RID: 506
		private readonly Stream _baseStream;

		// Token: 0x040001FB RID: 507
		private readonly Stream _baseBaseStream;

		// Token: 0x040001FC RID: 508
		private long _position;

		// Token: 0x040001FD RID: 509
		private uint _checksum;

		// Token: 0x040001FE RID: 510
		private readonly bool _leaveOpenOnClose;

		// Token: 0x040001FF RID: 511
		private bool _canWrite;

		// Token: 0x04000200 RID: 512
		private bool _isDisposed;

		// Token: 0x04000201 RID: 513
		private bool _everWritten;

		// Token: 0x04000202 RID: 514
		private long _initialPosition;

		// Token: 0x04000203 RID: 515
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x04000204 RID: 516
		private readonly EventHandler _onClose;

		// Token: 0x04000205 RID: 517
		private readonly Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> _saveCrcAndSizes;
	}
}
