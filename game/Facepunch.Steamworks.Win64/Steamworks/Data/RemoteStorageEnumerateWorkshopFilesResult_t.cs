using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000123 RID: 291
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t : ICallbackData
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00015DD5 File Offset: 0x00013FD5
		public int DataSize
		{
			get
			{
				return RemoteStorageEnumerateWorkshopFilesResult_t._datasize;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00015DDC File Offset: 0x00013FDC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageEnumerateWorkshopFilesResult;
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00015DE3 File Offset: 0x00013FE3
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageEnumerateWorkshopFilesResult_t()
		{
		}

		// Token: 0x04000914 RID: 2324
		internal Result Result;

		// Token: 0x04000915 RID: 2325
		internal int ResultsReturned;

		// Token: 0x04000916 RID: 2326
		internal int TotalResultCount;

		// Token: 0x04000917 RID: 2327
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId;

		// Token: 0x04000918 RID: 2328
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		internal float[] GScore;

		// Token: 0x04000919 RID: 2329
		internal AppId AppId;

		// Token: 0x0400091A RID: 2330
		internal uint StartIndex;

		// Token: 0x0400091B RID: 2331
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageEnumerateWorkshopFilesResult_t));
	}
}
