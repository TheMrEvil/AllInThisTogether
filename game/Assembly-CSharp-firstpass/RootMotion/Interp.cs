using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000BC RID: 188
	public class Interp
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00038D1C File Offset: 0x00036F1C
		public static float Float(float t, InterpolationMode mode)
		{
			float result;
			switch (mode)
			{
			case InterpolationMode.None:
				result = Interp.None(t, 0f, 1f);
				break;
			case InterpolationMode.InOutCubic:
				result = Interp.InOutCubic(t, 0f, 1f);
				break;
			case InterpolationMode.InOutQuintic:
				result = Interp.InOutQuintic(t, 0f, 1f);
				break;
			case InterpolationMode.InOutSine:
				result = Interp.InOutSine(t, 0f, 1f);
				break;
			case InterpolationMode.InQuintic:
				result = Interp.InQuintic(t, 0f, 1f);
				break;
			case InterpolationMode.InQuartic:
				result = Interp.InQuartic(t, 0f, 1f);
				break;
			case InterpolationMode.InCubic:
				result = Interp.InCubic(t, 0f, 1f);
				break;
			case InterpolationMode.InQuadratic:
				result = Interp.InQuadratic(t, 0f, 1f);
				break;
			case InterpolationMode.InElastic:
				result = Interp.OutElastic(t, 0f, 1f);
				break;
			case InterpolationMode.InElasticSmall:
				result = Interp.InElasticSmall(t, 0f, 1f);
				break;
			case InterpolationMode.InElasticBig:
				result = Interp.InElasticBig(t, 0f, 1f);
				break;
			case InterpolationMode.InSine:
				result = Interp.InSine(t, 0f, 1f);
				break;
			case InterpolationMode.InBack:
				result = Interp.InBack(t, 0f, 1f);
				break;
			case InterpolationMode.OutQuintic:
				result = Interp.OutQuintic(t, 0f, 1f);
				break;
			case InterpolationMode.OutQuartic:
				result = Interp.OutQuartic(t, 0f, 1f);
				break;
			case InterpolationMode.OutCubic:
				result = Interp.OutCubic(t, 0f, 1f);
				break;
			case InterpolationMode.OutInCubic:
				result = Interp.OutInCubic(t, 0f, 1f);
				break;
			case InterpolationMode.OutInQuartic:
				result = Interp.OutInCubic(t, 0f, 1f);
				break;
			case InterpolationMode.OutElastic:
				result = Interp.OutElastic(t, 0f, 1f);
				break;
			case InterpolationMode.OutElasticSmall:
				result = Interp.OutElasticSmall(t, 0f, 1f);
				break;
			case InterpolationMode.OutElasticBig:
				result = Interp.OutElasticBig(t, 0f, 1f);
				break;
			case InterpolationMode.OutSine:
				result = Interp.OutSine(t, 0f, 1f);
				break;
			case InterpolationMode.OutBack:
				result = Interp.OutBack(t, 0f, 1f);
				break;
			case InterpolationMode.OutBackCubic:
				result = Interp.OutBackCubic(t, 0f, 1f);
				break;
			case InterpolationMode.OutBackQuartic:
				result = Interp.OutBackQuartic(t, 0f, 1f);
				break;
			case InterpolationMode.BackInCubic:
				result = Interp.BackInCubic(t, 0f, 1f);
				break;
			case InterpolationMode.BackInQuartic:
				result = Interp.BackInQuartic(t, 0f, 1f);
				break;
			default:
				result = 0f;
				break;
			}
			return result;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00038FEC File Offset: 0x000371EC
		public static Vector3 V3(Vector3 v1, Vector3 v2, float t, InterpolationMode mode)
		{
			float num = Interp.Float(t, mode);
			return (1f - num) * v1 + num * v2;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0003901A File Offset: 0x0003721A
		public static float LerpValue(float value, float target, float increaseSpeed, float decreaseSpeed)
		{
			if (value == target)
			{
				return target;
			}
			if (value < target)
			{
				return Mathf.Clamp(value + Time.deltaTime * increaseSpeed, float.NegativeInfinity, target);
			}
			return Mathf.Clamp(value - Time.deltaTime * decreaseSpeed, target, float.PositiveInfinity);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0003904F File Offset: 0x0003724F
		private static float None(float t, float b, float c)
		{
			return b + c * t;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00039058 File Offset: 0x00037258
		private static float InOutCubic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (-2f * num2 + 3f * num);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00039080 File Offset: 0x00037280
		private static float InOutQuintic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (6f * num2 * num + -15f * num * num + 10f * num2);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000390B4 File Offset: 0x000372B4
		private static float InQuintic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (num2 * num);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000390D0 File Offset: 0x000372D0
		private static float InQuartic(float t, float b, float c)
		{
			float num = t * t;
			return b + c * (num * num);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000390E8 File Offset: 0x000372E8
		private static float InCubic(float t, float b, float c)
		{
			float num = t * t * t;
			return b + c * num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00039100 File Offset: 0x00037300
		private static float InQuadratic(float t, float b, float c)
		{
			float num = t * t;
			return b + c * num;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00039118 File Offset: 0x00037318
		private static float OutQuintic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (num2 * num + -5f * num * num + 10f * num2 + -10f * num + 5f * t);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00039158 File Offset: 0x00037358
		private static float OutQuartic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (-1f * num * num + 4f * num2 + -6f * num + 4f * t);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00039194 File Offset: 0x00037394
		private static float OutCubic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (num2 + -3f * num + 3f * t);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000391C0 File Offset: 0x000373C0
		private static float OutInCubic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (4f * num2 + -6f * num + 3f * t);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000391F0 File Offset: 0x000373F0
		private static float OutInQuartic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (6f * num2 + -9f * num + 4f * t);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00039220 File Offset: 0x00037420
		private static float BackInCubic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (4f * num2 + -3f * num);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00039248 File Offset: 0x00037448
		private static float BackInQuartic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (2f * num * num + 2f * num2 + -3f * num);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0003927C File Offset: 0x0003747C
		private static float OutBackCubic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (4f * num2 + -9f * num + 6f * t);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000392AC File Offset: 0x000374AC
		private static float OutBackQuartic(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (-2f * num * num + 10f * num2 + -15f * num + 8f * t);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000392E8 File Offset: 0x000374E8
		private static float OutElasticSmall(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (33f * num2 * num + -106f * num * num + 126f * num2 + -67f * num + 15f * t);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0003932C File Offset: 0x0003752C
		private static float OutElasticBig(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (56f * num2 * num + -175f * num * num + 200f * num2 + -100f * num + 20f * t);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00039370 File Offset: 0x00037570
		private static float InElasticSmall(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (33f * num2 * num + -59f * num * num + 32f * num2 + -5f * num);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000393AC File Offset: 0x000375AC
		private static float InElasticBig(float t, float b, float c)
		{
			float num = t * t;
			float num2 = num * t;
			return b + c * (56f * num2 * num + -105f * num * num + 60f * num2 + -10f * num);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000393E8 File Offset: 0x000375E8
		private static float InSine(float t, float b, float c)
		{
			c -= b;
			return -c * Mathf.Cos(t / 1f * 1.5707964f) + c + b;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00039408 File Offset: 0x00037608
		private static float OutSine(float t, float b, float c)
		{
			c -= b;
			return c * Mathf.Sin(t / 1f * 1.5707964f) + b;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00039425 File Offset: 0x00037625
		private static float InOutSine(float t, float b, float c)
		{
			c -= b;
			return -c / 2f * (Mathf.Cos(3.1415927f * t / 1f) - 1f) + b;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00039450 File Offset: 0x00037650
		private static float InElastic(float t, float b, float c)
		{
			c -= b;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (t == 0f)
			{
				return b;
			}
			if ((t /= num) == 1f)
			{
				return b + c;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(c))
			{
				num3 = c;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(c / num3);
			}
			return -(num3 * Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t * num - num4) * 6.2831855f / num2)) + b;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000394F8 File Offset: 0x000376F8
		private static float OutElastic(float t, float b, float c)
		{
			c -= b;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (t == 0f)
			{
				return b;
			}
			if ((t /= num) == 1f)
			{
				return b + c;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(c))
			{
				num3 = c;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(c / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * num - num4) * 6.2831855f / num2) + c + b;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00039598 File Offset: 0x00037798
		private static float InBack(float t, float b, float c)
		{
			c -= b;
			t /= 1f;
			float num = 1.70158f;
			return c * t * t * ((num + 1f) * t - num) + b;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000395CC File Offset: 0x000377CC
		private static float OutBack(float t, float b, float c)
		{
			float num = 1.70158f;
			c -= b;
			t = t / 1f - 1f;
			return c * (t * t * ((num + 1f) * t + num) + 1f) + b;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0003960C File Offset: 0x0003780C
		public Interp()
		{
		}
	}
}
