using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A5 RID: 421
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct SteamIPAddress
	{
		// Token: 0x06000D7C RID: 3452
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamIPAddress_t_IsSet")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsSet(ref SteamIPAddress self);

		// Token: 0x06000D7D RID: 3453 RVA: 0x000174D0 File Offset: 0x000156D0
		public static implicit operator IPAddress(SteamIPAddress value)
		{
			bool flag = value.Type == SteamIPType.Type4;
			if (flag)
			{
				return Utility.Int32ToIp(value.Ip4Address);
			}
			throw new Exception(string.Format("Oops - can't convert SteamIPAddress to System.Net.IPAddress because no-one coded support for {0} yet", value.Type));
		}

		// Token: 0x04000B43 RID: 2883
		[FieldOffset(0)]
		public uint Ip4Address;

		// Token: 0x04000B44 RID: 2884
		[FieldOffset(16)]
		internal SteamIPType Type;
	}
}
