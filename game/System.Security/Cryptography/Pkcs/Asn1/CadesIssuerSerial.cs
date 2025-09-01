using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C5 RID: 197
	internal struct CadesIssuerSerial
	{
		// Token: 0x0400037A RID: 890
		public GeneralName[] Issuer;

		// Token: 0x0400037B RID: 891
		[Integer]
		public ReadOnlyMemory<byte> SerialNumber;
	}
}
