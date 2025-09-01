using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A3 RID: 163
	internal struct EncryptedContentInfoAsn
	{
		// Token: 0x040002E7 RID: 743
		[ObjectIdentifier]
		internal string ContentType;

		// Token: 0x040002E8 RID: 744
		internal AlgorithmIdentifierAsn ContentEncryptionAlgorithm;

		// Token: 0x040002E9 RID: 745
		[ExpectedTag(0)]
		[OctetString]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? EncryptedContent;
	}
}
