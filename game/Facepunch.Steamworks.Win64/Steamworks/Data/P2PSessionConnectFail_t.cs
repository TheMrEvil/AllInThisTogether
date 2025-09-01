using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000143 RID: 323
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct P2PSessionConnectFail_t : ICallbackData
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000162B2 File Offset: 0x000144B2
		public int DataSize
		{
			get
			{
				return P2PSessionConnectFail_t._datasize;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x000162B9 File Offset: 0x000144B9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.P2PSessionConnectFail;
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000162C0 File Offset: 0x000144C0
		// Note: this type is marked as 'beforefieldinit'.
		static P2PSessionConnectFail_t()
		{
		}

		// Token: 0x04000991 RID: 2449
		internal ulong SteamIDRemote;

		// Token: 0x04000992 RID: 2450
		internal byte P2PSessionError;

		// Token: 0x04000993 RID: 2451
		public static int _datasize = Marshal.SizeOf(typeof(P2PSessionConnectFail_t));
	}
}
