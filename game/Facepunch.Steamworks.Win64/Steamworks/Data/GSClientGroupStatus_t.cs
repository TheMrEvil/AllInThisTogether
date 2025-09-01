using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000199 RID: 409
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientGroupStatus_t : ICallbackData
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00016FA0 File Offset: 0x000151A0
		public int DataSize
		{
			get
			{
				return GSClientGroupStatus_t._datasize;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00016FA7 File Offset: 0x000151A7
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSClientGroupStatus;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00016FAE File Offset: 0x000151AE
		// Note: this type is marked as 'beforefieldinit'.
		static GSClientGroupStatus_t()
		{
		}

		// Token: 0x04000AB1 RID: 2737
		internal ulong SteamIDUser;

		// Token: 0x04000AB2 RID: 2738
		internal ulong SteamIDGroup;

		// Token: 0x04000AB3 RID: 2739
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member;

		// Token: 0x04000AB4 RID: 2740
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer;

		// Token: 0x04000AB5 RID: 2741
		public static int _datasize = Marshal.SizeOf(typeof(GSClientGroupStatus_t));
	}
}
