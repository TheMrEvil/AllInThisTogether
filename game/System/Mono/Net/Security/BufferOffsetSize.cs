using System;

namespace Mono.Net.Security
{
	// Token: 0x02000087 RID: 135
	internal class BufferOffsetSize
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006233 File Offset: 0x00004433
		public int EndOffset
		{
			get
			{
				return this.Offset + this.Size;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006242 File Offset: 0x00004442
		public int Remaining
		{
			get
			{
				return this.Buffer.Length - this.Offset - this.Size;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000625C File Offset: 0x0000445C
		public BufferOffsetSize(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
			this.Complete = false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000062BF File Offset: 0x000044BF
		public override string ToString()
		{
			return string.Format("[BufferOffsetSize: {0} {1}]", this.Offset, this.Size);
		}

		// Token: 0x040001FB RID: 507
		public byte[] Buffer;

		// Token: 0x040001FC RID: 508
		public int Offset;

		// Token: 0x040001FD RID: 509
		public int Size;

		// Token: 0x040001FE RID: 510
		public int TotalBytes;

		// Token: 0x040001FF RID: 511
		public bool Complete;
	}
}
