using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015A RID: 346
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCRequestUGCDetailsResult_t : ICallbackData
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001660D File Offset: 0x0001480D
		public int DataSize
		{
			get
			{
				return SteamUGCRequestUGCDetailsResult_t._datasize;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00016614 File Offset: 0x00014814
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamUGCRequestUGCDetailsResult;
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0001661B File Offset: 0x0001481B
		// Note: this type is marked as 'beforefieldinit'.
		static SteamUGCRequestUGCDetailsResult_t()
		{
		}

		// Token: 0x040009C4 RID: 2500
		internal SteamUGCDetails_t Details;

		// Token: 0x040009C5 RID: 2501
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x040009C6 RID: 2502
		public static int _datasize = Marshal.SizeOf(typeof(SteamUGCRequestUGCDetailsResult_t));
	}
}
