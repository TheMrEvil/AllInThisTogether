using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018F RID: 399
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamRemotePlaySessionDisconnected_t : ICallbackData
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00016DBF File Offset: 0x00014FBF
		public int DataSize
		{
			get
			{
				return SteamRemotePlaySessionDisconnected_t._datasize;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00016DC6 File Offset: 0x00014FC6
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamRemotePlaySessionDisconnected;
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00016DCD File Offset: 0x00014FCD
		// Note: this type is marked as 'beforefieldinit'.
		static SteamRemotePlaySessionDisconnected_t()
		{
		}

		// Token: 0x04000A8D RID: 2701
		internal uint SessionID;

		// Token: 0x04000A8E RID: 2702
		public static int _datasize = Marshal.SizeOf(typeof(SteamRemotePlaySessionDisconnected_t));
	}
}
