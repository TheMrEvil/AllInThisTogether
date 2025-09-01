using System;

namespace QFSW.QC
{
	// Token: 0x02000034 RID: 52
	public struct ResponseConfig
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000072D4 File Offset: 0x000054D4
		// Note: this type is marked as 'beforefieldinit'.
		static ResponseConfig()
		{
		}

		// Token: 0x040000FB RID: 251
		public string InputPrompt;

		// Token: 0x040000FC RID: 252
		public bool LogInput;

		// Token: 0x040000FD RID: 253
		public static readonly ResponseConfig Default = new ResponseConfig
		{
			InputPrompt = "Enter input...",
			LogInput = true
		};
	}
}
