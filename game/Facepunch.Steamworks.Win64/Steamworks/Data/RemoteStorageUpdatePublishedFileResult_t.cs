using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000120 RID: 288
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUpdatePublishedFileResult_t : ICallbackData
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00015CAF File Offset: 0x00013EAF
		public int DataSize
		{
			get
			{
				return RemoteStorageUpdatePublishedFileResult_t._datasize;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00015CB6 File Offset: 0x00013EB6
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageUpdatePublishedFileResult;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00015CBD File Offset: 0x00013EBD
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageUpdatePublishedFileResult_t()
		{
		}

		// Token: 0x040008F3 RID: 2291
		internal Result Result;

		// Token: 0x040008F4 RID: 2292
		internal PublishedFileId PublishedFileId;

		// Token: 0x040008F5 RID: 2293
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x040008F6 RID: 2294
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageUpdatePublishedFileResult_t));
	}
}
