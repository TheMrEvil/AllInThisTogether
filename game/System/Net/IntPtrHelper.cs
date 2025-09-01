using System;

namespace System.Net
{
	// Token: 0x020005DB RID: 1499
	internal static class IntPtrHelper
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x000A6470 File Offset: 0x000A4670
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000A6480 File Offset: 0x000A4680
		internal static long Subtract(IntPtr a, IntPtr b)
		{
			return (long)a - (long)b;
		}
	}
}
