using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014C RID: 332
	internal struct MusicPlayerWantsPlay_t : ICallbackData
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000163F6 File Offset: 0x000145F6
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsPlay_t._datasize;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000163FD File Offset: 0x000145FD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsPlay;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00016404 File Offset: 0x00014604
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsPlay_t()
		{
		}

		// Token: 0x0400099F RID: 2463
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsPlay_t));
	}
}
