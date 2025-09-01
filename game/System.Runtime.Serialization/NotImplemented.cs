using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class NotImplemented
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B9 File Offset: 0x000002B9
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020C0 File Offset: 0x000002C0
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}
	}
}
