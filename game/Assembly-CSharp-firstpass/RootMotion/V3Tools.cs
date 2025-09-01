using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C4 RID: 196
	public static class V3Tools
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x0003A162 File Offset: 0x00038362
		public static float GetYaw(Vector3 forward)
		{
			return Mathf.Atan2(forward.x, forward.z) * 57.29578f;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0003A17B File Offset: 0x0003837B
		public static float GetPitch(Vector3 forward)
		{
			forward = forward.normalized;
			return -Mathf.Asin(forward.y) * 57.29578f;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0003A198 File Offset: 0x00038398
		public static float GetBank(Vector3 forward, Vector3 up)
		{
			up = Quaternion.Inverse(Quaternion.LookRotation(Vector3.up, forward)) * up;
			return Mathf.Atan2(up.x, up.z) * 57.29578f;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0003A1CC File Offset: 0x000383CC
		public static float GetYaw(Vector3 spaceForward, Vector3 spaceUp, Vector3 forward)
		{
			Vector3 vector = Quaternion.Inverse(Quaternion.LookRotation(spaceForward, spaceUp)) * forward;
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0003A203 File Offset: 0x00038403
		public static float GetPitch(Vector3 spaceForward, Vector3 spaceUp, Vector3 forward)
		{
			return -Mathf.Asin((Quaternion.Inverse(Quaternion.LookRotation(spaceForward, spaceUp)) * forward).y) * 57.29578f;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003A228 File Offset: 0x00038428
		public static float GetBank(Vector3 spaceForward, Vector3 spaceUp, Vector3 forward, Vector3 up)
		{
			Quaternion rotation = Quaternion.Inverse(Quaternion.LookRotation(spaceForward, spaceUp));
			forward = rotation * forward;
			up = rotation * up;
			up = Quaternion.Inverse(Quaternion.LookRotation(spaceUp, forward)) * up;
			return Mathf.Atan2(up.x, up.z) * 57.29578f;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0003A27D File Offset: 0x0003847D
		public static Vector3 Lerp(Vector3 fromVector, Vector3 toVector, float weight)
		{
			if (weight <= 0f)
			{
				return fromVector;
			}
			if (weight >= 1f)
			{
				return toVector;
			}
			return Vector3.Lerp(fromVector, toVector, weight);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003A29B File Offset: 0x0003849B
		public static Vector3 Slerp(Vector3 fromVector, Vector3 toVector, float weight)
		{
			if (weight <= 0f)
			{
				return fromVector;
			}
			if (weight >= 1f)
			{
				return toVector;
			}
			return Vector3.Slerp(fromVector, toVector, weight);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0003A2B9 File Offset: 0x000384B9
		public static Vector3 ExtractVertical(Vector3 v, Vector3 verticalAxis, float weight)
		{
			if (weight == 0f)
			{
				return Vector3.zero;
			}
			return Vector3.Project(v, verticalAxis) * weight;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0003A2D8 File Offset: 0x000384D8
		public static Vector3 ExtractHorizontal(Vector3 v, Vector3 normal, float weight)
		{
			if (weight == 0f)
			{
				return Vector3.zero;
			}
			Vector3 onNormal = v;
			Vector3.OrthoNormalize(ref normal, ref onNormal);
			return Vector3.Project(v, onNormal) * weight;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0003A30C File Offset: 0x0003850C
		public static Vector3 ClampDirection(Vector3 direction, Vector3 normalDirection, float clampWeight, int clampSmoothing)
		{
			if (clampWeight <= 0f)
			{
				return direction;
			}
			if (clampWeight >= 1f)
			{
				return normalDirection;
			}
			float num = Vector3.Angle(normalDirection, direction);
			float num2 = 1f - num / 180f;
			if (num2 > clampWeight)
			{
				return direction;
			}
			float num3 = (clampWeight > 0f) ? Mathf.Clamp(1f - (clampWeight - num2) / (1f - num2), 0f, 1f) : 1f;
			float num4 = (clampWeight > 0f) ? Mathf.Clamp(num2 / clampWeight, 0f, 1f) : 1f;
			for (int i = 0; i < clampSmoothing; i++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			return Vector3.Slerp(normalDirection, direction, num4 * num3);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0003A3C8 File Offset: 0x000385C8
		public static Vector3 ClampDirection(Vector3 direction, Vector3 normalDirection, float clampWeight, int clampSmoothing, out bool changed)
		{
			changed = false;
			if (clampWeight <= 0f)
			{
				return direction;
			}
			if (clampWeight >= 1f)
			{
				changed = true;
				return normalDirection;
			}
			float num = Vector3.Angle(normalDirection, direction);
			float num2 = 1f - num / 180f;
			if (num2 > clampWeight)
			{
				return direction;
			}
			changed = true;
			float num3 = (clampWeight > 0f) ? Mathf.Clamp(1f - (clampWeight - num2) / (1f - num2), 0f, 1f) : 1f;
			float num4 = (clampWeight > 0f) ? Mathf.Clamp(num2 / clampWeight, 0f, 1f) : 1f;
			for (int i = 0; i < clampSmoothing; i++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			return Vector3.Slerp(normalDirection, direction, num4 * num3);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0003A490 File Offset: 0x00038690
		public static Vector3 ClampDirection(Vector3 direction, Vector3 normalDirection, float clampWeight, int clampSmoothing, out float clampValue)
		{
			clampValue = 1f;
			if (clampWeight <= 0f)
			{
				return direction;
			}
			if (clampWeight >= 1f)
			{
				return normalDirection;
			}
			float num = Vector3.Angle(normalDirection, direction);
			float num2 = 1f - num / 180f;
			if (num2 > clampWeight)
			{
				clampValue = 0f;
				return direction;
			}
			float num3 = (clampWeight > 0f) ? Mathf.Clamp(1f - (clampWeight - num2) / (1f - num2), 0f, 1f) : 1f;
			float num4 = (clampWeight > 0f) ? Mathf.Clamp(num2 / clampWeight, 0f, 1f) : 1f;
			for (int i = 0; i < clampSmoothing; i++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			float num5 = num4 * num3;
			clampValue = 1f - num5;
			return Vector3.Slerp(normalDirection, direction, num5);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003A56C File Offset: 0x0003876C
		public static Vector3 LineToPlane(Vector3 origin, Vector3 direction, Vector3 planeNormal, Vector3 planePoint)
		{
			float num = Vector3.Dot(planePoint - origin, planeNormal);
			float num2 = Vector3.Dot(direction, planeNormal);
			if (num2 == 0f)
			{
				return Vector3.zero;
			}
			float d = num / num2;
			return origin + direction.normalized * d;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003A5B4 File Offset: 0x000387B4
		public static Vector3 PointToPlane(Vector3 point, Vector3 planePosition, Vector3 planeNormal)
		{
			if (planeNormal == Vector3.up)
			{
				return new Vector3(point.x, planePosition.y, point.z);
			}
			Vector3 onNormal = point - planePosition;
			Vector3 vector = planeNormal;
			Vector3.OrthoNormalize(ref vector, ref onNormal);
			return planePosition + Vector3.Project(point - planePosition, onNormal);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0003A60C File Offset: 0x0003880C
		public static Vector3 TransformPointUnscaled(Transform t, Vector3 point)
		{
			return t.position + t.rotation * point;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003A625 File Offset: 0x00038825
		public static Vector3 InverseTransformPointUnscaled(Transform t, Vector3 point)
		{
			return Quaternion.Inverse(t.rotation) * (point - t.position);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003A643 File Offset: 0x00038843
		public static Vector3 InverseTransformPoint(Vector3 tPos, Quaternion tRot, Vector3 tScale, Vector3 point)
		{
			return V3Tools.Div(Quaternion.Inverse(tRot) * (point - tPos), tScale);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0003A65D File Offset: 0x0003885D
		public static Vector3 TransformPoint(Vector3 tPos, Quaternion tRot, Vector3 tScale, Vector3 point)
		{
			return tPos + Vector3.Scale(tRot * point, tScale);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0003A672 File Offset: 0x00038872
		public static Vector3 Div(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
		}
	}
}
