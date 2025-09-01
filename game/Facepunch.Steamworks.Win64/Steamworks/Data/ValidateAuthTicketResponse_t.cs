using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000DB RID: 219
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ValidateAuthTicketResponse_t : ICallbackData
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00015203 File Offset: 0x00013403
		public int DataSize
		{
			get
			{
				return ValidateAuthTicketResponse_t._datasize;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001520A File Offset: 0x0001340A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ValidateAuthTicketResponse;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00015211 File Offset: 0x00013411
		// Note: this type is marked as 'beforefieldinit'.
		static ValidateAuthTicketResponse_t()
		{
		}

		// Token: 0x040007F1 RID: 2033
		internal ulong SteamID;

		// Token: 0x040007F2 RID: 2034
		internal AuthResponse AuthSessionResponse;

		// Token: 0x040007F3 RID: 2035
		internal ulong OwnerSteamID;

		// Token: 0x040007F4 RID: 2036
		public static int _datasize = Marshal.SizeOf(typeof(ValidateAuthTicketResponse_t));
	}
}
