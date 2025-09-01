using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C7 RID: 199
	internal struct PolicyQualifierInfo
	{
		// Token: 0x0400037E RID: 894
		[ObjectIdentifier]
		public string PolicyQualifierId;

		// Token: 0x0400037F RID: 895
		[AnyValue]
		public ReadOnlyMemory<byte> Qualifier;
	}
}
