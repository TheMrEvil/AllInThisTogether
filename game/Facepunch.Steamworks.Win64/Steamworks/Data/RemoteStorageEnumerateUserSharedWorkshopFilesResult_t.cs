using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t : ICallbackData
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00015ED1 File Offset: 0x000140D1
		public int DataSize
		{
			get
			{
				return RemoteStorageEnumerateUserSharedWorkshopFilesResult_t._datasize;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00015ED8 File Offset: 0x000140D8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageEnumerateUserSharedWorkshopFilesResult;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00015EDF File Offset: 0x000140DF
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t()
		{
		}

		// Token: 0x04000933 RID: 2355
		internal Result Result;

		// Token: 0x04000934 RID: 2356
		internal int ResultsReturned;

		// Token: 0x04000935 RID: 2357
		internal int TotalResultCount;

		// Token: 0x04000936 RID: 2358
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId;

		// Token: 0x04000937 RID: 2359
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t));
	}
}
