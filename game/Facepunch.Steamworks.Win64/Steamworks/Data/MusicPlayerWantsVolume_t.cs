using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000152 RID: 338
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsVolume_t : ICallbackData
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x000164CE File Offset: 0x000146CE
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsVolume_t._datasize;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000164D5 File Offset: 0x000146D5
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsVolume;
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000164DC File Offset: 0x000146DC
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsVolume_t()
		{
		}

		// Token: 0x040009A7 RID: 2471
		internal float NewVolume;

		// Token: 0x040009A8 RID: 2472
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsVolume_t));
	}
}
