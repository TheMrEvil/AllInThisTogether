using System;

namespace System
{
	// Token: 0x0200006A RID: 106
	internal static class NotImplemented
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00010D37 File Offset: 0x0000EF37
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00010D3E File Offset: 0x0000EF3E
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00010D37 File Offset: 0x0000EF37
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
