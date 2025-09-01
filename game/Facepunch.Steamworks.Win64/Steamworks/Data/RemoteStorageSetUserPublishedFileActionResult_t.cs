using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012B RID: 299
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t : ICallbackData
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00015EF5 File Offset: 0x000140F5
		public int DataSize
		{
			get
			{
				return RemoteStorageSetUserPublishedFileActionResult_t._datasize;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00015EFC File Offset: 0x000140FC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageSetUserPublishedFileActionResult;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00015F03 File Offset: 0x00014103
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageSetUserPublishedFileActionResult_t()
		{
		}

		// Token: 0x04000938 RID: 2360
		internal Result Result;

		// Token: 0x04000939 RID: 2361
		internal PublishedFileId PublishedFileId;

		// Token: 0x0400093A RID: 2362
		internal WorkshopFileAction Action;

		// Token: 0x0400093B RID: 2363
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageSetUserPublishedFileActionResult_t));
	}
}
