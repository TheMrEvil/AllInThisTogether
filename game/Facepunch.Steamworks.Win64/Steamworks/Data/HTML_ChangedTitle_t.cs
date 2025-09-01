using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_ChangedTitle_t : ICallbackData
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00016991 File Offset: 0x00014B91
		public int DataSize
		{
			get
			{
				return HTML_ChangedTitle_t._datasize;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00016998 File Offset: 0x00014B98
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_ChangedTitle;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001699F File Offset: 0x00014B9F
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_ChangedTitle_t()
		{
		}

		// Token: 0x04000A2A RID: 2602
		internal uint UnBrowserHandle;

		// Token: 0x04000A2B RID: 2603
		internal string PchTitle;

		// Token: 0x04000A2C RID: 2604
		public static int _datasize = Marshal.SizeOf(typeof(HTML_ChangedTitle_t));
	}
}
