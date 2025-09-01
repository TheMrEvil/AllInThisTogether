using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020001A4 RID: 420
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamDatagramHostedAddress
	{
		// Token: 0x06000D78 RID: 3448
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamDatagramHostedAddress_Clear")]
		internal static extern void InternalClear(ref SteamDatagramHostedAddress self);

		// Token: 0x06000D79 RID: 3449
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamDatagramHostedAddress_GetPopID")]
		internal static extern SteamNetworkingPOPID InternalGetPopID(ref SteamDatagramHostedAddress self);

		// Token: 0x06000D7A RID: 3450
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamDatagramHostedAddress_SetDevAddress")]
		internal static extern void InternalSetDevAddress(ref SteamDatagramHostedAddress self, uint nIP, ushort nPort, SteamNetworkingPOPID popid);

		// Token: 0x06000D7B RID: 3451 RVA: 0x000174AE File Offset: 0x000156AE
		internal string DataUTF8()
		{
			return Encoding.UTF8.GetString(this.Data, 0, Array.IndexOf<byte>(this.Data, 0));
		}

		// Token: 0x04000B41 RID: 2881
		internal int CbSize;

		// Token: 0x04000B42 RID: 2882
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] Data;
	}
}
