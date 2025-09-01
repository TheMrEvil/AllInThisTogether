using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C2 RID: 194
	[Choice]
	internal struct SignedAttributesSet
	{
		// Token: 0x04000375 RID: 885
		[SetOf]
		[ExpectedTag(0)]
		public AttributeAsn[] SignedAttributes;
	}
}
