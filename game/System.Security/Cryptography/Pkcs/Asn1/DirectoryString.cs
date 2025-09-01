using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A8 RID: 168
	[Choice]
	internal struct DirectoryString
	{
		// Token: 0x040002FC RID: 764
		[ExpectedTag(TagClass.Universal, 20)]
		internal ReadOnlyMemory<byte>? TeletexString;

		// Token: 0x040002FD RID: 765
		[PrintableString]
		internal string PrintableString;

		// Token: 0x040002FE RID: 766
		[ExpectedTag(TagClass.Universal, 28)]
		internal ReadOnlyMemory<byte>? UniversalString;

		// Token: 0x040002FF RID: 767
		[UTF8String]
		internal string Utf8String;

		// Token: 0x04000300 RID: 768
		[BMPString]
		internal string BMPString;
	}
}
