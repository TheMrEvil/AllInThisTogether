using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000642 RID: 1602
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002702 RID: 9986
		None = 0,
		// Token: 0x04002703 RID: 9987
		AppDomainID = 1,
		// Token: 0x04002704 RID: 9988
		ProcessID = 2,
		// Token: 0x04002705 RID: 9989
		CLRInstanceID = 4,
		// Token: 0x04002706 RID: 9990
		AssemblyName = 8,
		// Token: 0x04002707 RID: 9991
		TypeName = 16
	}
}
