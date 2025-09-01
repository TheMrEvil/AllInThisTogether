using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B6 RID: 182
	[Choice]
	internal struct RecipientInfoAsn
	{
		// Token: 0x04000324 RID: 804
		internal KeyTransRecipientInfoAsn Ktri;

		// Token: 0x04000325 RID: 805
		[ExpectedTag(1)]
		internal KeyAgreeRecipientInfoAsn Kari;
	}
}
