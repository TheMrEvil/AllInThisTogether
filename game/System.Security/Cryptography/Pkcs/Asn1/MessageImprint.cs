using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AD RID: 173
	internal struct MessageImprint
	{
		// Token: 0x0400030E RID: 782
		internal AlgorithmIdentifierAsn HashAlgorithm;

		// Token: 0x0400030F RID: 783
		[OctetString]
		internal ReadOnlyMemory<byte> HashedMessage;
	}
}
