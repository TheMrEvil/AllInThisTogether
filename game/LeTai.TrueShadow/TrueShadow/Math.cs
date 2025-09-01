using System;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x0200001A RID: 26
	public static class Math
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00006794 File Offset: 0x00004994
		public static float Angle360(Vector2 from, Vector2 to)
		{
			float num = Vector2.SignedAngle(from, to);
			if (num >= 0f)
			{
				return num;
			}
			return 360f + num;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000067BA File Offset: 0x000049BA
		public static Vector2 AngleDistanceVector(float angle, float distance, Vector2 zeroVector)
		{
			return Quaternion.Euler(0f, 0f, -angle) * zeroVector * distance;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000067E4 File Offset: 0x000049E4
		public static Vector2 Rotate(this Vector2 v, float angle)
		{
			float f = angle * 0.017453292f;
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			return new Vector2(num2 * v.x - num * v.y, num * v.x + num2 * v.y);
		}
	}
}
