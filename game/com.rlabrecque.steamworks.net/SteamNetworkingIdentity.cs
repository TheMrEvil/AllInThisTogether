using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0000F738 File Offset: 0x0000D938
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000F740 File Offset: 0x0000D940
		public bool IsInvalid()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000F748 File Offset: 0x0000D948
		public void SetSteamID(CSteamID steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000F756 File Offset: 0x0000D956
		public CSteamID GetSteamID()
		{
			return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000F763 File Offset: 0x0000D963
		public void SetSteamID64(ulong steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000F76C File Offset: 0x0000D96C
		public ulong GetSteamID64()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000F774 File Offset: 0x0000D974
		public bool SetXboxPairwiseID(string pszString)
		{
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				result = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public string GetXboxPairwiseID()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000F7BD File Offset: 0x0000D9BD
		public void SetPSNID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0000F7C6 File Offset: 0x0000D9C6
		public ulong GetPSNID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000F7CE File Offset: 0x0000D9CE
		public void SetStadiaID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetStadiaID(ref this, id);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000F7D7 File Offset: 0x0000D9D7
		public ulong GetStadiaID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetStadiaID(ref this);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000F7DF File Offset: 0x0000D9DF
		public void SetIPAddr(SteamNetworkingIPAddr addr)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000F7EA File Offset: 0x0000D9EA
		public SteamNetworkingIPAddr GetIPAddr()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000F7F1 File Offset: 0x0000D9F1
		public void SetIPv4Addr(uint nIPv4, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000F803 File Offset: 0x0000DA03
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000F80B File Offset: 0x0000DA0B
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000F816 File Offset: 0x0000DA16
		public void SetLocalHost()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000F81E File Offset: 0x0000DA1E
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000F828 File Offset: 0x0000DA28
		public bool SetGenericString(string pszString)
		{
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				result = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000F864 File Offset: 0x0000DA64
		public string GetGenericString()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000F871 File Offset: 0x0000DA71
		public bool SetGenericBytes(byte[] data, uint cbLen)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0000F87B File Offset: 0x0000DA7B
		public byte[] GetGenericBytes(out int cbLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000F882 File Offset: 0x0000DA82
		public bool Equals(SteamNetworkingIdentity x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public void ToString(out string buf)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(128);
			NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, intPtr, 128U);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		public bool ParseString(string pszStr)
		{
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				result = NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x04000AD0 RID: 2768
		public ESteamNetworkingIdentityType m_eType;

		// Token: 0x04000AD1 RID: 2769
		private int m_cbSize;

		// Token: 0x04000AD2 RID: 2770
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private uint[] m_reserved;

		// Token: 0x04000AD3 RID: 2771
		public const int k_cchMaxString = 128;

		// Token: 0x04000AD4 RID: 2772
		public const int k_cchMaxGenericString = 32;

		// Token: 0x04000AD5 RID: 2773
		public const int k_cchMaxXboxPairwiseID = 33;

		// Token: 0x04000AD6 RID: 2774
		public const int k_cbMaxGenericBytes = 32;
	}
}
