using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000148 RID: 328
	internal struct MusicPlayerRemoteWillActivate_t : ICallbackData
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00016366 File Offset: 0x00014566
		public int DataSize
		{
			get
			{
				return MusicPlayerRemoteWillActivate_t._datasize;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001636D File Offset: 0x0001456D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerRemoteWillActivate;
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00016374 File Offset: 0x00014574
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerRemoteWillActivate_t()
		{
		}

		// Token: 0x0400099B RID: 2459
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerRemoteWillActivate_t));
	}
}
