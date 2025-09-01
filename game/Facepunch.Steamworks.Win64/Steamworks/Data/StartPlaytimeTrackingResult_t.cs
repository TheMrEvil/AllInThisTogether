using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000162 RID: 354
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartPlaytimeTrackingResult_t : ICallbackData
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0001672D File Offset: 0x0001492D
		public int DataSize
		{
			get
			{
				return StartPlaytimeTrackingResult_t._datasize;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00016734 File Offset: 0x00014934
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.StartPlaytimeTrackingResult;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001673B File Offset: 0x0001493B
		// Note: this type is marked as 'beforefieldinit'.
		static StartPlaytimeTrackingResult_t()
		{
		}

		// Token: 0x040009E4 RID: 2532
		internal Result Result;

		// Token: 0x040009E5 RID: 2533
		public static int _datasize = Marshal.SizeOf(typeof(StartPlaytimeTrackingResult_t));
	}
}
