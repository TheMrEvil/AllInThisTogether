using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017E RID: 382
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_StatusText_t : ICallbackData
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00016B1D File Offset: 0x00014D1D
		public int DataSize
		{
			get
			{
				return HTML_StatusText_t._datasize;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00016B24 File Offset: 0x00014D24
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_StatusText;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00016B2B File Offset: 0x00014D2B
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_StatusText_t()
		{
		}

		// Token: 0x04000A5F RID: 2655
		internal uint UnBrowserHandle;

		// Token: 0x04000A60 RID: 2656
		internal string PchMsg;

		// Token: 0x04000A61 RID: 2657
		public static int _datasize = Marshal.SizeOf(typeof(HTML_StatusText_t));
	}
}
