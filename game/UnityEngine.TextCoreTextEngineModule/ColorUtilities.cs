using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000003 RID: 3
	internal static class ColorUtilities
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000214C File Offset: 0x0000034C
		internal static bool CompareColors(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000039C
		internal static bool CompareColorsRgb(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
		internal static bool CompareColors(Color a, Color b)
		{
			return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b) && Mathf.Approximately(a.a, b.a);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000223C File Offset: 0x0000043C
		internal static bool CompareColorsRgb(Color a, Color b)
		{
			return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002288 File Offset: 0x00000488
		internal static Color32 MultiplyColors(Color32 c1, Color32 c2)
		{
			byte r = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(r, g, b, a);
		}
	}
}
