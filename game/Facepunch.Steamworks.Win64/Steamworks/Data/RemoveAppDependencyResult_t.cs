using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoveAppDependencyResult_t : ICallbackData
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x000167E1 File Offset: 0x000149E1
		public int DataSize
		{
			get
			{
				return RemoveAppDependencyResult_t._datasize;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000167E8 File Offset: 0x000149E8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoveAppDependencyResult;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000167EF File Offset: 0x000149EF
		// Note: this type is marked as 'beforefieldinit'.
		static RemoveAppDependencyResult_t()
		{
		}

		// Token: 0x040009F4 RID: 2548
		internal Result Result;

		// Token: 0x040009F5 RID: 2549
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009F6 RID: 2550
		internal AppId AppID;

		// Token: 0x040009F7 RID: 2551
		public static int _datasize = Marshal.SizeOf(typeof(RemoveAppDependencyResult_t));
	}
}
