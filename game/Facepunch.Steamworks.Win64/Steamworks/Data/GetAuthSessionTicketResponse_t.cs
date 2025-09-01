using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAuthSessionTicketResponse_t : ICallbackData
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001526F File Offset: 0x0001346F
		public int DataSize
		{
			get
			{
				return GetAuthSessionTicketResponse_t._datasize;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00015276 File Offset: 0x00013476
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GetAuthSessionTicketResponse;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001527D File Offset: 0x0001347D
		// Note: this type is marked as 'beforefieldinit'.
		static GetAuthSessionTicketResponse_t()
		{
		}

		// Token: 0x040007FB RID: 2043
		internal uint AuthTicket;

		// Token: 0x040007FC RID: 2044
		internal Result Result;

		// Token: 0x040007FD RID: 2045
		public static int _datasize = Marshal.SizeOf(typeof(GetAuthSessionTicketResponse_t));
	}
}
