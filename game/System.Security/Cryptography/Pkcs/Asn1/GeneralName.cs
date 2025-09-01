using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A5 RID: 165
	[Choice]
	internal struct GeneralName
	{
		// Token: 0x040002EF RID: 751
		[ExpectedTag(0, ExplicitTag = true)]
		internal OtherName? OtherName;

		// Token: 0x040002F0 RID: 752
		[IA5String]
		[ExpectedTag(1, ExplicitTag = true)]
		internal string Rfc822Name;

		// Token: 0x040002F1 RID: 753
		[IA5String]
		[ExpectedTag(2, ExplicitTag = true)]
		internal string DnsName;

		// Token: 0x040002F2 RID: 754
		[AnyValue]
		[ExpectedTag(3, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? X400Address;

		// Token: 0x040002F3 RID: 755
		[AnyValue]
		[ExpectedTag(4, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? DirectoryName;

		// Token: 0x040002F4 RID: 756
		[ExpectedTag(5, ExplicitTag = true)]
		internal EdiPartyName? EdiPartyName;

		// Token: 0x040002F5 RID: 757
		[IA5String]
		[ExpectedTag(6, ExplicitTag = true)]
		internal string Uri;

		// Token: 0x040002F6 RID: 758
		[OctetString]
		[ExpectedTag(7, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? IPAddress;

		// Token: 0x040002F7 RID: 759
		[ExpectedTag(8, ExplicitTag = true)]
		[ObjectIdentifier]
		internal string RegisteredId;
	}
}
