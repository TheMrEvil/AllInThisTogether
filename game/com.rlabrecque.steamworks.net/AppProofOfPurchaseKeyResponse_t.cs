using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002C RID: 44
	[CallbackIdentity(1021)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000BE71 File Offset: 0x0000A071
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0000BE7E File Offset: 0x0000A07E
		public string m_rgchKey
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchKey_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchKey_, 240);
			}
		}

		// Token: 0x0400000C RID: 12
		public const int k_iCallback = 1021;

		// Token: 0x0400000D RID: 13
		public EResult m_eResult;

		// Token: 0x0400000E RID: 14
		public uint m_nAppID;

		// Token: 0x0400000F RID: 15
		public uint m_cchKeyLength;

		// Token: 0x04000010 RID: 16
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)]
		private byte[] m_rgchKey_;
	}
}
