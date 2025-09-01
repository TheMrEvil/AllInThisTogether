using System;

namespace UnityEngine.Yoga
{
	// Token: 0x02000009 RID: 9
	internal static class YogaConstants
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002224 File Offset: 0x00000424
		public static bool IsUndefined(float value)
		{
			return float.IsNaN(value);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000223C File Offset: 0x0000043C
		public static bool IsUndefined(YogaValue value)
		{
			return value.Unit == YogaUnit.Undefined;
		}

		// Token: 0x0400000D RID: 13
		public const float Undefined = float.NaN;
	}
}
