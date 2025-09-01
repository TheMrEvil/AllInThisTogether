using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002E RID: 46
	public static class Tween
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00004A1B File Offset: 0x00002C1B
		public static float EaseIn(float t)
		{
			return t * t;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004A20 File Offset: 0x00002C20
		public static float Flip(float x)
		{
			return 1f - x;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004A29 File Offset: 0x00002C29
		public static float Square(float t)
		{
			return t * t;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004A2E File Offset: 0x00002C2E
		public static float EaseOut(float t)
		{
			return Tween.Flip(Tween.Square(Tween.Flip(t)));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A40 File Offset: 0x00002C40
		public static float EaseInOut(float t)
		{
			return Mathf.Lerp(Tween.EaseIn(t), Tween.EaseOut(t), t);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A54 File Offset: 0x00002C54
		public static float BounceOut(float t)
		{
			if (t < 0.36363637f)
			{
				return 7.5625f * t * t;
			}
			if (t < 0.72727275f)
			{
				return 7.5625f * (t -= 0.54545456f) * t + 0.75f;
			}
			if (t < 0.90909094f)
			{
				return 7.5625f * (t -= 0.8181818f) * t + 0.9375f;
			}
			return 7.5625f * (t -= 0.95454544f) * t + 0.984375f;
		}
	}
}
