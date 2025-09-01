using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A0 RID: 160
	[Choice]
	internal struct CertificateChoiceAsn
	{
		// Token: 0x040002E2 RID: 738
		[ExpectedTag(TagClass.Universal, 16)]
		[AnyValue]
		public ReadOnlyMemory<byte>? Certificate;
	}
}
