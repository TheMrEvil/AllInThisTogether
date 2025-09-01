using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000182 RID: 386
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_BrowserRestarted_t : ICallbackData
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00016BAD File Offset: 0x00014DAD
		public int DataSize
		{
			get
			{
				return HTML_BrowserRestarted_t._datasize;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00016BB4 File Offset: 0x00014DB4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_BrowserRestarted;
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00016BBB File Offset: 0x00014DBB
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_BrowserRestarted_t()
		{
		}

		// Token: 0x04000A6A RID: 2666
		internal uint UnBrowserHandle;

		// Token: 0x04000A6B RID: 2667
		internal uint UnOldBrowserHandle;

		// Token: 0x04000A6C RID: 2668
		public static int _datasize = Marshal.SizeOf(typeof(HTML_BrowserRestarted_t));
	}
}
