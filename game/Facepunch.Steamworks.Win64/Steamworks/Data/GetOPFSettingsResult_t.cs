using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018A RID: 394
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOPFSettingsResult_t : ICallbackData
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00016D0B File Offset: 0x00014F0B
		public int DataSize
		{
			get
			{
				return GetOPFSettingsResult_t._datasize;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00016D12 File Offset: 0x00014F12
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GetOPFSettingsResult;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00016D19 File Offset: 0x00014F19
		// Note: this type is marked as 'beforefieldinit'.
		static GetOPFSettingsResult_t()
		{
		}

		// Token: 0x04000A83 RID: 2691
		internal Result Result;

		// Token: 0x04000A84 RID: 2692
		internal AppId VideoAppID;

		// Token: 0x04000A85 RID: 2693
		public static int _datasize = Marshal.SizeOf(typeof(GetOPFSettingsResult_t));
	}
}
