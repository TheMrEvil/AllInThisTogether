using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000115 RID: 277
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncedClient_t : ICallbackData
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00015AE5 File Offset: 0x00013CE5
		public int DataSize
		{
			get
			{
				return RemoteStorageAppSyncedClient_t._datasize;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00015AEC File Offset: 0x00013CEC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageAppSyncedClient;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00015AF3 File Offset: 0x00013CF3
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageAppSyncedClient_t()
		{
		}

		// Token: 0x040008C6 RID: 2246
		internal AppId AppID;

		// Token: 0x040008C7 RID: 2247
		internal Result Result;

		// Token: 0x040008C8 RID: 2248
		internal int NumDownloads;

		// Token: 0x040008C9 RID: 2249
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageAppSyncedClient_t));
	}
}
