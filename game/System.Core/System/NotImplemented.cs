using System;

namespace System
{
	// Token: 0x0200001A RID: 26
	internal static class NotImplemented
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000023E6 File Offset: 0x000005E6
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000023ED File Offset: 0x000005ED
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000023E6 File Offset: 0x000005E6
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
