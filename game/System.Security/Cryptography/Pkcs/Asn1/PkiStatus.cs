using System;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BD RID: 189
	internal enum PkiStatus
	{
		// Token: 0x04000355 RID: 853
		Granted,
		// Token: 0x04000356 RID: 854
		GrantedWithMods,
		// Token: 0x04000357 RID: 855
		Rejection,
		// Token: 0x04000358 RID: 856
		Waiting,
		// Token: 0x04000359 RID: 857
		RevocationWarning,
		// Token: 0x0400035A RID: 858
		RevocationNotification,
		// Token: 0x0400035B RID: 859
		KeyUpdateWarning
	}
}
