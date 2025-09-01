using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015E RID: 350
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DownloadItemResult_t : ICallbackData
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0001669D File Offset: 0x0001489D
		public int DataSize
		{
			get
			{
				return DownloadItemResult_t._datasize;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000166A4 File Offset: 0x000148A4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.DownloadItemResult;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000166AB File Offset: 0x000148AB
		// Note: this type is marked as 'beforefieldinit'.
		static DownloadItemResult_t()
		{
		}

		// Token: 0x040009D2 RID: 2514
		internal AppId AppID;

		// Token: 0x040009D3 RID: 2515
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009D4 RID: 2516
		internal Result Result;

		// Token: 0x040009D5 RID: 2517
		public static int _datasize = Marshal.SizeOf(typeof(DownloadItemResult_t));
	}
}
