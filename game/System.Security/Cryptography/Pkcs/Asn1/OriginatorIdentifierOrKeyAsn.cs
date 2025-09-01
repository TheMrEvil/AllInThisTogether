using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AE RID: 174
	[Choice]
	internal struct OriginatorIdentifierOrKeyAsn
	{
		// Token: 0x04000310 RID: 784
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		// Token: 0x04000311 RID: 785
		[OctetString]
		[ExpectedTag(0)]
		internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;

		// Token: 0x04000312 RID: 786
		[ExpectedTag(1)]
		internal OriginatorPublicKeyAsn OriginatorKey;
	}
}
