using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C0 RID: 192
	[Choice]
	internal struct SignerIdentifierAsn
	{
		// Token: 0x0400036C RID: 876
		public IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		// Token: 0x0400036D RID: 877
		[OctetString]
		[ExpectedTag(0)]
		public ReadOnlyMemory<byte>? SubjectKeyIdentifier;
	}
}
