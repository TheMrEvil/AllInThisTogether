using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012E RID: 302
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileUpdated_t : ICallbackData
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00015F61 File Offset: 0x00014161
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishedFileUpdated_t._datasize;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00015F68 File Offset: 0x00014168
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishedFileUpdated;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00015F6F File Offset: 0x0001416F
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishedFileUpdated_t()
		{
		}

		// Token: 0x04000946 RID: 2374
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000947 RID: 2375
		internal AppId AppID;

		// Token: 0x04000948 RID: 2376
		internal ulong Unused;

		// Token: 0x04000949 RID: 2377
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishedFileUpdated_t));
	}
}
