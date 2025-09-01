using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000063 RID: 99
	internal class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00014AAC File Offset: 0x00012CAC
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.hashing)
				{
					throw new Exception("Cannot change key during hash operation.");
				}
				if (value.Length > 64)
				{
					this.KeyValue = this.hash.ComputeHash(value);
				}
				else
				{
					this.KeyValue = (byte[])value.Clone();
				}
				this.initializePad();
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014B14 File Offset: 0x00012D14
		public HMAC()
		{
			this.hash = MD5.Create();
			this.HashSizeValue = this.hash.HashSize;
			byte[] array = new byte[64];
			new RNGCryptoServiceProvider().GetNonZeroBytes(array);
			this.KeyValue = (byte[])array.Clone();
			this.Initialize();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014B70 File Offset: 0x00012D70
		public HMAC(HashAlgorithm ha, byte[] rgbKey)
		{
			this.hash = ha;
			this.HashSizeValue = this.hash.HashSize;
			if (rgbKey.Length > 64)
			{
				this.KeyValue = this.hash.ComputeHash(rgbKey);
			}
			else
			{
				this.KeyValue = (byte[])rgbKey.Clone();
			}
			this.Initialize();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014BCD File Offset: 0x00012DCD
		public override void Initialize()
		{
			this.hash.Initialize();
			this.initializePad();
			this.hashing = false;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014BE8 File Offset: 0x00012DE8
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = this.hash.Hash;
			this.hash.Initialize();
			this.hash.TransformBlock(this.outerPad, 0, this.outerPad.Length, this.outerPad, 0);
			this.hash.TransformFinalBlock(array, 0, array.Length);
			this.Initialize();
			return this.hash.Hash;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00014C98 File Offset: 0x00012E98
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014CE8 File Offset: 0x00012EE8
		private void initializePad()
		{
			this.innerPad = new byte[64];
			this.outerPad = new byte[64];
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				this.innerPad[i] = (this.KeyValue[i] ^ 54);
				this.outerPad[i] = (this.KeyValue[i] ^ 92);
			}
			for (int j = this.KeyValue.Length; j < 64; j++)
			{
				this.innerPad[j] = 54;
				this.outerPad[j] = 92;
			}
		}

		// Token: 0x040002FD RID: 765
		private HashAlgorithm hash;

		// Token: 0x040002FE RID: 766
		private bool hashing;

		// Token: 0x040002FF RID: 767
		private byte[] innerPad;

		// Token: 0x04000300 RID: 768
		private byte[] outerPad;
	}
}
