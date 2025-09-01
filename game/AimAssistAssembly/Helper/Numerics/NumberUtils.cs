using System;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Numerics
{
	// Token: 0x02000007 RID: 7
	public static class NumberUtils
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000231E File Offset: 0x0000051E
		public static bool EqualsApprox(this Vector2 first, Vector2 second, float epsilon = 0.01f)
		{
			return Math.Abs(first.x - second.x) < epsilon && Math.Abs(first.y - second.y) < epsilon;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000234C File Offset: 0x0000054C
		public static bool EqualsApprox(this float f1, float f2, float epsilon = 0.01f)
		{
			return Math.Abs(f1 - f2) < epsilon;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002359 File Offset: 0x00000559
		public static float Sanitized(this float f)
		{
			if (!float.IsNaN(f))
			{
				return f;
			}
			return 0f;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000236A File Offset: 0x0000056A
		public static bool Between(this float f, float floor, float ceiling)
		{
			return f >= floor && f <= ceiling;
		}

		// Token: 0x04000011 RID: 17
		private const float Epsilon = 0.01f;
	}
}
