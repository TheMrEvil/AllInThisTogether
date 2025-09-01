using System;

namespace System
{
	// Token: 0x02000143 RID: 323
	internal static class NotImplemented
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001FD36 File Offset: 0x0001DF36
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
