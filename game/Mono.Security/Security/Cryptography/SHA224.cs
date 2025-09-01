using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000060 RID: 96
	public abstract class SHA224 : HashAlgorithm
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0001399B File Offset: 0x00011B9B
		public SHA224()
		{
			this.HashSizeValue = 224;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000139AE File Offset: 0x00011BAE
		public new static SHA224 Create()
		{
			return SHA224.Create("SHA224");
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000139BC File Offset: 0x00011BBC
		public new static SHA224 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new SHA224Managed();
			}
			return (SHA224)obj;
		}
	}
}
