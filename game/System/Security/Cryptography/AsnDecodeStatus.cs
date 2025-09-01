using System;

namespace System.Security.Cryptography
{
	// Token: 0x020002B9 RID: 697
	internal enum AsnDecodeStatus
	{
		// Token: 0x04000C45 RID: 3141
		NotDecoded = -1,
		// Token: 0x04000C46 RID: 3142
		Ok,
		// Token: 0x04000C47 RID: 3143
		BadAsn,
		// Token: 0x04000C48 RID: 3144
		BadTag,
		// Token: 0x04000C49 RID: 3145
		BadLength,
		// Token: 0x04000C4A RID: 3146
		InformationNotAvailable
	}
}
