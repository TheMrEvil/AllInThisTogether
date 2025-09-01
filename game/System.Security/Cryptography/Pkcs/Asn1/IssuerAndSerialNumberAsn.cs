using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A9 RID: 169
	internal struct IssuerAndSerialNumberAsn
	{
		// Token: 0x04000301 RID: 769
		[AnyValue]
		public ReadOnlyMemory<byte> Issuer;

		// Token: 0x04000302 RID: 770
		[Integer]
		public ReadOnlyMemory<byte> SerialNumber;
	}
}
