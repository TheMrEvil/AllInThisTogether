using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A2 RID: 418
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct MatchMakingKeyValuePair
	{
		// Token: 0x06000D6B RID: 3435
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_MatchMakingKeyValuePair_t_Construct")]
		internal static extern void InternalConstruct(ref MatchMakingKeyValuePair self);

		// Token: 0x04000B3C RID: 2876
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Key;

		// Token: 0x04000B3D RID: 2877
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Value;
	}
}
