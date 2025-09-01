using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011F RID: 287
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t : ICallbackData
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00015C8B File Offset: 0x00013E8B
		public int DataSize
		{
			get
			{
				return RemoteStorageUnsubscribePublishedFileResult_t._datasize;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00015C92 File Offset: 0x00013E92
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageUnsubscribePublishedFileResult;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00015C99 File Offset: 0x00013E99
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageUnsubscribePublishedFileResult_t()
		{
		}

		// Token: 0x040008F0 RID: 2288
		internal Result Result;

		// Token: 0x040008F1 RID: 2289
		internal PublishedFileId PublishedFileId;

		// Token: 0x040008F2 RID: 2290
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageUnsubscribePublishedFileResult_t));
	}
}
