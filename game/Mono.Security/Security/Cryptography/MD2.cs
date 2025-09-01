using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000057 RID: 87
	public abstract class MD2 : HashAlgorithm
	{
		// Token: 0x0600035A RID: 858 RVA: 0x000116D1 File Offset: 0x0000F8D1
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000116E4 File Offset: 0x0000F8E4
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public new static MD2 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD2Managed();
			}
			return (MD2)obj;
		}
	}
}
