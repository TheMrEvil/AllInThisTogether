using System;

namespace Mono
{
	// Token: 0x0200003D RID: 61
	[Flags]
	internal enum CertificateImportFlags
	{
		// Token: 0x04000DBF RID: 3519
		None = 0,
		// Token: 0x04000DC0 RID: 3520
		DisableNativeBackend = 1,
		// Token: 0x04000DC1 RID: 3521
		DisableAutomaticFallback = 2
	}
}
