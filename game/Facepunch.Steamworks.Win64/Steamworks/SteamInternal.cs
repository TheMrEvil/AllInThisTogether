using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000A RID: 10
	internal static class SteamInternal
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002A44 File Offset: 0x00000C44
		internal static bool GameServer_Init(uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersionString)
		{
			return SteamInternal.Native.SteamInternal_GameServer_Init(unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString);
		}

		// Token: 0x02000211 RID: 529
		internal static class Native
		{
			// Token: 0x06001082 RID: 4226
			[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
			[return: MarshalAs(UnmanagedType.I1)]
			public static extern bool SteamInternal_GameServer_Init(uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchVersionString);
		}
	}
}
