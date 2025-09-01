using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000E RID: 14
	internal struct FontEngineUtilities
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004908 File Offset: 0x00002B08
		internal static bool Approximately(float a, float b)
		{
			return Mathf.Abs(a - b) < 0.001f;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000492C File Offset: 0x00002B2C
		internal static int MaxValue(int a, int b, int c)
		{
			return (a < b) ? ((b < c) ? c : b) : ((a < c) ? c : a);
		}
	}
}
