using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000107 RID: 263
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FavoritesListAccountsUpdated_t : ICallbackData
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000158CE File Offset: 0x00013ACE
		public int DataSize
		{
			get
			{
				return FavoritesListAccountsUpdated_t._datasize;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000158D5 File Offset: 0x00013AD5
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FavoritesListAccountsUpdated;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000158DC File Offset: 0x00013ADC
		// Note: this type is marked as 'beforefieldinit'.
		static FavoritesListAccountsUpdated_t()
		{
		}

		// Token: 0x0400088E RID: 2190
		internal Result Result;

		// Token: 0x0400088F RID: 2191
		public static int _datasize = Marshal.SizeOf(typeof(FavoritesListAccountsUpdated_t));
	}
}
