using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A4 RID: 164
	internal struct EnvelopedDataAsn
	{
		// Token: 0x040002EA RID: 746
		public int Version;

		// Token: 0x040002EB RID: 747
		[OptionalValue]
		[ExpectedTag(0)]
		public OriginatorInfoAsn OriginatorInfo;

		// Token: 0x040002EC RID: 748
		[SetOf]
		public RecipientInfoAsn[] RecipientInfos;

		// Token: 0x040002ED RID: 749
		public EncryptedContentInfoAsn EncryptedContentInfo;

		// Token: 0x040002EE RID: 750
		[OptionalValue]
		[ExpectedTag(1)]
		[SetOf]
		public AttributeAsn[] UnprotectedAttributes;
	}
}
