using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004F6 RID: 1270
	internal sealed class ReadOnlyMemoryStream : Stream
	{
		// Token: 0x06002989 RID: 10633 RVA: 0x0008EEB1 File Offset: 0x0008D0B1
		public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
		{
			this._content = content;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x0008EEC0 File Offset: 0x0008D0C0
		public override long Length
		{
			get
			{
				return (long)this._content.Length;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x0008EECE File Offset: 0x0008D0CE
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x0008EED7 File Offset: 0x0008D0D7
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				if (value < 0L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0008EEFC File Offset: 0x0008D0FC
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.Current)
				{
					if (origin != SeekOrigin.End)
					{
						throw new ArgumentOutOfRangeException("origin");
					}
					num = (long)this._content.Length + offset;
				}
				else
				{
					num = (long)this._position + offset;
				}
			}
			else
			{
				num = offset;
			}
			long num2 = num;
			if (num2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (num2 < 0L)
			{
				throw new IOException("An attempt was made to move the position before the beginning of the stream.");
			}
			this._position = (int)num2;
			return (long)this._position;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0008EF70 File Offset: 0x0008D170
		public unsafe override int ReadByte()
		{
			ReadOnlySpan<byte> span = this._content.Span;
			if (this._position >= span.Length)
			{
				return -1;
			}
			int position = this._position;
			this._position = position + 1;
			return (int)(*span[position]);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0008EFB3 File Offset: 0x0008D1B3
		public override int Read(byte[] buffer, int offset, int count)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			return this.Read(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0008EFCC File Offset: 0x0008D1CC
		public override int Read(Span<byte> buffer)
		{
			int num = this._content.Length - this._position;
			if (num <= 0 || buffer.Length == 0)
			{
				return 0;
			}
			if (num <= buffer.Length)
			{
				this._content.Span.Slice(this._position).CopyTo(buffer);
				this._position = this._content.Length;
				return num;
			}
			this._content.Span.Slice(this._position, buffer.Length).CopyTo(buffer);
			this._position += buffer.Length;
			return buffer.Length;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0008F07E File Offset: 0x0008D27E
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult<int>(this.Read(new Span<byte>(buffer, offset, count)));
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0008F0AC File Offset: 0x0008D2AC
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(this.Read(buffer.Span));
			}
			return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0008F0D5 File Offset: 0x0008D2D5
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(base.ReadAsync(buffer, offset, count), callback, state);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0008F0E9 File Offset: 0x0008D2E9
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0008F0F4 File Offset: 0x0008D2F4
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length > this._position)
			{
				destination.Write(this._content.Span.Slice(this._position));
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0008F13C File Offset: 0x0008D33C
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length <= this._position)
			{
				return Task.CompletedTask;
			}
			return destination.WriteAsync(this._content.Slice(this._position), cancellationToken).AsTask();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x0008EB07 File Offset: 0x0008CD07
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0008F18A File Offset: 0x0008D38A
		private static void ValidateReadArrayArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x040015F6 RID: 5622
		private readonly ReadOnlyMemory<byte> _content;

		// Token: 0x040015F7 RID: 5623
		private int _position;
	}
}
