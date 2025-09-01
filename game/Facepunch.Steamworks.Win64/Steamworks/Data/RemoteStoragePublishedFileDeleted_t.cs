using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000127 RID: 295
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileDeleted_t : ICallbackData
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00015E65 File Offset: 0x00014065
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishedFileDeleted_t._datasize;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00015E6C File Offset: 0x0001406C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishedFileDeleted;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00015E73 File Offset: 0x00014073
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishedFileDeleted_t()
		{
		}

		// Token: 0x04000929 RID: 2345
		internal PublishedFileId PublishedFileId;

		// Token: 0x0400092A RID: 2346
		internal AppId AppID;

		// Token: 0x0400092B RID: 2347
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishedFileDeleted_t));
	}
}
