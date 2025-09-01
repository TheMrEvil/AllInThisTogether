using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000321 RID: 801
	internal sealed class SqlXmlStreamWrapper : Stream
	{
		// Token: 0x0600260D RID: 9741 RVA: 0x000A9A79 File Offset: 0x000A7C79
		internal SqlXmlStreamWrapper(Stream stream)
		{
			this._stream = stream;
			this._lPosition = 0L;
			this._isClosed = false;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000A9A97 File Offset: 0x000A7C97
		public override bool CanRead
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanRead;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000A9AAE File Offset: 0x000A7CAE
		public override bool CanSeek
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanSeek;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000A9AC5 File Offset: 0x000A7CC5
		public override bool CanWrite
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanWrite;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000A9ADC File Offset: 0x000A7CDC
		public override long Length
		{
			get
			{
				this.ThrowIfStreamClosed("get_Length");
				this.ThrowIfStreamCannotSeek("get_Length");
				return this._stream.Length;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000A9AFF File Offset: 0x000A7CFF
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000A9B1D File Offset: 0x000A7D1D
		public override long Position
		{
			get
			{
				this.ThrowIfStreamClosed("get_Position");
				this.ThrowIfStreamCannotSeek("get_Position");
				return this._lPosition;
			}
			set
			{
				this.ThrowIfStreamClosed("set_Position");
				this.ThrowIfStreamCannotSeek("set_Position");
				if (value < 0L || value > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000A9B5C File Offset: 0x000A7D5C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfStreamClosed("Seek");
			this.ThrowIfStreamCannotSeek("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._stream.Length + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000A9C38 File Offset: 0x000A7E38
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Read");
			this.ThrowIfStreamCannotRead("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int num = this._stream.Read(buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000A9CE8 File Offset: 0x000A7EE8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Write");
			this.ThrowIfStreamCannotWrite("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.Write(buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000A9D98 File Offset: 0x000A7F98
		public override int ReadByte()
		{
			this.ThrowIfStreamClosed("ReadByte");
			this.ThrowIfStreamCannotRead("ReadByte");
			if (this._stream.CanSeek && this._lPosition >= this._stream.Length)
			{
				return -1;
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int result = this._stream.ReadByte();
			this._lPosition += 1L;
			return result;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000A9E2C File Offset: 0x000A802C
		public override void WriteByte(byte value)
		{
			this.ThrowIfStreamClosed("WriteByte");
			this.ThrowIfStreamCannotWrite("WriteByte");
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.WriteByte(value);
			this._lPosition += 1L;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000A9E9D File Offset: 0x000A809D
		public override void SetLength(long value)
		{
			this.ThrowIfStreamClosed("SetLength");
			this.ThrowIfStreamCannotSeek("SetLength");
			this._stream.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000A9ED1 File Offset: 0x000A80D1
		public override void Flush()
		{
			if (this._stream != null)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000A9EE8 File Offset: 0x000A80E8
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._isClosed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000A9F18 File Offset: 0x000A8118
		private void ThrowIfStreamCannotSeek(string method)
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonSeekable(method));
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000A9F33 File Offset: 0x000A8133
		private void ThrowIfStreamCannotRead(string method)
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonReadable(method));
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000A9F4E File Offset: 0x000A814E
		private void ThrowIfStreamCannotWrite(string method)
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonWritable(method));
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000A9F69 File Offset: 0x000A8169
		private void ThrowIfStreamClosed(string method)
		{
			if (this.IsStreamClosed())
			{
				throw new ObjectDisposedException(SQLResource.InvalidOpStreamClosed(method));
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000A9F7F File Offset: 0x000A817F
		private bool IsStreamClosed()
		{
			return this._isClosed || this._stream == null || (!this._stream.CanRead && !this._stream.CanWrite && !this._stream.CanSeek);
		}

		// Token: 0x04001907 RID: 6407
		private Stream _stream;

		// Token: 0x04001908 RID: 6408
		private long _lPosition;

		// Token: 0x04001909 RID: 6409
		private bool _isClosed;
	}
}
