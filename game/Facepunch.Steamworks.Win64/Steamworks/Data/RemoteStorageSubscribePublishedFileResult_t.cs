using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011D RID: 285
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageSubscribePublishedFileResult_t : ICallbackData
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00015C43 File Offset: 0x00013E43
		public int DataSize
		{
			get
			{
				return RemoteStorageSubscribePublishedFileResult_t._datasize;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00015C4A File Offset: 0x00013E4A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageSubscribePublishedFileResult;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00015C51 File Offset: 0x00013E51
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageSubscribePublishedFileResult_t()
		{
		}

		// Token: 0x040008E7 RID: 2279
		internal Result Result;

		// Token: 0x040008E8 RID: 2280
		internal PublishedFileId PublishedFileId;

		// Token: 0x040008E9 RID: 2281
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageSubscribePublishedFileResult_t));
	}
}
