using System;

namespace System
{
	// Token: 0x020001BA RID: 442
	internal static class NotImplemented
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004D851 File Offset: 0x0004BA51
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004D858 File Offset: 0x0004BA58
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004D851 File Offset: 0x0004BA51
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
