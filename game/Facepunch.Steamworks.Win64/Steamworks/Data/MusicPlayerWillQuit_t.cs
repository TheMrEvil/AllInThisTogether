using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014B RID: 331
	internal struct MusicPlayerWillQuit_t : ICallbackData
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000163D2 File Offset: 0x000145D2
		public int DataSize
		{
			get
			{
				return MusicPlayerWillQuit_t._datasize;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x000163D9 File Offset: 0x000145D9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWillQuit;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000163E0 File Offset: 0x000145E0
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWillQuit_t()
		{
		}

		// Token: 0x0400099E RID: 2462
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWillQuit_t));
	}
}
