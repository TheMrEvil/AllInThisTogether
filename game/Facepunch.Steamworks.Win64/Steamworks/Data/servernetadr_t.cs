using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct servernetadr_t
	{
		// Token: 0x06000D6C RID: 3436
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_Construct")]
		internal static extern void InternalConstruct(ref servernetadr_t self);

		// Token: 0x06000D6D RID: 3437
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_Init")]
		internal static extern void InternalInit(ref servernetadr_t self, uint ip, ushort usQueryPort, ushort usConnectionPort);

		// Token: 0x06000D6E RID: 3438
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_GetQueryPort")]
		internal static extern ushort InternalGetQueryPort(ref servernetadr_t self);

		// Token: 0x06000D6F RID: 3439
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_SetQueryPort")]
		internal static extern void InternalSetQueryPort(ref servernetadr_t self, ushort usPort);

		// Token: 0x06000D70 RID: 3440
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_GetConnectionPort")]
		internal static extern ushort InternalGetConnectionPort(ref servernetadr_t self);

		// Token: 0x06000D71 RID: 3441
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_SetConnectionPort")]
		internal static extern void InternalSetConnectionPort(ref servernetadr_t self, ushort usPort);

		// Token: 0x06000D72 RID: 3442
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_GetIP")]
		internal static extern uint InternalGetIP(ref servernetadr_t self);

		// Token: 0x06000D73 RID: 3443
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_SetIP")]
		internal static extern void InternalSetIP(ref servernetadr_t self, uint unIP);

		// Token: 0x06000D74 RID: 3444
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_GetConnectionAddressString")]
		internal static extern Utf8StringPointer InternalGetConnectionAddressString(ref servernetadr_t self);

		// Token: 0x06000D75 RID: 3445
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_GetQueryAddressString")]
		internal static extern Utf8StringPointer InternalGetQueryAddressString(ref servernetadr_t self);

		// Token: 0x06000D76 RID: 3446
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_IsLessThan")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsLessThan(ref servernetadr_t self, ref servernetadr_t netadr);

		// Token: 0x06000D77 RID: 3447
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_servernetadr_t_Assign")]
		internal static extern void InternalAssign(ref servernetadr_t self, ref servernetadr_t that);

		// Token: 0x04000B3E RID: 2878
		internal ushort ConnectionPort;

		// Token: 0x04000B3F RID: 2879
		internal ushort QueryPort;

		// Token: 0x04000B40 RID: 2880
		internal uint IP;
	}
}
