using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016C RID: 364
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_BrowserReady_t : ICallbackData
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00016895 File Offset: 0x00014A95
		public int DataSize
		{
			get
			{
				return HTML_BrowserReady_t._datasize;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0001689C File Offset: 0x00014A9C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_BrowserReady;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000168A3 File Offset: 0x00014AA3
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_BrowserReady_t()
		{
		}

		// Token: 0x04000A05 RID: 2565
		internal uint UnBrowserHandle;

		// Token: 0x04000A06 RID: 2566
		public static int _datasize = Marshal.SizeOf(typeof(HTML_BrowserReady_t));
	}
}
