using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018C RID: 396
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BroadcastUploadStop_t : ICallbackData
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00016D53 File Offset: 0x00014F53
		public int DataSize
		{
			get
			{
				return BroadcastUploadStop_t._datasize;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00016D5A File Offset: 0x00014F5A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.BroadcastUploadStop;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00016D61 File Offset: 0x00014F61
		// Note: this type is marked as 'beforefieldinit'.
		static BroadcastUploadStop_t()
		{
		}

		// Token: 0x04000A88 RID: 2696
		internal BroadcastUploadResult Result;

		// Token: 0x04000A89 RID: 2697
		public static int _datasize = Marshal.SizeOf(typeof(BroadcastUploadStop_t));
	}
}
