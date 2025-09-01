using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001C RID: 28
	internal class ISteamMatchmakingRulesResponse : SteamInterface
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x00007004 File Offset: 0x00005204
		internal ISteamMatchmakingRulesResponse(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060003A2 RID: 930
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded")]
		private static extern void _RulesResponded(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchRule, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x060003A3 RID: 931 RVA: 0x00007016 File Offset: 0x00005216
		internal void RulesResponded([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchRule, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			ISteamMatchmakingRulesResponse._RulesResponded(this.Self, pchRule, pchValue);
		}

		// Token: 0x060003A4 RID: 932
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond")]
		private static extern void _RulesFailedToRespond(IntPtr self);

		// Token: 0x060003A5 RID: 933 RVA: 0x00007027 File Offset: 0x00005227
		internal void RulesFailedToRespond()
		{
			ISteamMatchmakingRulesResponse._RulesFailedToRespond(this.Self);
		}

		// Token: 0x060003A6 RID: 934
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete")]
		private static extern void _RulesRefreshComplete(IntPtr self);

		// Token: 0x060003A7 RID: 935 RVA: 0x00007036 File Offset: 0x00005236
		internal void RulesRefreshComplete()
		{
			ISteamMatchmakingRulesResponse._RulesRefreshComplete(this.Self);
		}
	}
}
