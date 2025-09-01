using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000168 RID: 360
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAppDependenciesResult_t : ICallbackData
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00016805 File Offset: 0x00014A05
		public int DataSize
		{
			get
			{
				return GetAppDependenciesResult_t._datasize;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0001680C File Offset: 0x00014A0C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GetAppDependenciesResult;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00016813 File Offset: 0x00014A13
		// Note: this type is marked as 'beforefieldinit'.
		static GetAppDependenciesResult_t()
		{
		}

		// Token: 0x040009F8 RID: 2552
		internal Result Result;

		// Token: 0x040009F9 RID: 2553
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009FA RID: 2554
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs;

		// Token: 0x040009FB RID: 2555
		internal uint NumAppDependencies;

		// Token: 0x040009FC RID: 2556
		internal uint TotalNumAppDependencies;

		// Token: 0x040009FD RID: 2557
		public static int _datasize = Marshal.SizeOf(typeof(GetAppDependenciesResult_t));
	}
}
