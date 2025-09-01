using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008C RID: 140
	internal abstract class MD4 : HashAlgorithm
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000F94D File Offset: 0x0000DB4D
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000FC3A File Offset: 0x0000DE3A
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FC48 File Offset: 0x0000DE48
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
