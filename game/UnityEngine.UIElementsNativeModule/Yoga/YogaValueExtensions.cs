using System;

namespace UnityEngine.Yoga
{
	// Token: 0x0200001F RID: 31
	internal static class YogaValueExtensions
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00003BBC File Offset: 0x00001DBC
		public static YogaValue Percent(this float value)
		{
			return YogaValue.Percent(value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static YogaValue Pt(this float value)
		{
			return YogaValue.Point(value);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003BEC File Offset: 0x00001DEC
		public static YogaValue Percent(this int value)
		{
			return YogaValue.Percent((float)value);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003C08 File Offset: 0x00001E08
		public static YogaValue Pt(this int value)
		{
			return YogaValue.Point((float)value);
		}
	}
}
