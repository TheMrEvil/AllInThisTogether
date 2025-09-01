using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200005D RID: 93
	public abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00012C15 File Offset: 0x00010E15
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00012C1D File Offset: 0x00010E1D
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

		// Token: 0x06000395 RID: 917 RVA: 0x00012C1F File Offset: 0x00010E1F
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00012C2C File Offset: 0x00010E2C
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00012C4F File Offset: 0x00010E4F
		// Note: this type is marked as 'beforefieldinit'.
		static RC4()
		{
		}

		// Token: 0x040002DA RID: 730
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x040002DB RID: 731
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
