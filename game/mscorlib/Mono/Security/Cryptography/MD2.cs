using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008A RID: 138
	internal abstract class MD2 : HashAlgorithm
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000F94D File Offset: 0x0000DB4D
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000F960 File Offset: 0x0000DB60
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F96C File Offset: 0x0000DB6C
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
