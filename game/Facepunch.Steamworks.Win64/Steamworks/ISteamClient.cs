using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200000F RID: 15
	internal class ISteamClient : SteamInterface
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004083 File Offset: 0x00002283
		internal ISteamClient(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000075 RID: 117
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateSteamPipe")]
		private static extern HSteamPipe _CreateSteamPipe(IntPtr self);

		// Token: 0x06000076 RID: 118 RVA: 0x00004098 File Offset: 0x00002298
		internal HSteamPipe CreateSteamPipe()
		{
			return ISteamClient._CreateSteamPipe(this.Self);
		}

		// Token: 0x06000077 RID: 119
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BReleaseSteamPipe")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BReleaseSteamPipe(IntPtr self, HSteamPipe hSteamPipe);

		// Token: 0x06000078 RID: 120 RVA: 0x000040B8 File Offset: 0x000022B8
		internal bool BReleaseSteamPipe(HSteamPipe hSteamPipe)
		{
			return ISteamClient._BReleaseSteamPipe(this.Self, hSteamPipe);
		}

		// Token: 0x06000079 RID: 121
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ConnectToGlobalUser")]
		private static extern HSteamUser _ConnectToGlobalUser(IntPtr self, HSteamPipe hSteamPipe);

		// Token: 0x0600007A RID: 122 RVA: 0x000040D8 File Offset: 0x000022D8
		internal HSteamUser ConnectToGlobalUser(HSteamPipe hSteamPipe)
		{
			return ISteamClient._ConnectToGlobalUser(this.Self, hSteamPipe);
		}

		// Token: 0x0600007B RID: 123
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateLocalUser")]
		private static extern HSteamUser _CreateLocalUser(IntPtr self, ref HSteamPipe phSteamPipe, AccountType eAccountType);

		// Token: 0x0600007C RID: 124 RVA: 0x000040F8 File Offset: 0x000022F8
		internal HSteamUser CreateLocalUser(ref HSteamPipe phSteamPipe, AccountType eAccountType)
		{
			return ISteamClient._CreateLocalUser(this.Self, ref phSteamPipe, eAccountType);
		}

		// Token: 0x0600007D RID: 125
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ReleaseUser")]
		private static extern void _ReleaseUser(IntPtr self, HSteamPipe hSteamPipe, HSteamUser hUser);

		// Token: 0x0600007E RID: 126 RVA: 0x00004119 File Offset: 0x00002319
		internal void ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser)
		{
			ISteamClient._ReleaseUser(this.Self, hSteamPipe, hUser);
		}

		// Token: 0x0600007F RID: 127
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUser")]
		private static extern IntPtr _GetISteamUser(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000080 RID: 128 RVA: 0x0000412C File Offset: 0x0000232C
		internal IntPtr GetISteamUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamUser(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000081 RID: 129
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServer")]
		private static extern IntPtr _GetISteamGameServer(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000082 RID: 130 RVA: 0x00004150 File Offset: 0x00002350
		internal IntPtr GetISteamGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamGameServer(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000083 RID: 131
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetLocalIPBinding")]
		private static extern void _SetLocalIPBinding(IntPtr self, ref SteamIPAddress unIP, ushort usPort);

		// Token: 0x06000084 RID: 132 RVA: 0x00004172 File Offset: 0x00002372
		internal void SetLocalIPBinding(ref SteamIPAddress unIP, ushort usPort)
		{
			ISteamClient._SetLocalIPBinding(this.Self, ref unIP, usPort);
		}

		// Token: 0x06000085 RID: 133
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamFriends")]
		private static extern IntPtr _GetISteamFriends(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000086 RID: 134 RVA: 0x00004184 File Offset: 0x00002384
		internal IntPtr GetISteamFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamFriends(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000087 RID: 135
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUtils")]
		private static extern IntPtr _GetISteamUtils(IntPtr self, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000088 RID: 136 RVA: 0x000041A8 File Offset: 0x000023A8
		internal IntPtr GetISteamUtils(HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamUtils(this.Self, hSteamPipe, pchVersion);
		}

		// Token: 0x06000089 RID: 137
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmaking")]
		private static extern IntPtr _GetISteamMatchmaking(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x0600008A RID: 138 RVA: 0x000041CC File Offset: 0x000023CC
		internal IntPtr GetISteamMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamMatchmaking(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x0600008B RID: 139
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmakingServers")]
		private static extern IntPtr _GetISteamMatchmakingServers(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x0600008C RID: 140 RVA: 0x000041F0 File Offset: 0x000023F0
		internal IntPtr GetISteamMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamMatchmakingServers(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x0600008D RID: 141
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGenericInterface")]
		private static extern IntPtr _GetISteamGenericInterface(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x0600008E RID: 142 RVA: 0x00004214 File Offset: 0x00002414
		internal IntPtr GetISteamGenericInterface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamGenericInterface(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x0600008F RID: 143
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUserStats")]
		private static extern IntPtr _GetISteamUserStats(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000090 RID: 144 RVA: 0x00004238 File Offset: 0x00002438
		internal IntPtr GetISteamUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamUserStats(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000091 RID: 145
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServerStats")]
		private static extern IntPtr _GetISteamGameServerStats(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000092 RID: 146 RVA: 0x0000425C File Offset: 0x0000245C
		internal IntPtr GetISteamGameServerStats(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamGameServerStats(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000093 RID: 147
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamApps")]
		private static extern IntPtr _GetISteamApps(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000094 RID: 148 RVA: 0x00004280 File Offset: 0x00002480
		internal IntPtr GetISteamApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamApps(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000095 RID: 149
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamNetworking")]
		private static extern IntPtr _GetISteamNetworking(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000096 RID: 150 RVA: 0x000042A4 File Offset: 0x000024A4
		internal IntPtr GetISteamNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamNetworking(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000097 RID: 151
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamRemoteStorage")]
		private static extern IntPtr _GetISteamRemoteStorage(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x06000098 RID: 152 RVA: 0x000042C8 File Offset: 0x000024C8
		internal IntPtr GetISteamRemoteStorage(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamRemoteStorage(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x06000099 RID: 153
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamScreenshots")]
		private static extern IntPtr _GetISteamScreenshots(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x0600009A RID: 154 RVA: 0x000042EC File Offset: 0x000024EC
		internal IntPtr GetISteamScreenshots(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamScreenshots(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x0600009B RID: 155
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameSearch")]
		private static extern IntPtr _GetISteamGameSearch(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x0600009C RID: 156 RVA: 0x00004310 File Offset: 0x00002510
		internal IntPtr GetISteamGameSearch(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamGameSearch(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x0600009D RID: 157
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetIPCCallCount")]
		private static extern uint _GetIPCCallCount(IntPtr self);

		// Token: 0x0600009E RID: 158 RVA: 0x00004334 File Offset: 0x00002534
		internal uint GetIPCCallCount()
		{
			return ISteamClient._GetIPCCallCount(this.Self);
		}

		// Token: 0x0600009F RID: 159
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetWarningMessageHook")]
		private static extern void _SetWarningMessageHook(IntPtr self, IntPtr pFunction);

		// Token: 0x060000A0 RID: 160 RVA: 0x00004353 File Offset: 0x00002553
		internal void SetWarningMessageHook(IntPtr pFunction)
		{
			ISteamClient._SetWarningMessageHook(this.Self, pFunction);
		}

		// Token: 0x060000A1 RID: 161
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BShutdownIfAllPipesClosed")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BShutdownIfAllPipesClosed(IntPtr self);

		// Token: 0x060000A2 RID: 162 RVA: 0x00004364 File Offset: 0x00002564
		internal bool BShutdownIfAllPipesClosed()
		{
			return ISteamClient._BShutdownIfAllPipesClosed(this.Self);
		}

		// Token: 0x060000A3 RID: 163
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTTP")]
		private static extern IntPtr _GetISteamHTTP(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000A4 RID: 164 RVA: 0x00004384 File Offset: 0x00002584
		internal IntPtr GetISteamHTTP(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamHTTP(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000A5 RID: 165
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamController")]
		private static extern IntPtr _GetISteamController(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000A6 RID: 166 RVA: 0x000043A8 File Offset: 0x000025A8
		internal IntPtr GetISteamController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamController(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000A7 RID: 167
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUGC")]
		private static extern IntPtr _GetISteamUGC(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000A8 RID: 168 RVA: 0x000043CC File Offset: 0x000025CC
		internal IntPtr GetISteamUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamUGC(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000A9 RID: 169
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamAppList")]
		private static extern IntPtr _GetISteamAppList(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000AA RID: 170 RVA: 0x000043F0 File Offset: 0x000025F0
		internal IntPtr GetISteamAppList(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamAppList(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000AB RID: 171
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMusic")]
		private static extern IntPtr _GetISteamMusic(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000AC RID: 172 RVA: 0x00004414 File Offset: 0x00002614
		internal IntPtr GetISteamMusic(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamMusic(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000AD RID: 173
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMusicRemote")]
		private static extern IntPtr _GetISteamMusicRemote(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000AE RID: 174 RVA: 0x00004438 File Offset: 0x00002638
		internal IntPtr GetISteamMusicRemote(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamMusicRemote(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000AF RID: 175
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTMLSurface")]
		private static extern IntPtr _GetISteamHTMLSurface(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000B0 RID: 176 RVA: 0x0000445C File Offset: 0x0000265C
		internal IntPtr GetISteamHTMLSurface(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamHTMLSurface(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000B1 RID: 177
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamInventory")]
		private static extern IntPtr _GetISteamInventory(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000B2 RID: 178 RVA: 0x00004480 File Offset: 0x00002680
		internal IntPtr GetISteamInventory(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamInventory(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000B3 RID: 179
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamVideo")]
		private static extern IntPtr _GetISteamVideo(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000B4 RID: 180 RVA: 0x000044A4 File Offset: 0x000026A4
		internal IntPtr GetISteamVideo(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamVideo(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000B5 RID: 181
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamParentalSettings")]
		private static extern IntPtr _GetISteamParentalSettings(IntPtr self, HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000B6 RID: 182 RVA: 0x000044C8 File Offset: 0x000026C8
		internal IntPtr GetISteamParentalSettings(HSteamUser hSteamuser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamParentalSettings(this.Self, hSteamuser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000B7 RID: 183
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamInput")]
		private static extern IntPtr _GetISteamInput(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000B8 RID: 184 RVA: 0x000044EC File Offset: 0x000026EC
		internal IntPtr GetISteamInput(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamInput(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000B9 RID: 185
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamParties")]
		private static extern IntPtr _GetISteamParties(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000BA RID: 186 RVA: 0x00004510 File Offset: 0x00002710
		internal IntPtr GetISteamParties(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamParties(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}

		// Token: 0x060000BB RID: 187
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamRemotePlay")]
		private static extern IntPtr _GetISteamRemotePlay(IntPtr self, HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion);

		// Token: 0x060000BC RID: 188 RVA: 0x00004534 File Offset: 0x00002734
		internal IntPtr GetISteamRemotePlay(HSteamUser hSteamUser, HSteamPipe hSteamPipe, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersion)
		{
			return ISteamClient._GetISteamRemotePlay(this.Self, hSteamUser, hSteamPipe, pchVersion);
		}
	}
}
