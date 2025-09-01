using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B5 RID: 181
	[Choice]
	internal struct RecipientIdentifierAsn
	{
		// Token: 0x04000322 RID: 802
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		// Token: 0x04000323 RID: 803
		[ExpectedTag(0)]
		[OctetString]
		internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;
	}
}
