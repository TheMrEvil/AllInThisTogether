using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000198 RID: 408
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSGameplayStats_t : ICallbackData
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00016F7C File Offset: 0x0001517C
		public int DataSize
		{
			get
			{
				return GSGameplayStats_t._datasize;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00016F83 File Offset: 0x00015183
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSGameplayStats;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00016F8A File Offset: 0x0001518A
		// Note: this type is marked as 'beforefieldinit'.
		static GSGameplayStats_t()
		{
		}

		// Token: 0x04000AAC RID: 2732
		internal Result Result;

		// Token: 0x04000AAD RID: 2733
		internal int Rank;

		// Token: 0x04000AAE RID: 2734
		internal uint TotalConnects;

		// Token: 0x04000AAF RID: 2735
		internal uint TotalMinutesPlayed;

		// Token: 0x04000AB0 RID: 2736
		public static int _datasize = Marshal.SizeOf(typeof(GSGameplayStats_t));
	}
}
