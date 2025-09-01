using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_CloseBrowser_t : ICallbackData
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00016901 File Offset: 0x00014B01
		public int DataSize
		{
			get
			{
				return HTML_CloseBrowser_t._datasize;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00016908 File Offset: 0x00014B08
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_CloseBrowser;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001690F File Offset: 0x00014B0F
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_CloseBrowser_t()
		{
		}

		// Token: 0x04000A1A RID: 2586
		internal uint UnBrowserHandle;

		// Token: 0x04000A1B RID: 2587
		public static int _datasize = Marshal.SizeOf(typeof(HTML_CloseBrowser_t));
	}
}
