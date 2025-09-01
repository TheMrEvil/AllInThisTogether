using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008F RID: 143
	internal abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00010DA1 File Offset: 0x0000EFA1
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00010DDE File Offset: 0x0000EFDE
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010E17 File Offset: 0x0000F017
		// Note: this type is marked as 'beforefieldinit'.
		static RC4()
		{
		}

		// Token: 0x04000EFF RID: 3839
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000F00 RID: 3840
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
