using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000059 RID: 89
	public abstract class MD4 : HashAlgorithm
	{
		// Token: 0x06000364 RID: 868 RVA: 0x000119BE File Offset: 0x0000FBBE
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000119D1 File Offset: 0x0000FBD1
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public new static MD4 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD4Managed();
			}
			return (MD4)obj;
		}
	}
}
