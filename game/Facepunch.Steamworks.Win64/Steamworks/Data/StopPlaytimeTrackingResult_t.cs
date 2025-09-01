using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StopPlaytimeTrackingResult_t : ICallbackData
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00016751 File Offset: 0x00014951
		public int DataSize
		{
			get
			{
				return StopPlaytimeTrackingResult_t._datasize;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00016758 File Offset: 0x00014958
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.StopPlaytimeTrackingResult;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0001675F File Offset: 0x0001495F
		// Note: this type is marked as 'beforefieldinit'.
		static StopPlaytimeTrackingResult_t()
		{
		}

		// Token: 0x040009E6 RID: 2534
		internal Result Result;

		// Token: 0x040009E7 RID: 2535
		public static int _datasize = Marshal.SizeOf(typeof(StopPlaytimeTrackingResult_t));
	}
}
