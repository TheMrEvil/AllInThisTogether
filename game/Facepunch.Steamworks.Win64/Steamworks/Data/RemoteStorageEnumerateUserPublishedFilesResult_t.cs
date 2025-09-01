using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011C RID: 284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t : ICallbackData
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00015C1F File Offset: 0x00013E1F
		public int DataSize
		{
			get
			{
				return RemoteStorageEnumerateUserPublishedFilesResult_t._datasize;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00015C26 File Offset: 0x00013E26
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageEnumerateUserPublishedFilesResult;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00015C2D File Offset: 0x00013E2D
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageEnumerateUserPublishedFilesResult_t()
		{
		}

		// Token: 0x040008E2 RID: 2274
		internal Result Result;

		// Token: 0x040008E3 RID: 2275
		internal int ResultsReturned;

		// Token: 0x040008E4 RID: 2276
		internal int TotalResultCount;

		// Token: 0x040008E5 RID: 2277
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId;

		// Token: 0x040008E6 RID: 2278
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserPublishedFilesResult_t));
	}
}
