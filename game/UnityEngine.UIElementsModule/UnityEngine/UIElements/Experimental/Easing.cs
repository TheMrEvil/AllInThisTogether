using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000386 RID: 902
	public static class Easing
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x000892A4 File Offset: 0x000874A4
		public static float Step(float t)
		{
			return (float)((t < 0.5f) ? 0 : 1);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000892C4 File Offset: 0x000874C4
		public static float Linear(float t)
		{
			return t;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000892D8 File Offset: 0x000874D8
		public static float InSine(float t)
		{
			return Mathf.Sin(1.5707964f * (t - 1f)) + 1f;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00089304 File Offset: 0x00087504
		public static float OutSine(float t)
		{
			return Mathf.Sin(t * 1.5707964f);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00089324 File Offset: 0x00087524
		public static float InOutSine(float t)
		{
			return (Mathf.Sin(3.1415927f * (t - 0.5f)) + 1f) * 0.5f;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00089354 File Offset: 0x00087554
		public static float InQuad(float t)
		{
			return t * t;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008936C File Offset: 0x0008756C
		public static float OutQuad(float t)
		{
			return t * (2f - t);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00089388 File Offset: 0x00087588
		public static float InOutQuad(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = t * t * 0.5f;
			}
			else
			{
				result = -0.5f * ((t - 1f) * (t - 3f) - 1f);
			}
			return result;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000893D8 File Offset: 0x000875D8
		public static float InCubic(float t)
		{
			return Easing.InPower(t, 3);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000893F4 File Offset: 0x000875F4
		public static float OutCubic(float t)
		{
			return Easing.OutPower(t, 3);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00089410 File Offset: 0x00087610
		public static float InOutCubic(float t)
		{
			return Easing.InOutPower(t, 3);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008942C File Offset: 0x0008762C
		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, (float)power);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00089448 File Offset: 0x00087648
		public static float OutPower(float t, int power)
		{
			int num = (power % 2 == 0) ? -1 : 1;
			return (float)num * (Mathf.Pow(t - 1f, (float)power) + (float)num);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008947C File Offset: 0x0008767C
		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = Easing.InPower(t, power) * 0.5f;
			}
			else
			{
				int num = (power % 2 == 0) ? -1 : 1;
				result = (float)num * 0.5f * (Mathf.Pow(t - 2f, (float)power) + (float)(num * 2));
			}
			return result;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000894DC File Offset: 0x000876DC
		public static float InBounce(float t)
		{
			return 1f - Easing.OutBounce(1f - t);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00089500 File Offset: 0x00087700
		public static float OutBounce(float t)
		{
			bool flag = t < 0.36363637f;
			float result;
			if (flag)
			{
				result = 7.5625f * t * t;
			}
			else
			{
				bool flag2 = t < 0.72727275f;
				if (flag2)
				{
					float num;
					t = (num = t - 0.54545456f);
					result = 7.5625f * num * t + 0.75f;
				}
				else
				{
					bool flag3 = t < 0.90909094f;
					if (flag3)
					{
						float num2;
						t = (num2 = t - 0.8181818f);
						result = 7.5625f * num2 * t + 0.9375f;
					}
					else
					{
						float num3;
						t = (num3 = t - 0.95454544f);
						result = 7.5625f * num3 * t + 0.984375f;
					}
				}
			}
			return result;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000895A0 File Offset: 0x000877A0
		public static float InOutBounce(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBounce(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000895F4 File Offset: 0x000877F4
		public static float InElastic(float t)
		{
			bool flag = t == 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					result = 1f;
				}
				else
				{
					float num = 0.3f;
					float num2 = num / 4f;
					float num3 = Mathf.Pow(2f, 10f * (t -= 1f));
					result = -(num3 * Mathf.Sin((t - num2) * 6.2831855f / num));
				}
			}
			return result;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00089670 File Offset: 0x00087870
		public static float OutElastic(float t)
		{
			bool flag = t == 0f;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					result = 1f;
				}
				else
				{
					float num = 0.3f;
					float num2 = num / 4f;
					result = Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num2) * 6.2831855f / num) + 1f;
				}
			}
			return result;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000896E4 File Offset: 0x000878E4
		public static float InOutElastic(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InElastic(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00089738 File Offset: 0x00087938
		public static float InBack(float t)
		{
			float num = 1.70158f;
			return t * t * ((num + 1f) * t - num);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00089760 File Offset: 0x00087960
		public static float OutBack(float t)
		{
			return 1f - Easing.InBack(1f - t);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00089784 File Offset: 0x00087984
		public static float InOutBack(float t)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBack(t * 2f) * 0.5f;
			}
			else
			{
				result = Easing.OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return result;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000897D8 File Offset: 0x000879D8
		public static float InBack(float t, float s)
		{
			return t * t * ((s + 1f) * t - s);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000897FC File Offset: 0x000879FC
		public static float OutBack(float t, float s)
		{
			return 1f - Easing.InBack(1f - t, s);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00089824 File Offset: 0x00087A24
		public static float InOutBack(float t, float s)
		{
			bool flag = t < 0.5f;
			float result;
			if (flag)
			{
				result = Easing.InBack(t * 2f, s) * 0.5f;
			}
			else
			{
				result = Easing.OutBack((t - 0.5f) * 2f, s) * 0.5f + 0.5f;
			}
			return result;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008987C File Offset: 0x00087A7C
		public static float InCirc(float t)
		{
			return -(Mathf.Sqrt(1f - t * t) - 1f);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000898A4 File Offset: 0x00087AA4
		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000898D0 File Offset: 0x00087AD0
		public static float InOutCirc(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float result;
			if (flag)
			{
				result = -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}
			else
			{
				t -= 2f;
				result = 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
			}
			return result;
		}

		// Token: 0x04000EA0 RID: 3744
		private const float HalfPi = 1.5707964f;
	}
}
