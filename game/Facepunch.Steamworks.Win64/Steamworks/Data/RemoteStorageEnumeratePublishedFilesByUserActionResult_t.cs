using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012C RID: 300
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t : ICallbackData
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00015F19 File Offset: 0x00014119
		public int DataSize
		{
			get
			{
				return RemoteStorageEnumeratePublishedFilesByUserActionResult_t._datasize;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00015F20 File Offset: 0x00014120
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageEnumeratePublishedFilesByUserActionResult;
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00015F27 File Offset: 0x00014127
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageEnumeratePublishedFilesByUserActionResult_t()
		{
		}

		// Token: 0x0400093C RID: 2364
		internal Result Result;

		// Token: 0x0400093D RID: 2365
		internal WorkshopFileAction Action;

		// Token: 0x0400093E RID: 2366
		internal int ResultsReturned;

		// Token: 0x0400093F RID: 2367
		internal int TotalResultCount;

		// Token: 0x04000940 RID: 2368
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId;

		// Token: 0x04000941 RID: 2369
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated;

		// Token: 0x04000942 RID: 2370
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t));
	}
}
