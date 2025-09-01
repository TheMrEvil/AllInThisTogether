using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C3 RID: 195
	public static class V2Tools
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00039F77 File Offset: 0x00038177
		public static Vector2 XZ(Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00039F8C File Offset: 0x0003818C
		public static float DeltaAngle(Vector2 dir1, Vector2 dir2)
		{
			float current = Mathf.Atan2(dir1.x, dir1.y) * 57.29578f;
			float target = Mathf.Atan2(dir2.x, dir2.y) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00039FD0 File Offset: 0x000381D0
		public static float DeltaAngleXZ(Vector3 dir1, Vector3 dir2)
		{
			float current = Mathf.Atan2(dir1.x, dir1.z) * 57.29578f;
			float target = Mathf.Atan2(dir2.x, dir2.z) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003A014 File Offset: 0x00038214
		public static bool LineCircleIntersect(Vector2 p1, Vector2 p2, Vector2 c, float r)
		{
			Vector2 vector = p2 - p1;
			Vector2 vector2 = c - p1;
			float num = Vector2.Dot(vector, vector);
			float num2 = 2f * Vector2.Dot(vector2, vector);
			float num3 = Vector2.Dot(vector2, vector2) - r * r;
			float num4 = num2 * num2 - 4f * num * num3;
			if (num4 < 0f)
			{
				return false;
			}
			num4 = Mathf.Sqrt(num4);
			float num5 = 2f * num;
			float num6 = (num2 - num4) / num5;
			float num7 = (num2 + num4) / num5;
			return (num6 >= 0f && num6 <= 1f) || (num7 >= 0f && num7 <= 1f);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0003A0BC File Offset: 0x000382BC
		public static bool RayCircleIntersect(Vector2 p1, Vector2 dir, Vector2 c, float r)
		{
			Vector2 vector = p1 + dir;
			p1 -= c;
			vector -= c;
			float f = vector.x - p1.x;
			float f2 = vector.y - p1.y;
			float f3 = Mathf.Sqrt(Mathf.Pow(f, 2f) + Mathf.Pow(f2, 2f));
			float f4 = p1.x * vector.y - vector.x * p1.y;
			return Mathf.Pow(r, 2f) * Mathf.Pow(f3, 2f) - Mathf.Pow(f4, 2f) >= 0f;
		}
	}
}
