using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000193 RID: 403
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientApprove_t : ICallbackData
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00016E8D File Offset: 0x0001508D
		public int DataSize
		{
			get
			{
				return GSClientApprove_t._datasize;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00016E94 File Offset: 0x00015094
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSClientApprove;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00016E9B File Offset: 0x0001509B
		// Note: this type is marked as 'beforefieldinit'.
		static GSClientApprove_t()
		{
		}

		// Token: 0x04000A9C RID: 2716
		internal ulong SteamID;

		// Token: 0x04000A9D RID: 2717
		internal ulong OwnerSteamID;

		// Token: 0x04000A9E RID: 2718
		public static int _datasize = Marshal.SizeOf(typeof(GSClientApprove_t));
	}
}
