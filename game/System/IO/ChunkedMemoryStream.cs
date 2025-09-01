using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004F3 RID: 1267
	internal sealed class ChunkedMemoryStream : Stream
	{
		// Token: 0x0600296B RID: 10603 RVA: 0x0008E97B File Offset: 0x0008CB7B
		internal ChunkedMemoryStream()
		{
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0008E984 File Offset: 0x0008CB84
		public byte[] ToArray()
		{
			byte[] array = new byte[this._totalLength];
			int num = 0;
			for (ChunkedMemoryStream.MemoryChunk memoryChunk = this._headChunk; memoryChunk != null; memoryChunk = memoryChunk._next)
			{
				Buffer.BlockCopy(memoryChunk._buffer, 0, array, num, memoryChunk._freeOffset);
				num += memoryChunk._freeOffset;
			}
			return array;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0008E9D0 File Offset: 0x0008CBD0
		public override void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				if (this._currentChunk != null)
				{
					int num = this._currentChunk._buffer.Length - this._currentChunk._freeOffset;
					if (num > 0)
					{
						int num2 = Math.Min(num, count);
						Buffer.BlockCopy(buffer, offset, this._currentChunk._buffer, this._currentChunk._freeOffset, num2);
						count -= num2;
						offset += num2;
						this._totalLength += num2;
						this._currentChunk._freeOffset += num2;
						continue;
					}
				}
				this.AppendChunk((long)count);
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0008EA6A File Offset: 0x0008CC6A
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			this.Write(buffer, offset, count);
			return Task.CompletedTask;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0008EA8C File Offset: 0x0008CC8C
		private void AppendChunk(long count)
		{
			int num = (this._currentChunk != null) ? (this._currentChunk._buffer.Length * 2) : 1024;
			if (count > (long)num)
			{
				num = (int)Math.Min(count, 1048576L);
			}
			ChunkedMemoryStream.MemoryChunk memoryChunk = new ChunkedMemoryStream.MemoryChunk(num);
			if (this._currentChunk == null)
			{
				this._headChunk = (this._currentChunk = memoryChunk);
				return;
			}
			this._currentChunk._next = memoryChunk;
			this._currentChunk = memoryChunk;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x0008EAFE File Offset: 0x0008CCFE
		public override long Length
		{
			get
			{
				return (long)this._totalLength;
			}
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0008EB07 File Offset: 0x0008CD07
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x06002978 RID: 10616 RVA: 0x000044FA File Offset: 0x000026FA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x0008EB0E File Offset: 0x0008CD0E
		public override void SetLength(long value)
		{
			if (this._currentChunk != null)
			{
				throw new NotSupportedException();
			}
			this.AppendChunk(value);
		}

		// Token: 0x040015E6 RID: 5606
		private ChunkedMemoryStream.MemoryChunk _headChunk;

		// Token: 0x040015E7 RID: 5607
		private ChunkedMemoryStream.MemoryChunk _currentChunk;

		// Token: 0x040015E8 RID: 5608
		private const int InitialChunkDefaultSize = 1024;

		// Token: 0x040015E9 RID: 5609
		private const int MaxChunkSize = 1048576;

		// Token: 0x040015EA RID: 5610
		private int _totalLength;

		// Token: 0x020004F4 RID: 1268
		private sealed class MemoryChunk
		{
			// Token: 0x0600297B RID: 10619 RVA: 0x0008EB25 File Offset: 0x0008CD25
			internal MemoryChunk(int bufferSize)
			{
				this._buffer = new byte[bufferSize];
			}

			// Token: 0x040015EB RID: 5611
			internal readonly byte[] _buffer;

			// Token: 0x040015EC RID: 5612
			internal int _freeOffset;

			// Token: 0x040015ED RID: 5613
			internal ChunkedMemoryStream.MemoryChunk _next;
		}
	}
}
