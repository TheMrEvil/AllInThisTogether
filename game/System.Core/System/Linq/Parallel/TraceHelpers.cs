using System;
using System.Diagnostics;

namespace System.Linq.Parallel
{
	// Token: 0x020001FE RID: 510
	internal static class TraceHelpers
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00003A59 File Offset: 0x00001C59
		[Conditional("PFXTRACE")]
		internal static void TraceInfo(string msg, params object[] args)
		{
		}
	}
}
