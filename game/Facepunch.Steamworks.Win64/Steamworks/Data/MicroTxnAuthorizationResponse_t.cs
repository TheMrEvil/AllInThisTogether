using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MicroTxnAuthorizationResponse_t : ICallbackData
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00015227 File Offset: 0x00013427
		public int DataSize
		{
			get
			{
				return MicroTxnAuthorizationResponse_t._datasize;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001522E File Offset: 0x0001342E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MicroTxnAuthorizationResponse;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00015235 File Offset: 0x00013435
		// Note: this type is marked as 'beforefieldinit'.
		static MicroTxnAuthorizationResponse_t()
		{
		}

		// Token: 0x040007F5 RID: 2037
		internal uint AppID;

		// Token: 0x040007F6 RID: 2038
		internal ulong OrderID;

		// Token: 0x040007F7 RID: 2039
		internal byte Authorized;

		// Token: 0x040007F8 RID: 2040
		public static int _datasize = Marshal.SizeOf(typeof(MicroTxnAuthorizationResponse_t));
	}
}
