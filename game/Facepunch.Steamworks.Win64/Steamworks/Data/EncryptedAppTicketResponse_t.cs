using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000DD RID: 221
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EncryptedAppTicketResponse_t : ICallbackData
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001524B File Offset: 0x0001344B
		public int DataSize
		{
			get
			{
				return EncryptedAppTicketResponse_t._datasize;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00015252 File Offset: 0x00013452
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.EncryptedAppTicketResponse;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00015259 File Offset: 0x00013459
		// Note: this type is marked as 'beforefieldinit'.
		static EncryptedAppTicketResponse_t()
		{
		}

		// Token: 0x040007F9 RID: 2041
		internal Result Result;

		// Token: 0x040007FA RID: 2042
		public static int _datasize = Marshal.SizeOf(typeof(EncryptedAppTicketResponse_t));
	}
}
