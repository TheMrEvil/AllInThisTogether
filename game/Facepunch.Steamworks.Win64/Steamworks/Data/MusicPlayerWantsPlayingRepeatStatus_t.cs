using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000155 RID: 341
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t : ICallbackData
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0001653A File Offset: 0x0001473A
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsPlayingRepeatStatus_t._datasize;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00016541 File Offset: 0x00014741
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsPlayingRepeatStatus;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00016548 File Offset: 0x00014748
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsPlayingRepeatStatus_t()
		{
		}

		// Token: 0x040009AD RID: 2477
		internal int PlayingRepeatStatus;

		// Token: 0x040009AE RID: 2478
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsPlayingRepeatStatus_t));
	}
}
