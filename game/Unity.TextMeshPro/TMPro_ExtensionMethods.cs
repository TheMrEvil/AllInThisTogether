using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000011 RID: 17
	public static class TMPro_ExtensionMethods
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00016ABC File Offset: 0x00014CBC
		public static int[] ToIntArray(this string text)
		{
			int[] array = new int[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (int)text[i];
			}
			return array;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00016AF4 File Offset: 0x00014CF4
		public static string ArrayToString(this char[] chars)
		{
			string text = string.Empty;
			int num = 0;
			while (num < chars.Length && chars[num] != '\0')
			{
				text += chars[num].ToString();
				num++;
			}
			return text;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00016B30 File Offset: 0x00014D30
		public static string IntToString(this int[] unicodes)
		{
			char[] array = new char[unicodes.Length];
			for (int i = 0; i < unicodes.Length; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00016B64 File Offset: 0x00014D64
		internal static string UintToString(this List<uint> unicodes)
		{
			char[] array = new char[unicodes.Count];
			for (int i = 0; i < unicodes.Count; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00016BA0 File Offset: 0x00014DA0
		public static string IntToString(this int[] unicodes, int start, int length)
		{
			if (start > unicodes.Length)
			{
				return string.Empty;
			}
			int num = Mathf.Min(start + length, unicodes.Length);
			char[] array = new char[num - start];
			int num2 = 0;
			for (int i = start; i < num; i++)
			{
				array[num2++] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public static int FindInstanceID<T>(this List<T> list, T target) where T : UnityEngine.Object
		{
			int instanceID = target.GetInstanceID();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetInstanceID() == instanceID)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00016C31 File Offset: 0x00014E31
		public static bool Compare(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00016C6D File Offset: 0x00014E6D
		public static bool CompareRGB(this Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00016C9B File Offset: 0x00014E9B
		public static bool Compare(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00016CD7 File Offset: 0x00014ED7
		public static bool CompareRGB(this Color a, Color b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00016D08 File Offset: 0x00014F08
		public static Color32 Multiply(this Color32 c1, Color32 c2)
		{
			byte r = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(r, g, b, a);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public static Color32 Tint(this Color32 c1, Color32 c2)
		{
			byte r = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(r, g, b, a);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00016E48 File Offset: 0x00015048
		public static Color32 Tint(this Color32 c1, float tint)
		{
			byte r = (byte)Mathf.Clamp((float)c1.r / 255f * tint * 255f, 0f, 255f);
			byte g = (byte)Mathf.Clamp((float)c1.g / 255f * tint * 255f, 0f, 255f);
			byte b = (byte)Mathf.Clamp((float)c1.b / 255f * tint * 255f, 0f, 255f);
			byte a = (byte)Mathf.Clamp((float)c1.a / 255f * tint * 255f, 0f, 255f);
			return new Color32(r, g, b, a);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00016EF4 File Offset: 0x000150F4
		public static Color MinAlpha(this Color c1, Color c2)
		{
			float a = (c1.a < c2.a) ? c1.a : c2.a;
			return new Color(c1.r, c1.g, c1.b, a);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00016F38 File Offset: 0x00015138
		public static bool Compare(this Vector3 v1, Vector3 v2, int accuracy)
		{
			bool flag = (int)(v1.x * (float)accuracy) == (int)(v2.x * (float)accuracy);
			bool flag2 = (int)(v1.y * (float)accuracy) == (int)(v2.y * (float)accuracy);
			bool flag3 = (int)(v1.z * (float)accuracy) == (int)(v2.z * (float)accuracy);
			return flag && flag2 && flag3;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00016F90 File Offset: 0x00015190
		public static bool Compare(this Quaternion q1, Quaternion q2, int accuracy)
		{
			bool flag = (int)(q1.x * (float)accuracy) == (int)(q2.x * (float)accuracy);
			bool flag2 = (int)(q1.y * (float)accuracy) == (int)(q2.y * (float)accuracy);
			bool flag3 = (int)(q1.z * (float)accuracy) == (int)(q2.z * (float)accuracy);
			bool flag4 = (int)(q1.w * (float)accuracy) == (int)(q2.w * (float)accuracy);
			return flag && flag2 && flag3 && flag4;
		}
	}
}
