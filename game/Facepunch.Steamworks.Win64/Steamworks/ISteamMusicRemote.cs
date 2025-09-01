using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000020 RID: 32
	internal class ISteamMusicRemote : SteamInterface
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x00007387 File Offset: 0x00005587
		internal ISteamMusicRemote(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060003EA RID: 1002
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamMusicRemote_v001();

		// Token: 0x060003EB RID: 1003 RVA: 0x00007399 File Offset: 0x00005599
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamMusicRemote.SteamAPI_SteamMusicRemote_v001();
		}

		// Token: 0x060003EC RID: 1004
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RegisterSteamMusicRemote(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName);

		// Token: 0x060003ED RID: 1005 RVA: 0x000073A0 File Offset: 0x000055A0
		internal bool RegisterSteamMusicRemote([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName)
		{
			return ISteamMusicRemote._RegisterSteamMusicRemote(this.Self, pchName);
		}

		// Token: 0x060003EE RID: 1006
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DeregisterSteamMusicRemote(IntPtr self);

		// Token: 0x060003EF RID: 1007 RVA: 0x000073C0 File Offset: 0x000055C0
		internal bool DeregisterSteamMusicRemote()
		{
			return ISteamMusicRemote._DeregisterSteamMusicRemote(this.Self);
		}

		// Token: 0x060003F0 RID: 1008
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsCurrentMusicRemote(IntPtr self);

		// Token: 0x060003F1 RID: 1009 RVA: 0x000073E0 File Offset: 0x000055E0
		internal bool BIsCurrentMusicRemote()
		{
			return ISteamMusicRemote._BIsCurrentMusicRemote(this.Self);
		}

		// Token: 0x060003F2 RID: 1010
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_BActivationSuccess")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BActivationSuccess(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x060003F3 RID: 1011 RVA: 0x00007400 File Offset: 0x00005600
		internal bool BActivationSuccess([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._BActivationSuccess(this.Self, bValue);
		}

		// Token: 0x060003F4 RID: 1012
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetDisplayName")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetDisplayName(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDisplayName);

		// Token: 0x060003F5 RID: 1013 RVA: 0x00007420 File Offset: 0x00005620
		internal bool SetDisplayName([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDisplayName)
		{
			return ISteamMusicRemote._SetDisplayName(this.Self, pchDisplayName);
		}

		// Token: 0x060003F6 RID: 1014
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetPNGIcon_64x64(IntPtr self, IntPtr pvBuffer, uint cbBufferLength);

		// Token: 0x060003F7 RID: 1015 RVA: 0x00007440 File Offset: 0x00005640
		internal bool SetPNGIcon_64x64(IntPtr pvBuffer, uint cbBufferLength)
		{
			return ISteamMusicRemote._SetPNGIcon_64x64(this.Self, pvBuffer, cbBufferLength);
		}

		// Token: 0x060003F8 RID: 1016
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayPrevious")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnablePlayPrevious(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x060003F9 RID: 1017 RVA: 0x00007464 File Offset: 0x00005664
		internal bool EnablePlayPrevious([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnablePlayPrevious(this.Self, bValue);
		}

		// Token: 0x060003FA RID: 1018
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayNext")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnablePlayNext(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x060003FB RID: 1019 RVA: 0x00007484 File Offset: 0x00005684
		internal bool EnablePlayNext([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnablePlayNext(this.Self, bValue);
		}

		// Token: 0x060003FC RID: 1020
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableShuffled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnableShuffled(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x060003FD RID: 1021 RVA: 0x000074A4 File Offset: 0x000056A4
		internal bool EnableShuffled([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnableShuffled(this.Self, bValue);
		}

		// Token: 0x060003FE RID: 1022
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableLooped")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnableLooped(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x060003FF RID: 1023 RVA: 0x000074C4 File Offset: 0x000056C4
		internal bool EnableLooped([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnableLooped(this.Self, bValue);
		}

		// Token: 0x06000400 RID: 1024
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableQueue")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnableQueue(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x06000401 RID: 1025 RVA: 0x000074E4 File Offset: 0x000056E4
		internal bool EnableQueue([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnableQueue(this.Self, bValue);
		}

		// Token: 0x06000402 RID: 1026
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlaylists")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _EnablePlaylists(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x06000403 RID: 1027 RVA: 0x00007504 File Offset: 0x00005704
		internal bool EnablePlaylists([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._EnablePlaylists(this.Self, bValue);
		}

		// Token: 0x06000404 RID: 1028
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdatePlaybackStatus(IntPtr self, MusicStatus nStatus);

		// Token: 0x06000405 RID: 1029 RVA: 0x00007524 File Offset: 0x00005724
		internal bool UpdatePlaybackStatus(MusicStatus nStatus)
		{
			return ISteamMusicRemote._UpdatePlaybackStatus(this.Self, nStatus);
		}

		// Token: 0x06000406 RID: 1030
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateShuffled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateShuffled(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x06000407 RID: 1031 RVA: 0x00007544 File Offset: 0x00005744
		internal bool UpdateShuffled([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._UpdateShuffled(this.Self, bValue);
		}

		// Token: 0x06000408 RID: 1032
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateLooped")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateLooped(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x06000409 RID: 1033 RVA: 0x00007564 File Offset: 0x00005764
		internal bool UpdateLooped([MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamMusicRemote._UpdateLooped(this.Self, bValue);
		}

		// Token: 0x0600040A RID: 1034
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateVolume")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateVolume(IntPtr self, float flValue);

		// Token: 0x0600040B RID: 1035 RVA: 0x00007584 File Offset: 0x00005784
		internal bool UpdateVolume(float flValue)
		{
			return ISteamMusicRemote._UpdateVolume(this.Self, flValue);
		}

		// Token: 0x0600040C RID: 1036
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryWillChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CurrentEntryWillChange(IntPtr self);

		// Token: 0x0600040D RID: 1037 RVA: 0x000075A4 File Offset: 0x000057A4
		internal bool CurrentEntryWillChange()
		{
			return ISteamMusicRemote._CurrentEntryWillChange(this.Self);
		}

		// Token: 0x0600040E RID: 1038
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CurrentEntryIsAvailable(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bAvailable);

		// Token: 0x0600040F RID: 1039 RVA: 0x000075C4 File Offset: 0x000057C4
		internal bool CurrentEntryIsAvailable([MarshalAs(UnmanagedType.U1)] bool bAvailable)
		{
			return ISteamMusicRemote._CurrentEntryIsAvailable(this.Self, bAvailable);
		}

		// Token: 0x06000410 RID: 1040
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateCurrentEntryText(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchText);

		// Token: 0x06000411 RID: 1041 RVA: 0x000075E4 File Offset: 0x000057E4
		internal bool UpdateCurrentEntryText([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchText)
		{
			return ISteamMusicRemote._UpdateCurrentEntryText(this.Self, pchText);
		}

		// Token: 0x06000412 RID: 1042
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateCurrentEntryElapsedSeconds(IntPtr self, int nValue);

		// Token: 0x06000413 RID: 1043 RVA: 0x00007604 File Offset: 0x00005804
		internal bool UpdateCurrentEntryElapsedSeconds(int nValue)
		{
			return ISteamMusicRemote._UpdateCurrentEntryElapsedSeconds(this.Self, nValue);
		}

		// Token: 0x06000414 RID: 1044
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateCurrentEntryCoverArt(IntPtr self, IntPtr pvBuffer, uint cbBufferLength);

		// Token: 0x06000415 RID: 1045 RVA: 0x00007624 File Offset: 0x00005824
		internal bool UpdateCurrentEntryCoverArt(IntPtr pvBuffer, uint cbBufferLength)
		{
			return ISteamMusicRemote._UpdateCurrentEntryCoverArt(this.Self, pvBuffer, cbBufferLength);
		}

		// Token: 0x06000416 RID: 1046
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryDidChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CurrentEntryDidChange(IntPtr self);

		// Token: 0x06000417 RID: 1047 RVA: 0x00007648 File Offset: 0x00005848
		internal bool CurrentEntryDidChange()
		{
			return ISteamMusicRemote._CurrentEntryDidChange(this.Self);
		}

		// Token: 0x06000418 RID: 1048
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueWillChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _QueueWillChange(IntPtr self);

		// Token: 0x06000419 RID: 1049 RVA: 0x00007668 File Offset: 0x00005868
		internal bool QueueWillChange()
		{
			return ISteamMusicRemote._QueueWillChange(this.Self);
		}

		// Token: 0x0600041A RID: 1050
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetQueueEntries")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ResetQueueEntries(IntPtr self);

		// Token: 0x0600041B RID: 1051 RVA: 0x00007688 File Offset: 0x00005888
		internal bool ResetQueueEntries()
		{
			return ISteamMusicRemote._ResetQueueEntries(this.Self);
		}

		// Token: 0x0600041C RID: 1052
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetQueueEntry")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetQueueEntry(IntPtr self, int nID, int nPosition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchEntryText);

		// Token: 0x0600041D RID: 1053 RVA: 0x000076A8 File Offset: 0x000058A8
		internal bool SetQueueEntry(int nID, int nPosition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchEntryText)
		{
			return ISteamMusicRemote._SetQueueEntry(this.Self, nID, nPosition, pchEntryText);
		}

		// Token: 0x0600041E RID: 1054
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetCurrentQueueEntry(IntPtr self, int nID);

		// Token: 0x0600041F RID: 1055 RVA: 0x000076CC File Offset: 0x000058CC
		internal bool SetCurrentQueueEntry(int nID)
		{
			return ISteamMusicRemote._SetCurrentQueueEntry(this.Self, nID);
		}

		// Token: 0x06000420 RID: 1056
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueDidChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _QueueDidChange(IntPtr self);

		// Token: 0x06000421 RID: 1057 RVA: 0x000076EC File Offset: 0x000058EC
		internal bool QueueDidChange()
		{
			return ISteamMusicRemote._QueueDidChange(this.Self);
		}

		// Token: 0x06000422 RID: 1058
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistWillChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _PlaylistWillChange(IntPtr self);

		// Token: 0x06000423 RID: 1059 RVA: 0x0000770C File Offset: 0x0000590C
		internal bool PlaylistWillChange()
		{
			return ISteamMusicRemote._PlaylistWillChange(this.Self);
		}

		// Token: 0x06000424 RID: 1060
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetPlaylistEntries")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ResetPlaylistEntries(IntPtr self);

		// Token: 0x06000425 RID: 1061 RVA: 0x0000772C File Offset: 0x0000592C
		internal bool ResetPlaylistEntries()
		{
			return ISteamMusicRemote._ResetPlaylistEntries(this.Self);
		}

		// Token: 0x06000426 RID: 1062
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPlaylistEntry")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetPlaylistEntry(IntPtr self, int nID, int nPosition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchEntryText);

		// Token: 0x06000427 RID: 1063 RVA: 0x0000774C File Offset: 0x0000594C
		internal bool SetPlaylistEntry(int nID, int nPosition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchEntryText)
		{
			return ISteamMusicRemote._SetPlaylistEntry(this.Self, nID, nPosition, pchEntryText);
		}

		// Token: 0x06000428 RID: 1064
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetCurrentPlaylistEntry(IntPtr self, int nID);

		// Token: 0x06000429 RID: 1065 RVA: 0x00007770 File Offset: 0x00005970
		internal bool SetCurrentPlaylistEntry(int nID)
		{
			return ISteamMusicRemote._SetCurrentPlaylistEntry(this.Self, nID);
		}

		// Token: 0x0600042A RID: 1066
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistDidChange")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _PlaylistDidChange(IntPtr self);

		// Token: 0x0600042B RID: 1067 RVA: 0x00007790 File Offset: 0x00005990
		internal bool PlaylistDidChange()
		{
			return ISteamMusicRemote._PlaylistDidChange(this.Self);
		}
	}
}
