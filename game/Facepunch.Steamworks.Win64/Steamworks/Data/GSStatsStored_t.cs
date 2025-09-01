using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019E RID: 414
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSStatsStored_t : ICallbackData
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00017054 File Offset: 0x00015254
		public int DataSize
		{
			get
			{
				return GSStatsStored_t._datasize;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0001705B File Offset: 0x0001525B
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSStatsStored;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00017062 File Offset: 0x00015262
		// Note: this type is marked as 'beforefieldinit'.
		static GSStatsStored_t()
		{
		}

		// Token: 0x04000AC9 RID: 2761
		internal Result Result;

		// Token: 0x04000ACA RID: 2762
		internal ulong SteamIDUser;

		// Token: 0x04000ACB RID: 2763
		public static int _datasize = Marshal.SizeOf(typeof(GSStatsStored_t));
	}
}
