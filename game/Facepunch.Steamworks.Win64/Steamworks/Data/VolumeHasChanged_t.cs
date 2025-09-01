using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000147 RID: 327
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VolumeHasChanged_t : ICallbackData
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00016342 File Offset: 0x00014542
		public int DataSize
		{
			get
			{
				return VolumeHasChanged_t._datasize;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00016349 File Offset: 0x00014549
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.VolumeHasChanged;
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00016350 File Offset: 0x00014550
		// Note: this type is marked as 'beforefieldinit'.
		static VolumeHasChanged_t()
		{
		}

		// Token: 0x04000999 RID: 2457
		internal float NewVolume;

		// Token: 0x0400099A RID: 2458
		public static int _datasize = Marshal.SizeOf(typeof(VolumeHasChanged_t));
	}
}
