using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015F RID: 351
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserFavoriteItemsListChanged_t : ICallbackData
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000166C1 File Offset: 0x000148C1
		public int DataSize
		{
			get
			{
				return UserFavoriteItemsListChanged_t._datasize;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000166C8 File Offset: 0x000148C8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UserFavoriteItemsListChanged;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000166CF File Offset: 0x000148CF
		// Note: this type is marked as 'beforefieldinit'.
		static UserFavoriteItemsListChanged_t()
		{
		}

		// Token: 0x040009D6 RID: 2518
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009D7 RID: 2519
		internal Result Result;

		// Token: 0x040009D8 RID: 2520
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest;

		// Token: 0x040009D9 RID: 2521
		public static int _datasize = Marshal.SizeOf(typeof(UserFavoriteItemsListChanged_t));
	}
}
