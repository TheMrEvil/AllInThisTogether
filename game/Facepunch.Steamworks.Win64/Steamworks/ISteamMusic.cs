using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001F RID: 31
	internal class ISteamMusic : SteamInterface
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x000072A2 File Offset: 0x000054A2
		internal ISteamMusic(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060003D5 RID: 981
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamMusic_v001();

		// Token: 0x060003D6 RID: 982 RVA: 0x000072B4 File Offset: 0x000054B4
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamMusic.SteamAPI_SteamMusic_v001();
		}

		// Token: 0x060003D7 RID: 983
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsEnabled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsEnabled(IntPtr self);

		// Token: 0x060003D8 RID: 984 RVA: 0x000072BC File Offset: 0x000054BC
		internal bool BIsEnabled()
		{
			return ISteamMusic._BIsEnabled(this.Self);
		}

		// Token: 0x060003D9 RID: 985
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsPlaying")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsPlaying(IntPtr self);

		// Token: 0x060003DA RID: 986 RVA: 0x000072DC File Offset: 0x000054DC
		internal bool BIsPlaying()
		{
			return ISteamMusic._BIsPlaying(this.Self);
		}

		// Token: 0x060003DB RID: 987
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetPlaybackStatus")]
		private static extern MusicStatus _GetPlaybackStatus(IntPtr self);

		// Token: 0x060003DC RID: 988 RVA: 0x000072FC File Offset: 0x000054FC
		internal MusicStatus GetPlaybackStatus()
		{
			return ISteamMusic._GetPlaybackStatus(this.Self);
		}

		// Token: 0x060003DD RID: 989
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Play")]
		private static extern void _Play(IntPtr self);

		// Token: 0x060003DE RID: 990 RVA: 0x0000731B File Offset: 0x0000551B
		internal void Play()
		{
			ISteamMusic._Play(this.Self);
		}

		// Token: 0x060003DF RID: 991
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Pause")]
		private static extern void _Pause(IntPtr self);

		// Token: 0x060003E0 RID: 992 RVA: 0x0000732A File Offset: 0x0000552A
		internal void Pause()
		{
			ISteamMusic._Pause(this.Self);
		}

		// Token: 0x060003E1 RID: 993
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayPrevious")]
		private static extern void _PlayPrevious(IntPtr self);

		// Token: 0x060003E2 RID: 994 RVA: 0x00007339 File Offset: 0x00005539
		internal void PlayPrevious()
		{
			ISteamMusic._PlayPrevious(this.Self);
		}

		// Token: 0x060003E3 RID: 995
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayNext")]
		private static extern void _PlayNext(IntPtr self);

		// Token: 0x060003E4 RID: 996 RVA: 0x00007348 File Offset: 0x00005548
		internal void PlayNext()
		{
			ISteamMusic._PlayNext(this.Self);
		}

		// Token: 0x060003E5 RID: 997
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_SetVolume")]
		private static extern void _SetVolume(IntPtr self, float flVolume);

		// Token: 0x060003E6 RID: 998 RVA: 0x00007357 File Offset: 0x00005557
		internal void SetVolume(float flVolume)
		{
			ISteamMusic._SetVolume(this.Self, flVolume);
		}

		// Token: 0x060003E7 RID: 999
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetVolume")]
		private static extern float _GetVolume(IntPtr self);

		// Token: 0x060003E8 RID: 1000 RVA: 0x00007368 File Offset: 0x00005568
		internal float GetVolume()
		{
			return ISteamMusic._GetVolume(this.Self);
		}
	}
}
