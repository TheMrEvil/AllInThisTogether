using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000126 RID: 294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileUnsubscribed_t : ICallbackData
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00015E41 File Offset: 0x00014041
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishedFileUnsubscribed_t._datasize;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00015E48 File Offset: 0x00014048
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishedFileUnsubscribed;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00015E4F File Offset: 0x0001404F
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishedFileUnsubscribed_t()
		{
		}

		// Token: 0x04000926 RID: 2342
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000927 RID: 2343
		internal AppId AppID;

		// Token: 0x04000928 RID: 2344
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishedFileUnsubscribed_t));
	}
}
