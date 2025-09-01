using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012F RID: 303
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileWriteAsyncComplete_t : ICallbackData
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00015F85 File Offset: 0x00014185
		public int DataSize
		{
			get
			{
				return RemoteStorageFileWriteAsyncComplete_t._datasize;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00015F8C File Offset: 0x0001418C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageFileWriteAsyncComplete;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00015F93 File Offset: 0x00014193
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageFileWriteAsyncComplete_t()
		{
		}

		// Token: 0x0400094A RID: 2378
		internal Result Result;

		// Token: 0x0400094B RID: 2379
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageFileWriteAsyncComplete_t));
	}
}
