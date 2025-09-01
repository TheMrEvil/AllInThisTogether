using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000051 RID: 81
	public class BlockProcessor
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00010734 File Offset: 0x0000E934
		public BlockProcessor(ICryptoTransform transform) : this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010743 File Offset: 0x0000E943
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (blockSize <= 0)
			{
				throw new ArgumentOutOfRangeException("blockSize");
			}
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00010784 File Offset: 0x0000E984
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000107BC File Offset: 0x0000E9BC
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000107D7 File Offset: 0x0000E9D7
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Buffer.BlockCopy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.transform.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.transform.TransformBlock(rgb, num + ib, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Buffer.BlockCopy(rgb, num + ib, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000108BD File Offset: 0x0000EABD
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x040002A5 RID: 677
		private ICryptoTransform transform;

		// Token: 0x040002A6 RID: 678
		private byte[] block;

		// Token: 0x040002A7 RID: 679
		private int blockSize;

		// Token: 0x040002A8 RID: 680
		private int blockCount;
	}
}
