using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017A RID: 378
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_JSConfirm_t : ICallbackData
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00016A8D File Offset: 0x00014C8D
		public int DataSize
		{
			get
			{
				return HTML_JSConfirm_t._datasize;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00016A94 File Offset: 0x00014C94
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_JSConfirm;
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00016A9B File Offset: 0x00014C9B
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_JSConfirm_t()
		{
		}

		// Token: 0x04000A4D RID: 2637
		internal uint UnBrowserHandle;

		// Token: 0x04000A4E RID: 2638
		internal string PchMessage;

		// Token: 0x04000A4F RID: 2639
		public static int _datasize = Marshal.SizeOf(typeof(HTML_JSConfirm_t));
	}
}
