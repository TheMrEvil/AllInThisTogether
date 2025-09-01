using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C1 RID: 193
	internal struct SignerInfoAsn
	{
		// Token: 0x0400036E RID: 878
		public int Version;

		// Token: 0x0400036F RID: 879
		public SignerIdentifierAsn Sid;

		// Token: 0x04000370 RID: 880
		public AlgorithmIdentifierAsn DigestAlgorithm;

		// Token: 0x04000371 RID: 881
		[ExpectedTag(0)]
		[OptionalValue]
		[AnyValue]
		public ReadOnlyMemory<byte>? SignedAttributes;

		// Token: 0x04000372 RID: 882
		public AlgorithmIdentifierAsn SignatureAlgorithm;

		// Token: 0x04000373 RID: 883
		[OctetString]
		public ReadOnlyMemory<byte> SignatureValue;

		// Token: 0x04000374 RID: 884
		[ExpectedTag(1)]
		[SetOf]
		[OptionalValue]
		public AttributeAsn[] UnsignedAttributes;
	}
}
