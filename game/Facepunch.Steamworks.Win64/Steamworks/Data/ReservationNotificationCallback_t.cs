using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000111 RID: 273
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ReservationNotificationCallback_t : ICallbackData
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00015A55 File Offset: 0x00013C55
		public int DataSize
		{
			get
			{
				return ReservationNotificationCallback_t._datasize;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00015A5C File Offset: 0x00013C5C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ReservationNotificationCallback;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00015A63 File Offset: 0x00013C63
		// Note: this type is marked as 'beforefieldinit'.
		static ReservationNotificationCallback_t()
		{
		}

		// Token: 0x040008BF RID: 2239
		internal ulong BeaconID;

		// Token: 0x040008C0 RID: 2240
		internal ulong SteamIDJoiner;

		// Token: 0x040008C1 RID: 2241
		public static int _datasize = Marshal.SizeOf(typeof(ReservationNotificationCallback_t));
	}
}
