using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000023 RID: 35
	[Flags]
	internal enum LogLevel
	{
		// Token: 0x0400006C RID: 108
		None = 0,
		// Token: 0x0400006D RID: 109
		Error = 1,
		// Token: 0x0400006E RID: 110
		Warning = 2,
		// Token: 0x0400006F RID: 111
		Info = 4,
		// Token: 0x04000070 RID: 112
		Default = 3,
		// Token: 0x04000071 RID: 113
		All = 255
	}
}
