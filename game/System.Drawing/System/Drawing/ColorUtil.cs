using System;

namespace System.Drawing
{
	// Token: 0x02000010 RID: 16
	internal static class ColorUtil
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000253A File Offset: 0x0000073A
		public static Color FromKnownColor(KnownColor color)
		{
			return Color.FromKnownColor(color);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002542 File Offset: 0x00000742
		public static bool IsSystemColor(this Color color)
		{
			return color.IsSystemColor;
		}
	}
}
