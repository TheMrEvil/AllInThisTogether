using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003B RID: 59
	public static class FLogicMethods
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000B448 File Offset: 0x00009648
		public static float Lerp(this float from, float to, float value)
		{
			if (to != from)
			{
				return Mathf.Clamp((value - from) / (to - from), -1f, 1f);
			}
			return 0f;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000B46A File Offset: 0x0000966A
		public static float InverseLerp(float from, float to, float value)
		{
			if (to != from)
			{
				return Mathf.Clamp((value - from) / (to - from), -1f, 1f);
			}
			return 0f;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000B48C File Offset: 0x0000968C
		public static float InverseLerpUnclamped(float xx, float yy, float value)
		{
			if (yy - xx == 0f)
			{
				return 0f;
			}
			return (value - xx) / (yy - xx);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000B4A8 File Offset: 0x000096A8
		public static float FLerp(float a, float b, float t, float factor = 0.01f)
		{
			float num = b;
			if (num > a)
			{
				b += factor;
			}
			else
			{
				b -= factor;
			}
			float num2 = Mathf.LerpUnclamped(a, b, t);
			if (num > a)
			{
				if (num2 >= num)
				{
					return num;
				}
			}
			else if (num2 <= num)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public static int IntLerp(int a, int b, float t)
		{
			int result = 0;
			FLogicMethods.IntLerp(ref result, a, b, t);
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000B4FE File Offset: 0x000096FE
		public static void IntLerp(ref int source, int a, int b, float t)
		{
			source = Mathf.RoundToInt((float)a * (1f - t)) + Mathf.RoundToInt((float)b * t);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000B51B File Offset: 0x0000971B
		public static void IntLerp(ref int source, int b, float t)
		{
			FLogicMethods.IntLerp(ref source, source, b, t);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000B527 File Offset: 0x00009727
		public static float FAbs(this float value)
		{
			if (value < 0f)
			{
				value = -value;
			}
			return value;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000B536 File Offset: 0x00009736
		public static float HyperCurve(this float value)
		{
			return -(1f / (3.2f * value - 4f)) - 0.25f;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000B552 File Offset: 0x00009752
		public static float TopDownDistanceManhattan(this Vector3 a, Vector3 b)
		{
			return 0f + (a.x - b.x).FAbs() + (a.z - b.z).FAbs();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000B57F File Offset: 0x0000977F
		public static float TopDownDistance(this Vector3 a, Vector3 b)
		{
			a.y = a.z;
			b.y = b.z;
			return Vector2.Distance(a, b);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000B5AC File Offset: 0x000097AC
		public static float DistanceManhattan(this Vector3 a, Vector3 b)
		{
			return 0f + (a.x - b.x).FAbs() + (a.y - b.y).FAbs() + (a.z - b.z).FAbs();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000B5EC File Offset: 0x000097EC
		public static float WrapAngle(float angle)
		{
			angle %= 360f;
			if (angle > 180f)
			{
				return angle - 360f;
			}
			return angle;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000B608 File Offset: 0x00009808
		public static Vector3 WrapVector(Vector3 angles)
		{
			return new Vector3(FLogicMethods.WrapAngle(angles.x), FLogicMethods.WrapAngle(angles.y), FLogicMethods.WrapAngle(angles.z));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000B630 File Offset: 0x00009830
		public static float UnwrapAngle(float angle)
		{
			if (angle >= 0f)
			{
				return angle;
			}
			angle = -angle % 360f;
			return 360f - angle;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000B64D File Offset: 0x0000984D
		public static Vector3 UnwrapVector(Vector3 angles)
		{
			return new Vector3(FLogicMethods.UnwrapAngle(angles.x), FLogicMethods.UnwrapAngle(angles.y), FLogicMethods.UnwrapAngle(angles.z));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000B678 File Offset: 0x00009878
		public static bool IsAlmostEqual(float val, float to, int afterComma = 2, float addRange = 0f)
		{
			float num = 1f / Mathf.Pow(10f, (float)afterComma) + addRange;
			return (val > to - num && val < to + num) || val == to;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000B6AD File Offset: 0x000098AD
		public static Quaternion TopDownAngle(Vector3 from, Vector3 to)
		{
			from.y = 0f;
			to.y = 0f;
			return Quaternion.LookRotation(to - from);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public static Quaternion TopDownAnglePosition2D(Vector2 from, Vector2 to, float offset = 0f)
		{
			Vector2 vector = to - from;
			return Quaternion.AngleAxis(Mathf.Atan2(vector.y, vector.x) * 57.29578f + offset, Vector3.forward);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000B70C File Offset: 0x0000990C
		public static bool ContainsIndex<T>(this List<T> list, int i, bool falseIfNull = true) where T : class
		{
			return list != null && i >= 0 && i < list.Count && (!falseIfNull || list[i] != null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000B738 File Offset: 0x00009938
		public static bool ContainsIndex<T>(this List<T> list, int i) where T : struct
		{
			return list != null && i >= 0 && i < list.Count;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000B751 File Offset: 0x00009951
		public static bool ContainsIndex<T>(this T[] list, int i, bool falseIfNull) where T : class
		{
			return list != null && i >= 0 && i < list.Length && (!falseIfNull || list[i] != null);
		}
	}
}
