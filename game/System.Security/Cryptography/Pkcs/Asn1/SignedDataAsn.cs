using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BF RID: 191
	internal struct SignedDataAsn
	{
		// Token: 0x04000366 RID: 870
		public int Version;

		// Token: 0x04000367 RID: 871
		[SetOf]
		public AlgorithmIdentifierAsn[] DigestAlgorithms;

		// Token: 0x04000368 RID: 872
		public EncapsulatedContentInfoAsn EncapContentInfo;

		// Token: 0x04000369 RID: 873
		[ExpectedTag(0)]
		[SetOf]
		[OptionalValue]
		public CertificateChoiceAsn[] CertificateSet;

		// Token: 0x0400036A RID: 874
		[AnyValue]
		[ExpectedTag(1)]
		[OptionalValue]
		public ReadOnlyMemory<byte>? RevocationInfoChoices;

		// Token: 0x0400036B RID: 875
		[SetOf]
		public SignerInfoAsn[] SignerInfos;
	}
}
