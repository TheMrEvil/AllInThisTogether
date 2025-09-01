using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000D6 RID: 214
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamServerConnectFailure_t : ICallbackData
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0001515E File Offset: 0x0001335E
		public int DataSize
		{
			get
			{
				return SteamServerConnectFailure_t._datasize;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00015165 File Offset: 0x00013365
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamServerConnectFailure;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00015169 File Offset: 0x00013369
		// Note: this type is marked as 'beforefieldinit'.
		static SteamServerConnectFailure_t()
		{
		}

		// Token: 0x040007E3 RID: 2019
		internal Result Result;

		// Token: 0x040007E4 RID: 2020
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying;

		// Token: 0x040007E5 RID: 2021
		public static int _datasize = Marshal.SizeOf(typeof(SteamServerConnectFailure_t));
	}
}
