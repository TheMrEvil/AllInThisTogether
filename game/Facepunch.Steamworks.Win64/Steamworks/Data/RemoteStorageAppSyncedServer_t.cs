using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000116 RID: 278
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncedServer_t : ICallbackData
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00015B09 File Offset: 0x00013D09
		public int DataSize
		{
			get
			{
				return RemoteStorageAppSyncedServer_t._datasize;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00015B10 File Offset: 0x00013D10
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageAppSyncedServer;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00015B17 File Offset: 0x00013D17
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageAppSyncedServer_t()
		{
		}

		// Token: 0x040008CA RID: 2250
		internal AppId AppID;

		// Token: 0x040008CB RID: 2251
		internal Result Result;

		// Token: 0x040008CC RID: 2252
		internal int NumUploads;

		// Token: 0x040008CD RID: 2253
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageAppSyncedServer_t));
	}
}
