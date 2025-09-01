using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BA RID: 186
	internal struct Rfc3161TimeStampResp
	{
		// Token: 0x04000332 RID: 818
		public PkiStatusInfo Status;

		// Token: 0x04000333 RID: 819
		[AnyValue]
		[OptionalValue]
		public ReadOnlyMemory<byte>? TimeStampToken;
	}
}
