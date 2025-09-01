using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AA RID: 170
	[Choice]
	internal struct KeyAgreeRecipientIdentifierAsn
	{
		// Token: 0x04000303 RID: 771
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		// Token: 0x04000304 RID: 772
		[ExpectedTag(0)]
		internal RecipientKeyIdentifier RKeyId;
	}
}
