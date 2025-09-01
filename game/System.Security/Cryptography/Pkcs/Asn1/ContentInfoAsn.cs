using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A1 RID: 161
	internal struct ContentInfoAsn
	{
		// Token: 0x040002E3 RID: 739
		[ObjectIdentifier]
		public string ContentType;

		// Token: 0x040002E4 RID: 740
		[AnyValue]
		[ExpectedTag(0, ExplicitTag = true)]
		public ReadOnlyMemory<byte> Content;
	}
}
