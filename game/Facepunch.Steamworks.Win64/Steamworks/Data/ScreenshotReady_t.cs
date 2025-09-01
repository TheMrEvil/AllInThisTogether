using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000144 RID: 324
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ScreenshotReady_t : ICallbackData
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000162D6 File Offset: 0x000144D6
		public int DataSize
		{
			get
			{
				return ScreenshotReady_t._datasize;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000162DD File Offset: 0x000144DD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ScreenshotReady;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000162E4 File Offset: 0x000144E4
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenshotReady_t()
		{
		}

		// Token: 0x04000994 RID: 2452
		internal uint Local;

		// Token: 0x04000995 RID: 2453
		internal Result Result;

		// Token: 0x04000996 RID: 2454
		public static int _datasize = Marshal.SizeOf(typeof(ScreenshotReady_t));
	}
}
