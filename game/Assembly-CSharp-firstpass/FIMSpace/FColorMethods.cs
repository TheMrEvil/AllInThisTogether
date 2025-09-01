using System;
using System.Globalization;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000037 RID: 55
	public static class FColorMethods
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000099FC File Offset: 0x00007BFC
		public static Color ChangeColorAlpha(this Color color, float alpha)
		{
			return new Color(color.r, color.g, color.b, alpha);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009A18 File Offset: 0x00007C18
		public static Color ToGammaSpace(Color hdrColor)
		{
			float num = hdrColor.r;
			if (hdrColor.g > num)
			{
				num = hdrColor.g;
			}
			if (hdrColor.b > num)
			{
				num = hdrColor.b;
			}
			if (hdrColor.a > num)
			{
				num = hdrColor.a;
			}
			if (num <= 0f)
			{
				return Color.clear;
			}
			return hdrColor / num;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009A71 File Offset: 0x00007C71
		public static Color ChangeColorsValue(this Color color, float brightenOrDarken = 0f)
		{
			return new Color(color.r + brightenOrDarken, color.g + brightenOrDarken, color.b + brightenOrDarken, color.a);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009A98 File Offset: 0x00007C98
		public static Color32 HexToColor(this string hex)
		{
			if (string.IsNullOrEmpty(hex))
			{
				FDebug.LogRed("Trying convert from hex to color empty string!");
				return Color.white;
			}
			uint num = 255U;
			hex = hex.Replace("#", "");
			hex = hex.Replace("0x", "");
			if (!uint.TryParse(hex, NumberStyles.HexNumber, null, out num))
			{
				Debug.Log("Error during converting hex string.");
				return Color.white;
			}
			return new Color32((byte)(((ulong)num & 18446744073692774400UL) >> 24), (byte)((num & 16711680U) >> 16), (byte)((num & 65280U) >> 8), (byte)(num & 255U));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00009B40 File Offset: 0x00007D40
		public static string ColorToHex(this Color32 color, bool addHash = true)
		{
			string str = "";
			if (addHash)
			{
				str = "#";
			}
			return str + string.Format("{0}{1}{2}{3}", new object[]
			{
				(color.r.ToString("X").Length == 1) ? string.Format("0{0}", color.r.ToString("X")) : color.r.ToString("X"),
				(color.g.ToString("X").Length == 1) ? string.Format("0{0}", color.g.ToString("X")) : color.g.ToString("X"),
				(color.b.ToString("X").Length == 1) ? string.Format("0{0}", color.b.ToString("X")) : color.b.ToString("X"),
				(color.a.ToString("X").Length == 1) ? string.Format("0{0}", color.a.ToString("X")) : color.a.ToString("X")
			});
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009C9C File Offset: 0x00007E9C
		public static string ColorToHex(this Color color, bool addHash = true)
		{
			return new Color32((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), (byte)(color.a * 255f)).ColorToHex(addHash);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static void LerpMaterialColor(this Material mat, string property, Color targetColor, float deltaMultiplier = 8f)
		{
			if (mat == null)
			{
				return;
			}
			if (!mat.HasProperty(property))
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Material ",
					mat.name,
					" don't have property '",
					property,
					"'  in shader ",
					mat.shader.name
				}));
				return;
			}
			Color color = mat.GetColor(property);
			mat.SetColor(property, Color.Lerp(color, targetColor, Time.deltaTime * deltaMultiplier));
		}
	}
}
