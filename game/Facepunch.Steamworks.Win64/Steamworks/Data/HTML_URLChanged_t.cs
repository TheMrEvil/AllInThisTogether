using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_URLChanged_t : ICallbackData
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00016925 File Offset: 0x00014B25
		public int DataSize
		{
			get
			{
				return HTML_URLChanged_t._datasize;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0001692C File Offset: 0x00014B2C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_URLChanged;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00016933 File Offset: 0x00014B33
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_URLChanged_t()
		{
		}

		// Token: 0x04000A1C RID: 2588
		internal uint UnBrowserHandle;

		// Token: 0x04000A1D RID: 2589
		internal string PchURL;

		// Token: 0x04000A1E RID: 2590
		internal string PchPostData;

		// Token: 0x04000A1F RID: 2591
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect;

		// Token: 0x04000A20 RID: 2592
		internal string PchPageTitle;

		// Token: 0x04000A21 RID: 2593
		[MarshalAs(UnmanagedType.I1)]
		internal bool BNewNavigation;

		// Token: 0x04000A22 RID: 2594
		public static int _datasize = Marshal.SizeOf(typeof(HTML_URLChanged_t));
	}
}
