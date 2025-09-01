using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoveUGCDependencyResult_t : ICallbackData
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00016799 File Offset: 0x00014999
		public int DataSize
		{
			get
			{
				return RemoveUGCDependencyResult_t._datasize;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x000167A0 File Offset: 0x000149A0
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoveUGCDependencyResult;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000167A7 File Offset: 0x000149A7
		// Note: this type is marked as 'beforefieldinit'.
		static RemoveUGCDependencyResult_t()
		{
		}

		// Token: 0x040009EC RID: 2540
		internal Result Result;

		// Token: 0x040009ED RID: 2541
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009EE RID: 2542
		internal PublishedFileId ChildPublishedFileId;

		// Token: 0x040009EF RID: 2543
		public static int _datasize = Marshal.SizeOf(typeof(RemoveUGCDependencyResult_t));
	}
}
