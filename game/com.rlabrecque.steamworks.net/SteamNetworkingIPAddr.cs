using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIPAddr : IEquatable<SteamNetworkingIPAddr>
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0000F904 File Offset: 0x0000DB04
		public bool IsIPv6AllZeros()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public void SetIPv6(byte[] ipv6, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000F916 File Offset: 0x0000DB16
		public void SetIPv4(uint nIP, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000F920 File Offset: 0x0000DB20
		public bool IsIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000F928 File Offset: 0x0000DB28
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000F930 File Offset: 0x0000DB30
		public void SetIPv6LocalHost(ushort nPort = 0)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000F939 File Offset: 0x0000DB39
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000F944 File Offset: 0x0000DB44
		public void ToString(out string buf, bool bWithPort)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(48);
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, intPtr, 48U, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000F974 File Offset: 0x0000DB74
		public bool ParseString(string pszStr)
		{
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				result = NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		public bool Equals(SteamNetworkingIPAddr x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000F9BA File Offset: 0x0000DBBA
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000F9C2 File Offset: 0x0000DBC2
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x04000AD7 RID: 2775
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] m_ipv6;

		// Token: 0x04000AD8 RID: 2776
		public ushort m_port;

		// Token: 0x04000AD9 RID: 2777
		public const int k_cchMaxString = 48;
	}
}
