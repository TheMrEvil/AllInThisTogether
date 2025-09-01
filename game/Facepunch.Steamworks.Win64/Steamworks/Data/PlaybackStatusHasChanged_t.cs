using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000146 RID: 326
	internal struct PlaybackStatusHasChanged_t : ICallbackData
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0001631E File Offset: 0x0001451E
		public int DataSize
		{
			get
			{
				return PlaybackStatusHasChanged_t._datasize;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00016325 File Offset: 0x00014525
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.PlaybackStatusHasChanged;
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0001632C File Offset: 0x0001452C
		// Note: this type is marked as 'beforefieldinit'.
		static PlaybackStatusHasChanged_t()
		{
		}

		// Token: 0x04000998 RID: 2456
		public static int _datasize = Marshal.SizeOf(typeof(PlaybackStatusHasChanged_t));
	}
}
