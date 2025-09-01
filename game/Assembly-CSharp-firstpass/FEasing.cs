using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public static class FEasing
{
	// Token: 0x0600002F RID: 47 RVA: 0x00002D4E File Offset: 0x00000F4E
	public static float EaseInCubic(float start, float end, float value, float ignore = 1f)
	{
		end -= start;
		return end * value * value * value + start;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002D5E File Offset: 0x00000F5E
	public static float EaseOutCubic(float start, float end, float value, float ignore = 1f)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value + 1f) + start;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002D80 File Offset: 0x00000F80
	public static float EaseInOutCubic(float start, float end, float value, float ignore = 1f)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value + start;
		}
		value -= 2f;
		return end * 0.5f * (value * value * value + 2f) + start;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002DD4 File Offset: 0x00000FD4
	public static float EaseOutElastic(float start, float end, float value, float rangeMul = 1f)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f * rangeMul;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 * 0.25f * rangeMul;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		return num3 * Mathf.Pow(2f, -10f * value * rangeMul) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002E74 File Offset: 0x00001074
	public static float EaseInElastic(float start, float end, float value, float rangeMul = 1f)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f * rangeMul;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f * rangeMul;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		return -(num3 * Mathf.Pow(2f, 10f * rangeMul * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002F1C File Offset: 0x0000111C
	public static float EaseInOutElastic(float start, float end, float value, float rangeMul = 1f)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f * rangeMul;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num * 0.5f) == 2f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f * rangeMul;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		if (value < 1f)
		{
			return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
		}
		return num3 * Mathf.Pow(2f, -10f * rangeMul * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003010 File Offset: 0x00001210
	public static float EaseInExpo(float start, float end, float value, float ignore = 1f)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003032 File Offset: 0x00001232
	public static float EaseOutExpo(float start, float end, float value, float ignore = 1f)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003058 File Offset: 0x00001258
	public static float EaseInOutExpo(float start, float end, float value, float ignore = 1f)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}
		value -= 1f;
		return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000030C8 File Offset: 0x000012C8
	public static float Linear(float start, float end, float value, float ignore = 1f)
	{
		return Mathf.Lerp(start, end, value);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000030D4 File Offset: 0x000012D4
	public static FEasing.Function GetEasingFunction(FEasing.EFease easingFunction)
	{
		if (easingFunction == FEasing.EFease.EaseInCubic)
		{
			return new FEasing.Function(FEasing.EaseInCubic);
		}
		if (easingFunction == FEasing.EFease.EaseOutCubic)
		{
			return new FEasing.Function(FEasing.EaseOutCubic);
		}
		if (easingFunction == FEasing.EFease.EaseInOutCubic)
		{
			return new FEasing.Function(FEasing.EaseInOutCubic);
		}
		if (easingFunction == FEasing.EFease.EaseInElastic)
		{
			return new FEasing.Function(FEasing.EaseInElastic);
		}
		if (easingFunction == FEasing.EFease.EaseOutElastic)
		{
			return new FEasing.Function(FEasing.EaseOutElastic);
		}
		if (easingFunction == FEasing.EFease.EaseInOutElastic)
		{
			return new FEasing.Function(FEasing.EaseInOutElastic);
		}
		if (easingFunction == FEasing.EFease.EaseInExpo)
		{
			return new FEasing.Function(FEasing.EaseInExpo);
		}
		if (easingFunction == FEasing.EFease.EaseOutExpo)
		{
			return new FEasing.Function(FEasing.EaseOutExpo);
		}
		if (easingFunction == FEasing.EFease.EaseInOutExpo)
		{
			return new FEasing.Function(FEasing.EaseInOutExpo);
		}
		if (easingFunction == FEasing.EFease.Linear)
		{
			return new FEasing.Function(FEasing.Linear);
		}
		return null;
	}

	// Token: 0x0200018C RID: 396
	public enum EFease
	{
		// Token: 0x04000C49 RID: 3145
		EaseInCubic,
		// Token: 0x04000C4A RID: 3146
		EaseOutCubic,
		// Token: 0x04000C4B RID: 3147
		EaseInOutCubic,
		// Token: 0x04000C4C RID: 3148
		EaseInOutElastic,
		// Token: 0x04000C4D RID: 3149
		EaseInElastic,
		// Token: 0x04000C4E RID: 3150
		EaseOutElastic,
		// Token: 0x04000C4F RID: 3151
		EaseInExpo,
		// Token: 0x04000C50 RID: 3152
		EaseOutExpo,
		// Token: 0x04000C51 RID: 3153
		EaseInOutExpo,
		// Token: 0x04000C52 RID: 3154
		Linear
	}

	// Token: 0x0200018D RID: 397
	// (Invoke) Token: 0x06000EA9 RID: 3753
	public delegate float Function(float s, float e, float v, float extraParameter = 1f);
}
