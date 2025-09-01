using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000095 RID: 149
	internal class HMACAlgorithm
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0001319D File Offset: 0x0001139D
		public HMACAlgorithm(string algoName)
		{
			this.CreateHash(algoName);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000131AC File Offset: 0x000113AC
		~HMACAlgorithm()
		{
			this.Dispose();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000131D8 File Offset: 0x000113D8
		private void CreateHash(string algoName)
		{
			this.algo = HashAlgorithm.Create(algoName);
			this.hashName = algoName;
			this.block = new BlockProcessor(this.algo, 8);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000131FF File Offset: 0x000113FF
		public void Dispose()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001321D File Offset: 0x0001141D
		public HashAlgorithm Algo
		{
			get
			{
				return this.algo;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00013225 File Offset: 0x00011425
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0001322D File Offset: 0x0001142D
		public string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				this.CreateHash(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00013236 File Offset: 0x00011436
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0001323E File Offset: 0x0001143E
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (value != null && value.Length > 64)
				{
					this.key = this.algo.ComputeHash(value);
					return;
				}
				this.key = (byte[])value.Clone();
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013270 File Offset: 0x00011470
		public void Initialize()
		{
			this.hash = null;
			this.block.Initialize();
			byte[] array = this.KeySetup(this.key, 54);
			this.algo.Initialize();
			this.block.Core(array);
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000132C0 File Offset: 0x000114C0
		private byte[] KeySetup(byte[] key, byte padding)
		{
			byte[] array = new byte[64];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = (key[i] ^ padding);
			}
			for (int j = key.Length; j < 64; j++)
			{
				array[j] = padding;
			}
			return array;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013300 File Offset: 0x00011500
		public void Core(byte[] rgb, int ib, int cb)
		{
			this.block.Core(rgb, ib, cb);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013310 File Offset: 0x00011510
		public byte[] Final()
		{
			this.block.Final();
			byte[] array = this.algo.Hash;
			byte[] array2 = this.KeySetup(this.key, 92);
			this.algo.Initialize();
			this.algo.TransformBlock(array2, 0, array2.Length, array2, 0);
			this.algo.TransformFinalBlock(array, 0, array.Length);
			this.hash = this.algo.Hash;
			this.algo.Clear();
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array, 0, array.Length);
			return this.hash;
		}

		// Token: 0x04000F29 RID: 3881
		private byte[] key;

		// Token: 0x04000F2A RID: 3882
		private byte[] hash;

		// Token: 0x04000F2B RID: 3883
		private HashAlgorithm algo;

		// Token: 0x04000F2C RID: 3884
		private string hashName;

		// Token: 0x04000F2D RID: 3885
		private BlockProcessor block;
	}
}
