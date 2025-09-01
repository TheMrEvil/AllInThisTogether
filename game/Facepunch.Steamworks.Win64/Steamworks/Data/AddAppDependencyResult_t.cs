using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000166 RID: 358
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddAppDependencyResult_t : ICallbackData
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000167BD File Offset: 0x000149BD
		public int DataSize
		{
			get
			{
				return AddAppDependencyResult_t._datasize;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x000167C4 File Offset: 0x000149C4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AddAppDependencyResult;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000167CB File Offset: 0x000149CB
		// Note: this type is marked as 'beforefieldinit'.
		static AddAppDependencyResult_t()
		{
		}

		// Token: 0x040009F0 RID: 2544
		internal Result Result;

		// Token: 0x040009F1 RID: 2545
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009F2 RID: 2546
		internal AppId AppID;

		// Token: 0x040009F3 RID: 2547
		public static int _datasize = Marshal.SizeOf(typeof(AddAppDependencyResult_t));
	}
}
