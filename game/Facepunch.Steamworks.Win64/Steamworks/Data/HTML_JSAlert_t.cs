using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000179 RID: 377
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_JSAlert_t : ICallbackData
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00016A69 File Offset: 0x00014C69
		public int DataSize
		{
			get
			{
				return HTML_JSAlert_t._datasize;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00016A70 File Offset: 0x00014C70
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_JSAlert;
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00016A77 File Offset: 0x00014C77
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_JSAlert_t()
		{
		}

		// Token: 0x04000A4A RID: 2634
		internal uint UnBrowserHandle;

		// Token: 0x04000A4B RID: 2635
		internal string PchMessage;

		// Token: 0x04000A4C RID: 2636
		public static int _datasize = Marshal.SizeOf(typeof(HTML_JSAlert_t));
	}
}
