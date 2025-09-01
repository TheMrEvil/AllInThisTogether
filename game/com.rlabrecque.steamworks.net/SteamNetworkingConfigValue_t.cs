using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		// Token: 0x04000ACC RID: 2764
		public ESteamNetworkingConfigValue m_eValue;

		// Token: 0x04000ACD RID: 2765
		public ESteamNetworkingConfigDataType m_eDataType;

		// Token: 0x04000ACE RID: 2766
		public SteamNetworkingConfigValue_t.OptionValue m_val;

		// Token: 0x020001F2 RID: 498
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000B2F RID: 2863
			[FieldOffset(0)]
			public int m_int32;

			// Token: 0x04000B30 RID: 2864
			[FieldOffset(0)]
			public long m_int64;

			// Token: 0x04000B31 RID: 2865
			[FieldOffset(0)]
			public float m_float;

			// Token: 0x04000B32 RID: 2866
			[FieldOffset(0)]
			public IntPtr m_string;

			// Token: 0x04000B33 RID: 2867
			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	}
}
