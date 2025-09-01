using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000192 RID: 402
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamRelayNetworkStatus_t : ICallbackData
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x00016E4A File Offset: 0x0001504A
		internal string DebugMsgUTF8()
		{
			return Encoding.UTF8.GetString(this.DebugMsg, 0, Array.IndexOf<byte>(this.DebugMsg, 0));
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00016E69 File Offset: 0x00015069
		public int DataSize
		{
			get
			{
				return SteamRelayNetworkStatus_t._datasize;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00016E70 File Offset: 0x00015070
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamRelayNetworkStatus;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00016E77 File Offset: 0x00015077
		// Note: this type is marked as 'beforefieldinit'.
		static SteamRelayNetworkStatus_t()
		{
		}

		// Token: 0x04000A96 RID: 2710
		internal SteamNetworkingAvailability Avail;

		// Token: 0x04000A97 RID: 2711
		internal int PingMeasurementInProgress;

		// Token: 0x04000A98 RID: 2712
		internal SteamNetworkingAvailability AvailNetworkConfig;

		// Token: 0x04000A99 RID: 2713
		internal SteamNetworkingAvailability AvailAnyRelay;

		// Token: 0x04000A9A RID: 2714
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] DebugMsg;

		// Token: 0x04000A9B RID: 2715
		public static int _datasize = Marshal.SizeOf(typeof(SteamRelayNetworkStatus_t));
	}
}
