using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct gameserveritem_t
	{
		// Token: 0x06000D63 RID: 3427
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_gameserveritem_t_Construct")]
		internal static extern void InternalConstruct(ref gameserveritem_t self);

		// Token: 0x06000D64 RID: 3428
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_gameserveritem_t_GetName")]
		internal static extern Utf8StringPointer InternalGetName(ref gameserveritem_t self);

		// Token: 0x06000D65 RID: 3429
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_gameserveritem_t_SetName")]
		internal static extern void InternalSetName(ref gameserveritem_t self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pName);

		// Token: 0x06000D66 RID: 3430 RVA: 0x00017413 File Offset: 0x00015613
		internal string GameDirUTF8()
		{
			return Encoding.UTF8.GetString(this.GameDir, 0, Array.IndexOf<byte>(this.GameDir, 0));
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00017432 File Offset: 0x00015632
		internal string MapUTF8()
		{
			return Encoding.UTF8.GetString(this.Map, 0, Array.IndexOf<byte>(this.Map, 0));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00017451 File Offset: 0x00015651
		internal string GameDescriptionUTF8()
		{
			return Encoding.UTF8.GetString(this.GameDescription, 0, Array.IndexOf<byte>(this.GameDescription, 0));
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00017470 File Offset: 0x00015670
		internal string ServerNameUTF8()
		{
			return Encoding.UTF8.GetString(this.ServerName, 0, Array.IndexOf<byte>(this.ServerName, 0));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001748F File Offset: 0x0001568F
		internal string GameTagsUTF8()
		{
			return Encoding.UTF8.GetString(this.GameTags, 0, Array.IndexOf<byte>(this.GameTags, 0));
		}

		// Token: 0x04000B2A RID: 2858
		internal servernetadr_t NetAdr;

		// Token: 0x04000B2B RID: 2859
		internal int Ping;

		// Token: 0x04000B2C RID: 2860
		[MarshalAs(UnmanagedType.I1)]
		internal bool HadSuccessfulResponse;

		// Token: 0x04000B2D RID: 2861
		[MarshalAs(UnmanagedType.I1)]
		internal bool DoNotRefresh;

		// Token: 0x04000B2E RID: 2862
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] GameDir;

		// Token: 0x04000B2F RID: 2863
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] Map;

		// Token: 0x04000B30 RID: 2864
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] GameDescription;

		// Token: 0x04000B31 RID: 2865
		internal uint AppID;

		// Token: 0x04000B32 RID: 2866
		internal int Players;

		// Token: 0x04000B33 RID: 2867
		internal int MaxPlayers;

		// Token: 0x04000B34 RID: 2868
		internal int BotPlayers;

		// Token: 0x04000B35 RID: 2869
		[MarshalAs(UnmanagedType.I1)]
		internal bool Password;

		// Token: 0x04000B36 RID: 2870
		[MarshalAs(UnmanagedType.I1)]
		internal bool Secure;

		// Token: 0x04000B37 RID: 2871
		internal uint TimeLastPlayed;

		// Token: 0x04000B38 RID: 2872
		internal int ServerVersion;

		// Token: 0x04000B39 RID: 2873
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] ServerName;

		// Token: 0x04000B3A RID: 2874
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] GameTags;

		// Token: 0x04000B3B RID: 2875
		internal ulong SteamID;
	}
}
