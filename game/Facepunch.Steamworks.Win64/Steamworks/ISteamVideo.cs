using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000030 RID: 48
	internal class ISteamVideo : SteamInterface
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		internal ISteamVideo(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000706 RID: 1798
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamVideo_v002();

		// Token: 0x06000707 RID: 1799 RVA: 0x0000A8B6 File Offset: 0x00008AB6
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamVideo.SteamAPI_SteamVideo_v002();
		}

		// Token: 0x06000708 RID: 1800
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetVideoURL")]
		private static extern void _GetVideoURL(IntPtr self, AppId unVideoAppID);

		// Token: 0x06000709 RID: 1801 RVA: 0x0000A8BD File Offset: 0x00008ABD
		internal void GetVideoURL(AppId unVideoAppID)
		{
			ISteamVideo._GetVideoURL(this.Self, unVideoAppID);
		}

		// Token: 0x0600070A RID: 1802
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_IsBroadcasting")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsBroadcasting(IntPtr self, ref int pnNumViewers);

		// Token: 0x0600070B RID: 1803 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		internal bool IsBroadcasting(ref int pnNumViewers)
		{
			return ISteamVideo._IsBroadcasting(this.Self, ref pnNumViewers);
		}

		// Token: 0x0600070C RID: 1804
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetOPFSettings")]
		private static extern void _GetOPFSettings(IntPtr self, AppId unVideoAppID);

		// Token: 0x0600070D RID: 1805 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		internal void GetOPFSettings(AppId unVideoAppID)
		{
			ISteamVideo._GetOPFSettings(this.Self, unVideoAppID);
		}

		// Token: 0x0600070E RID: 1806
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetOPFStringForApp")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetOPFStringForApp(IntPtr self, AppId unVideoAppID, IntPtr pchBuffer, ref int pnBufferSize);

		// Token: 0x0600070F RID: 1807 RVA: 0x0000A900 File Offset: 0x00008B00
		internal bool GetOPFStringForApp(AppId unVideoAppID, out string pchBuffer, ref int pnBufferSize)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamVideo._GetOPFStringForApp(this.Self, unVideoAppID, intPtr, ref pnBufferSize);
			pchBuffer = Helpers.MemoryToString(intPtr);
			return result;
		}
	}
}
