using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000077 RID: 119
	[CallbackIdentity(4705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryRequestPricesResult_t
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0000BF4B File Offset: 0x0000A14B
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0000BF58 File Offset: 0x0000A158
		public string m_rgchCurrency
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchCurrency_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchCurrency_, 4);
			}
		}

		// Token: 0x04000135 RID: 309
		public const int k_iCallback = 4705;

		// Token: 0x04000136 RID: 310
		public EResult m_result;

		// Token: 0x04000137 RID: 311
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		private byte[] m_rgchCurrency_;
	}
}
