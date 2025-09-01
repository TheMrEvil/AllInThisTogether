using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClanOfficerListResponse_t : ICallbackData
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00015453 File Offset: 0x00013653
		public int DataSize
		{
			get
			{
				return ClanOfficerListResponse_t._datasize;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001545A File Offset: 0x0001365A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ClanOfficerListResponse;
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00015461 File Offset: 0x00013661
		// Note: this type is marked as 'beforefieldinit'.
		static ClanOfficerListResponse_t()
		{
		}

		// Token: 0x04000821 RID: 2081
		internal ulong SteamIDClan;

		// Token: 0x04000822 RID: 2082
		internal int COfficers;

		// Token: 0x04000823 RID: 2083
		internal byte Success;

		// Token: 0x04000824 RID: 2084
		public static int _datasize = Marshal.SizeOf(typeof(ClanOfficerListResponse_t));
	}
}
