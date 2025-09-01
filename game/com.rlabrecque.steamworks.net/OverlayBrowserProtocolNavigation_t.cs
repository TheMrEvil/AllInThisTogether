using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000042 RID: 66
	[CallbackIdentity(349)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct OverlayBrowserProtocolNavigation_t
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0000BEEB File Offset: 0x0000A0EB
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public string rgchURI
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.rgchURI_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.rgchURI_, 1024);
			}
		}

		// Token: 0x0400005D RID: 93
		public const int k_iCallback = 349;

		// Token: 0x0400005E RID: 94
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] rgchURI_;
	}
}
