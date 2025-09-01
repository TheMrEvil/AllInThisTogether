using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000153 RID: 339
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerSelectsQueueEntry_t : ICallbackData
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000164F2 File Offset: 0x000146F2
		public int DataSize
		{
			get
			{
				return MusicPlayerSelectsQueueEntry_t._datasize;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000164F9 File Offset: 0x000146F9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerSelectsQueueEntry;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00016500 File Offset: 0x00014700
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerSelectsQueueEntry_t()
		{
		}

		// Token: 0x040009A9 RID: 2473
		internal int NID;

		// Token: 0x040009AA RID: 2474
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerSelectsQueueEntry_t));
	}
}
