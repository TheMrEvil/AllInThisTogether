using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C6 RID: 198
	internal struct PolicyInformation
	{
		// Token: 0x0400037C RID: 892
		[ObjectIdentifier]
		public string PolicyIdentifier;

		// Token: 0x0400037D RID: 893
		[OptionalValue]
		public PolicyQualifierInfo[] PolicyQualifiers;
	}
}
