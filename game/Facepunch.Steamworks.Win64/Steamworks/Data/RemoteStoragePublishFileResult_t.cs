using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishFileResult_t : ICallbackData
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00015BD7 File Offset: 0x00013DD7
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishFileResult_t._datasize;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00015BDE File Offset: 0x00013DDE
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishFileResult;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00015BE5 File Offset: 0x00013DE5
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishFileResult_t()
		{
		}

		// Token: 0x040008DB RID: 2267
		internal Result Result;

		// Token: 0x040008DC RID: 2268
		internal PublishedFileId PublishedFileId;

		// Token: 0x040008DD RID: 2269
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x040008DE RID: 2270
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishFileResult_t));
	}
}
