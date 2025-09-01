using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BB RID: 187
	internal struct PkiStatusInfo
	{
		// Token: 0x04000334 RID: 820
		public int Status;

		// Token: 0x04000335 RID: 821
		[OptionalValue]
		[AnyValue]
		[ExpectedTag(TagClass.Universal, 16)]
		public ReadOnlyMemory<byte>? StatusString;

		// Token: 0x04000336 RID: 822
		[OptionalValue]
		public PkiFailureInfo? FailInfo;
	}
}
