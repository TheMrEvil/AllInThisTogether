using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramHostedAddress
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public void Clear()
		{
			this.m_cbSize = 0;
			this.m_data = new byte[128];
		}

		// Token: 0x04000A9E RID: 2718
		public int m_cbSize;

		// Token: 0x04000A9F RID: 2719
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] m_data;
	}
}
