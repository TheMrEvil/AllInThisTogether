using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014F RID: 335
	internal struct MusicPlayerWantsPlayNext_t : ICallbackData
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00016462 File Offset: 0x00014662
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsPlayNext_t._datasize;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00016469 File Offset: 0x00014669
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsPlayNext;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00016470 File Offset: 0x00014670
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsPlayNext_t()
		{
		}

		// Token: 0x040009A2 RID: 2466
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsPlayNext_t));
	}
}
