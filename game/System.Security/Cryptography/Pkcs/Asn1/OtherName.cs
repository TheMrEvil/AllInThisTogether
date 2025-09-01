using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A6 RID: 166
	internal struct OtherName
	{
		// Token: 0x040002F8 RID: 760
		internal string TypeId;

		// Token: 0x040002F9 RID: 761
		[ExpectedTag(0, ExplicitTag = true)]
		[AnyValue]
		internal ReadOnlyMemory<byte> Value;
	}
}
