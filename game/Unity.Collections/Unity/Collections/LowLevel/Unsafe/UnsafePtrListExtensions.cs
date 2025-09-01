using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000103 RID: 259
	internal static class UnsafePtrListExtensions
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		public static ref UnsafeList ListData(this UnsafePtrList from)
		{
			return UnsafeUtility.As<UnsafePtrList, UnsafeList>(ref from);
		}
	}
}
