using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_FinishedRequest_t : ICallbackData
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00016949 File Offset: 0x00014B49
		public int DataSize
		{
			get
			{
				return HTML_FinishedRequest_t._datasize;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00016950 File Offset: 0x00014B50
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_FinishedRequest;
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00016957 File Offset: 0x00014B57
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_FinishedRequest_t()
		{
		}

		// Token: 0x04000A23 RID: 2595
		internal uint UnBrowserHandle;

		// Token: 0x04000A24 RID: 2596
		internal string PchURL;

		// Token: 0x04000A25 RID: 2597
		internal string PchPageTitle;

		// Token: 0x04000A26 RID: 2598
		public static int _datasize = Marshal.SizeOf(typeof(HTML_FinishedRequest_t));
	}
}
