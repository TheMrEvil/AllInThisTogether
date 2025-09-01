using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000692 RID: 1682
	internal static class BinaryUtil
	{
		// Token: 0x06003E0E RID: 15886 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
