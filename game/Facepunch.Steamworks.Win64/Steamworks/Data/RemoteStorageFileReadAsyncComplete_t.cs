using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000130 RID: 304
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileReadAsyncComplete_t : ICallbackData
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00015FA9 File Offset: 0x000141A9
		public int DataSize
		{
			get
			{
				return RemoteStorageFileReadAsyncComplete_t._datasize;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00015FB0 File Offset: 0x000141B0
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageFileReadAsyncComplete;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00015FB7 File Offset: 0x000141B7
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageFileReadAsyncComplete_t()
		{
		}

		// Token: 0x0400094C RID: 2380
		internal ulong FileReadAsync;

		// Token: 0x0400094D RID: 2381
		internal Result Result;

		// Token: 0x0400094E RID: 2382
		internal uint Offset;

		// Token: 0x0400094F RID: 2383
		internal uint Read;

		// Token: 0x04000950 RID: 2384
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageFileReadAsyncComplete_t));
	}
}
