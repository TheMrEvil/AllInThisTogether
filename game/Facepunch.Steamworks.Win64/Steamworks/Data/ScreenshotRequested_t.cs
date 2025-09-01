using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000145 RID: 325
	internal struct ScreenshotRequested_t : ICallbackData
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000162FA File Offset: 0x000144FA
		public int DataSize
		{
			get
			{
				return ScreenshotRequested_t._datasize;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00016301 File Offset: 0x00014501
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ScreenshotRequested;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00016308 File Offset: 0x00014508
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenshotRequested_t()
		{
		}

		// Token: 0x04000997 RID: 2455
		public static int _datasize = Marshal.SizeOf(typeof(ScreenshotRequested_t));
	}
}
