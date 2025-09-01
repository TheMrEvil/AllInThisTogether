using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000125 RID: 293
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileSubscribed_t : ICallbackData
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00015E1D File Offset: 0x0001401D
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishedFileSubscribed_t._datasize;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00015E24 File Offset: 0x00014024
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishedFileSubscribed;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00015E2B File Offset: 0x0001402B
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishedFileSubscribed_t()
		{
		}

		// Token: 0x04000923 RID: 2339
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000924 RID: 2340
		internal AppId AppID;

		// Token: 0x04000925 RID: 2341
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishedFileSubscribed_t));
	}
}
