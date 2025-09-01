using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x02000644 RID: 1604
	[FriendAccessAllowed]
	internal enum TargetFrameworkId
	{
		// Token: 0x0400270B RID: 9995
		NotYetChecked,
		// Token: 0x0400270C RID: 9996
		Unrecognized,
		// Token: 0x0400270D RID: 9997
		Unspecified,
		// Token: 0x0400270E RID: 9998
		NetFramework,
		// Token: 0x0400270F RID: 9999
		Portable,
		// Token: 0x04002710 RID: 10000
		NetCore,
		// Token: 0x04002711 RID: 10001
		Silverlight,
		// Token: 0x04002712 RID: 10002
		Phone
	}
}
