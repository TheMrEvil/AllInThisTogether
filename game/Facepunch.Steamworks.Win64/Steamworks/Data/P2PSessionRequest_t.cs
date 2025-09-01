using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000142 RID: 322
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct P2PSessionRequest_t : ICallbackData
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001628E File Offset: 0x0001448E
		public int DataSize
		{
			get
			{
				return P2PSessionRequest_t._datasize;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00016295 File Offset: 0x00014495
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.P2PSessionRequest;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0001629C File Offset: 0x0001449C
		// Note: this type is marked as 'beforefieldinit'.
		static P2PSessionRequest_t()
		{
		}

		// Token: 0x0400098F RID: 2447
		internal ulong SteamIDRemote;

		// Token: 0x04000990 RID: 2448
		public static int _datasize = Marshal.SizeOf(typeof(P2PSessionRequest_t));
	}
}
