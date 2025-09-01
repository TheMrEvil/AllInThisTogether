using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000150 RID: 336
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsShuffled_t : ICallbackData
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00016486 File Offset: 0x00014686
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsShuffled_t._datasize;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0001648D File Offset: 0x0001468D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsShuffled;
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00016494 File Offset: 0x00014694
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsShuffled_t()
		{
		}

		// Token: 0x040009A3 RID: 2467
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled;

		// Token: 0x040009A4 RID: 2468
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsShuffled_t));
	}
}
