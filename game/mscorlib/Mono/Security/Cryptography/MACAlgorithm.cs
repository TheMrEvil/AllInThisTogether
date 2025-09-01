using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000096 RID: 150
	internal class MACAlgorithm
	{
		// Token: 0x0600038B RID: 907 RVA: 0x000133AC File Offset: 0x000115AC
		public MACAlgorithm(SymmetricAlgorithm algorithm)
		{
			this.algo = algorithm;
			this.algo.Mode = CipherMode.CBC;
			this.blockSize = this.algo.BlockSize >> 3;
			this.algo.IV = new byte[this.blockSize];
			this.block = new byte[this.blockSize];
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001340C File Offset: 0x0001160C
		public void Initialize(byte[] key)
		{
			this.algo.Key = key;
			if (this.enc == null)
			{
				this.enc = this.algo.CreateEncryptor();
			}
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001344C File Offset: 0x0001164C
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Array.Copy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.enc.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.enc.TransformBlock(rgb, num, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Array.Copy(rgb, num, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00013524 File Offset: 0x00011724
		public byte[] Final()
		{
			byte[] result;
			if (this.blockCount > 0 || (this.algo.Padding != PaddingMode.Zeros && this.algo.Padding != PaddingMode.None))
			{
				result = this.enc.TransformFinalBlock(this.block, 0, this.blockCount);
			}
			else
			{
				result = (byte[])this.block.Clone();
			}
			if (!this.enc.CanReuseTransform)
			{
				this.enc.Dispose();
				this.enc = null;
			}
			return result;
		}

		// Token: 0x04000F2E RID: 3886
		private SymmetricAlgorithm algo;

		// Token: 0x04000F2F RID: 3887
		private ICryptoTransform enc;

		// Token: 0x04000F30 RID: 3888
		private byte[] block;

		// Token: 0x04000F31 RID: 3889
		private int blockSize;

		// Token: 0x04000F32 RID: 3890
		private int blockCount;
	}
}
