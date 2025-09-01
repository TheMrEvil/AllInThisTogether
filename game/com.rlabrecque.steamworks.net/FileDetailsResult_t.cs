using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002D RID: 45
	[CallbackIdentity(1023)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FileDetailsResult_t
	{
		// Token: 0x04000011 RID: 17
		public const int k_iCallback = 1023;

		// Token: 0x04000012 RID: 18
		public EResult m_eResult;

		// Token: 0x04000013 RID: 19
		public ulong m_ulFileSize;

		// Token: 0x04000014 RID: 20
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] m_FileSHA;

		// Token: 0x04000015 RID: 21
		public uint m_unFlags;
	}
}
