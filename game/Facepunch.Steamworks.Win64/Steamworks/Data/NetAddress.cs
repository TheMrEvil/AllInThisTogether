using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A7 RID: 423
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 18)]
	public struct NetAddress
	{
		// Token: 0x06000D9B RID: 3483
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_Clear")]
		internal static extern void InternalClear(ref NetAddress self);

		// Token: 0x06000D9C RID: 3484
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsIPv6AllZeros(ref NetAddress self);

		// Token: 0x06000D9D RID: 3485
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_SetIPv6")]
		internal static extern void InternalSetIPv6(ref NetAddress self, ref byte ipv6, ushort nPort);

		// Token: 0x06000D9E RID: 3486
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_SetIPv4")]
		internal static extern void InternalSetIPv4(ref NetAddress self, uint nIP, ushort nPort);

		// Token: 0x06000D9F RID: 3487
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_IsIPv4")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsIPv4(ref NetAddress self);

		// Token: 0x06000DA0 RID: 3488
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_GetIPv4")]
		internal static extern uint InternalGetIPv4(ref NetAddress self);

		// Token: 0x06000DA1 RID: 3489
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost")]
		internal static extern void InternalSetIPv6LocalHost(ref NetAddress self, ushort nPort);

		// Token: 0x06000DA2 RID: 3490
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_IsLocalHost")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsLocalHost(ref NetAddress self);

		// Token: 0x06000DA3 RID: 3491
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_ToString")]
		internal static extern void InternalToString(ref NetAddress self, IntPtr buf, uint cbBuf, [MarshalAs(UnmanagedType.U1)] bool bWithPort);

		// Token: 0x06000DA4 RID: 3492
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_ParseString")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalParseString(ref NetAddress self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszStr);

		// Token: 0x06000DA5 RID: 3493
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingIPAddr_IsEqualTo")]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool InternalIsEqualTo(ref NetAddress self, ref NetAddress x);

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00017691 File Offset: 0x00015891
		public ushort Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001769C File Offset: 0x0001589C
		public static NetAddress AnyIp(ushort port)
		{
			NetAddress cleared = NetAddress.Cleared;
			cleared.port = port;
			return cleared;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000176C0 File Offset: 0x000158C0
		public static NetAddress LocalHost(ushort port)
		{
			NetAddress cleared = NetAddress.Cleared;
			NetAddress.InternalSetIPv6LocalHost(ref cleared, port);
			return cleared;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000176E4 File Offset: 0x000158E4
		public static NetAddress From(string addrStr, ushort port)
		{
			return NetAddress.From(IPAddress.Parse(addrStr), port);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00017704 File Offset: 0x00015904
		public static NetAddress From(IPAddress address, ushort port)
		{
			byte[] addressBytes = address.GetAddressBytes();
			bool flag = address.AddressFamily == AddressFamily.InterNetwork;
			if (flag)
			{
				NetAddress cleared = NetAddress.Cleared;
				NetAddress.InternalSetIPv4(ref cleared, address.IpToInt32(), port);
				return cleared;
			}
			throw new NotImplementedException("Oops - no IPV6 support yet?");
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0001774C File Offset: 0x0001594C
		public static NetAddress Cleared
		{
			get
			{
				NetAddress result = default(NetAddress);
				NetAddress.InternalClear(ref result);
				return result;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00017770 File Offset: 0x00015970
		public bool IsIPv6AllZeros
		{
			get
			{
				NetAddress netAddress = this;
				return NetAddress.InternalIsIPv6AllZeros(ref netAddress);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00017790 File Offset: 0x00015990
		public bool IsIPv4
		{
			get
			{
				NetAddress netAddress = this;
				return NetAddress.InternalIsIPv4(ref netAddress);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000177B0 File Offset: 0x000159B0
		public bool IsLocalHost
		{
			get
			{
				NetAddress netAddress = this;
				return NetAddress.InternalIsLocalHost(ref netAddress);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000177D0 File Offset: 0x000159D0
		public IPAddress Address
		{
			get
			{
				bool isIPv = this.IsIPv4;
				if (isIPv)
				{
					NetAddress netAddress = this;
					uint ipAddress = NetAddress.InternalGetIPv4(ref netAddress);
					return Utility.Int32ToIp(ipAddress);
				}
				throw new NotImplementedException("Oops - no IPV6 support yet?");
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00017810 File Offset: 0x00015A10
		public override string ToString()
		{
			IntPtr intPtr = Helpers.TakeMemory();
			NetAddress netAddress = this;
			NetAddress.InternalToString(ref netAddress, intPtr, 32768U, true);
			return Helpers.MemoryToString(intPtr);
		}

		// Token: 0x04000B49 RID: 2889
		[FieldOffset(0)]
		internal NetAddress.IPV4 ip;

		// Token: 0x04000B4A RID: 2890
		[FieldOffset(16)]
		internal ushort port;

		// Token: 0x02000283 RID: 643
		internal struct IPV4
		{
			// Token: 0x04000F03 RID: 3843
			internal ulong m_8zeros;

			// Token: 0x04000F04 RID: 3844
			internal ushort m_0000;

			// Token: 0x04000F05 RID: 3845
			internal ushort m_ffff;

			// Token: 0x04000F06 RID: 3846
			internal byte ip0;

			// Token: 0x04000F07 RID: 3847
			internal byte ip1;

			// Token: 0x04000F08 RID: 3848
			internal byte ip2;

			// Token: 0x04000F09 RID: 3849
			internal byte ip3;
		}
	}
}
