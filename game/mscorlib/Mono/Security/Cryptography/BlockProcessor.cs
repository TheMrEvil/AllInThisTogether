using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000088 RID: 136
	internal class BlockProcessor
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000F058 File Offset: 0x0000D258
		public BlockProcessor(ICryptoTransform transform) : this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000F067 File Offset: 0x0000D267
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

		// Token: 0x060002D9 RID: 729 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F0FB File Offset: 0x0000D2FB
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F108 File Offset: 0x0000D308
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

		// Token: 0x060002DD RID: 733 RVA: 0x0000F1E1 File Offset: 0x0000D3E1
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x04000ED7 RID: 3799
		private ICryptoTransform transform;

		// Token: 0x04000ED8 RID: 3800
		private byte[] block;

		// Token: 0x04000ED9 RID: 3801
		private int blockSize;

		// Token: 0x04000EDA RID: 3802
		private int blockCount;
	}
}
