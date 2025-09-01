using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017F RID: 383
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_ShowToolTip_t : ICallbackData
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00016B41 File Offset: 0x00014D41
		public int DataSize
		{
			get
			{
				return HTML_ShowToolTip_t._datasize;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00016B48 File Offset: 0x00014D48
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_ShowToolTip;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00016B4F File Offset: 0x00014D4F
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_ShowToolTip_t()
		{
		}

		// Token: 0x04000A62 RID: 2658
		internal uint UnBrowserHandle;

		// Token: 0x04000A63 RID: 2659
		internal string PchMsg;

		// Token: 0x04000A64 RID: 2660
		public static int _datasize = Marshal.SizeOf(typeof(HTML_ShowToolTip_t));
	}
}
