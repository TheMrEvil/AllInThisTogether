using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015D RID: 349
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ItemInstalled_t : ICallbackData
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00016679 File Offset: 0x00014879
		public int DataSize
		{
			get
			{
				return ItemInstalled_t._datasize;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00016680 File Offset: 0x00014880
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ItemInstalled;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00016687 File Offset: 0x00014887
		// Note: this type is marked as 'beforefieldinit'.
		static ItemInstalled_t()
		{
		}

		// Token: 0x040009CF RID: 2511
		internal AppId AppID;

		// Token: 0x040009D0 RID: 2512
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009D1 RID: 2513
		public static int _datasize = Marshal.SizeOf(typeof(ItemInstalled_t));
	}
}
