using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000118 RID: 280
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncStatusCheck_t : ICallbackData
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00015B70 File Offset: 0x00013D70
		public int DataSize
		{
			get
			{
				return RemoteStorageAppSyncStatusCheck_t._datasize;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00015B77 File Offset: 0x00013D77
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageAppSyncStatusCheck;
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00015B7E File Offset: 0x00013D7E
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageAppSyncStatusCheck_t()
		{
		}

		// Token: 0x040008D4 RID: 2260
		internal AppId AppID;

		// Token: 0x040008D5 RID: 2261
		internal Result Result;

		// Token: 0x040008D6 RID: 2262
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageAppSyncStatusCheck_t));
	}
}
