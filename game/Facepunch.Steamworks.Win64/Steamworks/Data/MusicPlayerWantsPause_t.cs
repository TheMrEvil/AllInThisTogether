using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014D RID: 333
	internal struct MusicPlayerWantsPause_t : ICallbackData
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0001641A File Offset: 0x0001461A
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsPause_t._datasize;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00016421 File Offset: 0x00014621
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsPause;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00016428 File Offset: 0x00014628
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsPause_t()
		{
		}

		// Token: 0x040009A0 RID: 2464
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsPause_t));
	}
}
