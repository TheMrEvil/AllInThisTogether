using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011B RID: 283
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageDeletePublishedFileResult_t : ICallbackData
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00015BFB File Offset: 0x00013DFB
		public int DataSize
		{
			get
			{
				return RemoteStorageDeletePublishedFileResult_t._datasize;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00015C02 File Offset: 0x00013E02
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageDeletePublishedFileResult;
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00015C09 File Offset: 0x00013E09
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageDeletePublishedFileResult_t()
		{
		}

		// Token: 0x040008DF RID: 2271
		internal Result Result;

		// Token: 0x040008E0 RID: 2272
		internal PublishedFileId PublishedFileId;

		// Token: 0x040008E1 RID: 2273
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageDeletePublishedFileResult_t));
	}
}
