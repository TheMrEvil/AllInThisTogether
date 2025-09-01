using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A2 RID: 162
	internal struct EncapsulatedContentInfoAsn
	{
		// Token: 0x040002E5 RID: 741
		[ObjectIdentifier]
		public string ContentType;

		// Token: 0x040002E6 RID: 742
		[AnyValue]
		[ExpectedTag(0, ExplicitTag = true)]
		[OptionalValue]
		public ReadOnlyMemory<byte>? Content;
	}
}
