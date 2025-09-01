using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A6 RID: 422
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 136)]
	public struct NetIdentity
	{
		// Token: 0x06000D7E RID: 3454
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_Clear")]
		internal static extern void InternalClear(ref NetIdentity self);

		// Token: 0x06000D7F RID: 3455
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_IsInvalid")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsInvalid(ref NetIdentity self);

		// Token: 0x06000D80 RID: 3456
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetSteamID")]
		internal static extern void InternalSetSteamID(ref NetIdentity self, SteamId steamID);

		// Token: 0x06000D81 RID: 3457
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetSteamID")]
		internal static extern SteamId InternalGetSteamID(ref NetIdentity self);

		// Token: 0x06000D82 RID: 3458
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetSteamID64")]
		internal static extern void InternalSetSteamID64(ref NetIdentity self, ulong steamID);

		// Token: 0x06000D83 RID: 3459
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetSteamID64")]
		internal static extern ulong InternalGetSteamID64(ref NetIdentity self);

		// Token: 0x06000D84 RID: 3460
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalSetXboxPairwiseID(ref NetIdentity self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszString);

		// Token: 0x06000D85 RID: 3461
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID")]
		internal static extern Utf8StringPointer InternalGetXboxPairwiseID(ref NetIdentity self);

		// Token: 0x06000D86 RID: 3462
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetIPAddr")]
		internal static extern void InternalSetIPAddr(ref NetIdentity self, ref NetAddress addr);

		// Token: 0x06000D87 RID: 3463
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetIPAddr")]
		internal static extern IntPtr InternalGetIPAddr(ref NetIdentity self);

		// Token: 0x06000D88 RID: 3464
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetLocalHost")]
		internal static extern void InternalSetLocalHost(ref NetIdentity self);

		// Token: 0x06000D89 RID: 3465
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_IsLocalHost")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsLocalHost(ref NetIdentity self);

		// Token: 0x06000D8A RID: 3466
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetGenericString")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalSetGenericString(ref NetIdentity self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszString);

		// Token: 0x06000D8B RID: 3467
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetGenericString")]
		internal static extern Utf8StringPointer InternalGetGenericString(ref NetIdentity self);

		// Token: 0x06000D8C RID: 3468
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_SetGenericBytes")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalSetGenericBytes(ref NetIdentity self, IntPtr data, uint cbLen);

		// Token: 0x06000D8D RID: 3469
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_GetGenericBytes")]
		internal static extern byte InternalGetGenericBytes(ref NetIdentity self, ref int cbLen);

		// Token: 0x06000D8E RID: 3470
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_IsEqualTo")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsEqualTo(ref NetIdentity self, ref NetIdentity x);

		// Token: 0x06000D8F RID: 3471
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_ToString")]
		internal static extern void InternalToString(ref NetIdentity self, IntPtr buf, uint cbBuf);

		// Token: 0x06000D90 RID: 3472
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIdentity_ParseString")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalParseString(ref NetIdentity self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr);

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00017518 File Offset: 0x00015718
		public static NetIdentity LocalHost
		{
			get
			{
				NetIdentity result = default(NetIdentity);
				NetIdentity.InternalSetLocalHost(ref result);
				return result;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0001753B File Offset: 0x0001573B
		public bool IsSteamId
		{
			get
			{
				return this.type == NetIdentity.IdentityType.SteamID;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00017547 File Offset: 0x00015747
		public bool IsIpAddress
		{
			get
			{
				return this.type == NetIdentity.IdentityType.IPAddress;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00017554 File Offset: 0x00015754
		public bool IsLocalHost
		{
			get
			{
				NetIdentity netIdentity = default(NetIdentity);
				return NetIdentity.InternalIsLocalHost(ref netIdentity);
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00017578 File Offset: 0x00015778
		public static implicit operator NetIdentity(SteamId value)
		{
			NetIdentity result = default(NetIdentity);
			NetIdentity.InternalSetSteamID(ref result, value);
			return result;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001759C File Offset: 0x0001579C
		public static implicit operator NetIdentity(NetAddress address)
		{
			NetIdentity result = default(NetIdentity);
			NetIdentity.InternalSetIPAddr(ref result, ref address);
			return result;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000175C4 File Offset: 0x000157C4
		public static implicit operator SteamId(NetIdentity value)
		{
			return value.SteamId;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000175E0 File Offset: 0x000157E0
		public SteamId SteamId
		{
			get
			{
				bool flag = this.type != NetIdentity.IdentityType.SteamID;
				SteamId result;
				if (flag)
				{
					result = default(SteamId);
				}
				else
				{
					NetIdentity netIdentity = this;
					result = NetIdentity.InternalGetSteamID(ref netIdentity);
				}
				return result;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00017620 File Offset: 0x00015820
		public NetAddress Address
		{
			get
			{
				bool flag = this.type != NetIdentity.IdentityType.IPAddress;
				NetAddress result;
				if (flag)
				{
					result = default(NetAddress);
				}
				else
				{
					NetIdentity netIdentity = this;
					IntPtr ptr = NetIdentity.InternalGetIPAddr(ref netIdentity);
					result = ptr.ToType<NetAddress>();
				}
				return result;
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00017668 File Offset: 0x00015868
		public override string ToString()
		{
			NetIdentity netIdentity = this;
			string result;
			SteamNetworkingUtils.Internal.SteamNetworkingIdentity_ToString(ref netIdentity, out result);
			return result;
		}

		// Token: 0x04000B45 RID: 2885
		[FieldOffset(0)]
		internal NetIdentity.IdentityType type;

		// Token: 0x04000B46 RID: 2886
		[FieldOffset(4)]
		internal int size;

		// Token: 0x04000B47 RID: 2887
		[FieldOffset(8)]
		internal ulong steamid;

		// Token: 0x04000B48 RID: 2888
		[FieldOffset(8)]
		internal NetAddress netaddress;

		// Token: 0x02000282 RID: 642
		internal enum IdentityType
		{
			// Token: 0x04000EFE RID: 3838
			Invalid,
			// Token: 0x04000EFF RID: 3839
			IPAddress,
			// Token: 0x04000F00 RID: 3840
			GenericString,
			// Token: 0x04000F01 RID: 3841
			GenericBytes,
			// Token: 0x04000F02 RID: 3842
			SteamID = 16
		}
	}
}
