using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000164 RID: 356
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddUGCDependencyResult_t : ICallbackData
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00016775 File Offset: 0x00014975
		public int DataSize
		{
			get
			{
				return AddUGCDependencyResult_t._datasize;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0001677C File Offset: 0x0001497C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AddUGCDependencyResult;
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00016783 File Offset: 0x00014983
		// Note: this type is marked as 'beforefieldinit'.
		static AddUGCDependencyResult_t()
		{
		}

		// Token: 0x040009E8 RID: 2536
		internal Result Result;

		// Token: 0x040009E9 RID: 2537
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009EA RID: 2538
		internal PublishedFileId ChildPublishedFileId;

		// Token: 0x040009EB RID: 2539
		public static int _datasize = Marshal.SizeOf(typeof(AddUGCDependencyResult_t));
	}
}
