using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014E RID: 334
	internal struct MusicPlayerWantsPlayPrevious_t : ICallbackData
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0001643E File Offset: 0x0001463E
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsPlayPrevious_t._datasize;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00016445 File Offset: 0x00014645
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsPlayPrevious;
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0001644C File Offset: 0x0001464C
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsPlayPrevious_t()
		{
		}

		// Token: 0x040009A1 RID: 2465
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsPlayPrevious_t));
	}
}
