using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017C RID: 380
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_NewWindow_t : ICallbackData
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00016AD5 File Offset: 0x00014CD5
		public int DataSize
		{
			get
			{
				return HTML_NewWindow_t._datasize;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00016ADC File Offset: 0x00014CDC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_NewWindow;
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00016AE3 File Offset: 0x00014CE3
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_NewWindow_t()
		{
		}

		// Token: 0x04000A54 RID: 2644
		internal uint UnBrowserHandle;

		// Token: 0x04000A55 RID: 2645
		internal string PchURL;

		// Token: 0x04000A56 RID: 2646
		internal uint UnX;

		// Token: 0x04000A57 RID: 2647
		internal uint UnY;

		// Token: 0x04000A58 RID: 2648
		internal uint UnWide;

		// Token: 0x04000A59 RID: 2649
		internal uint UnTall;

		// Token: 0x04000A5A RID: 2650
		internal uint UnNewWindow_BrowserHandle_IGNORE;

		// Token: 0x04000A5B RID: 2651
		public static int _datasize = Marshal.SizeOf(typeof(HTML_NewWindow_t));
	}
}
