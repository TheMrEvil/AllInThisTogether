using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000195 RID: 405
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSClientKick_t : ICallbackData
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00016EF4 File Offset: 0x000150F4
		public int DataSize
		{
			get
			{
				return GSClientKick_t._datasize;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00016EFB File Offset: 0x000150FB
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSClientKick;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00016F02 File Offset: 0x00015102
		// Note: this type is marked as 'beforefieldinit'.
		static GSClientKick_t()
		{
		}

		// Token: 0x04000AA3 RID: 2723
		internal ulong SteamID;

		// Token: 0x04000AA4 RID: 2724
		internal DenyReason DenyReason;

		// Token: 0x04000AA5 RID: 2725
		public static int _datasize = Marshal.SizeOf(typeof(GSClientKick_t));
	}
}
