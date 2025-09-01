using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteItemResult_t : ICallbackData
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00016829 File Offset: 0x00014A29
		public int DataSize
		{
			get
			{
				return DeleteItemResult_t._datasize;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00016830 File Offset: 0x00014A30
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.DeleteItemResult;
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00016837 File Offset: 0x00014A37
		// Note: this type is marked as 'beforefieldinit'.
		static DeleteItemResult_t()
		{
		}

		// Token: 0x040009FE RID: 2558
		internal Result Result;

		// Token: 0x040009FF RID: 2559
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000A00 RID: 2560
		public static int _datasize = Marshal.SizeOf(typeof(DeleteItemResult_t));
	}
}
