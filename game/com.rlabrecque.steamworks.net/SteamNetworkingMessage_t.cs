using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public struct SteamNetworkingMessage_t
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000F9CD File Offset: 0x0000DBCD
		public void Release()
		{
			throw new NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
		public static void Release(IntPtr pointer)
		{
			NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
		public static SteamNetworkingMessage_t FromIntPtr(IntPtr pointer)
		{
			return (SteamNetworkingMessage_t)Marshal.PtrToStructure(pointer, typeof(SteamNetworkingMessage_t));
		}

		// Token: 0x04000ADA RID: 2778
		public IntPtr m_pData;

		// Token: 0x04000ADB RID: 2779
		public int m_cbSize;

		// Token: 0x04000ADC RID: 2780
		public HSteamNetConnection m_conn;

		// Token: 0x04000ADD RID: 2781
		public SteamNetworkingIdentity m_identityPeer;

		// Token: 0x04000ADE RID: 2782
		public long m_nConnUserData;

		// Token: 0x04000ADF RID: 2783
		public SteamNetworkingMicroseconds m_usecTimeReceived;

		// Token: 0x04000AE0 RID: 2784
		public long m_nMessageNumber;

		// Token: 0x04000AE1 RID: 2785
		public IntPtr m_pfnFreeData;

		// Token: 0x04000AE2 RID: 2786
		internal IntPtr m_pfnRelease;

		// Token: 0x04000AE3 RID: 2787
		public int m_nChannel;

		// Token: 0x04000AE4 RID: 2788
		public int m_nFlags;

		// Token: 0x04000AE5 RID: 2789
		public long m_nUserData;

		// Token: 0x04000AE6 RID: 2790
		public ushort m_idxLane;

		// Token: 0x04000AE7 RID: 2791
		public ushort _pad1__;
	}
}
