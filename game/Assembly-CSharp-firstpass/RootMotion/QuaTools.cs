using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000BF RID: 191
	public static class QuaTools
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x000397FC File Offset: 0x000379FC
		public static float GetYaw(Quaternion space, Vector3 forward)
		{
			Vector3 vector = Quaternion.Inverse(space) * forward;
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0003982D File Offset: 0x00037A2D
		public static float GetPitch(Quaternion space, Vector3 forward)
		{
			forward = forward.normalized;
			return -Mathf.Asin((Quaternion.Inverse(space) * forward).y) * 57.29578f;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00039858 File Offset: 0x00037A58
		public static float GetBank(Quaternion space, Vector3 forward, Vector3 up)
		{
			Vector3 forward2 = space * Vector3.up;
			Quaternion rotation = Quaternion.Inverse(space);
			forward = rotation * forward;
			up = rotation * up;
			up = Quaternion.Inverse(Quaternion.LookRotation(forward2, forward)) * up;
			return Mathf.Atan2(up.x, up.z) * 57.29578f;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000398B4 File Offset: 0x00037AB4
		public static float GetYaw(Quaternion space, Quaternion rotation)
		{
			Vector3 vector = Quaternion.Inverse(space) * (rotation * Vector3.forward);
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000398EF File Offset: 0x00037AEF
		public static float GetPitch(Quaternion space, Quaternion rotation)
		{
			return -Mathf.Asin((Quaternion.Inverse(space) * (rotation * Vector3.forward)).y) * 57.29578f;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00039918 File Offset: 0x00037B18
		public static float GetBank(Quaternion space, Quaternion rotation)
		{
			Vector3 forward = space * Vector3.up;
			Quaternion rotation2 = Quaternion.Inverse(space);
			Vector3 upwards = rotation2 * (rotation * Vector3.forward);
			Vector3 vector = rotation2 * (rotation * Vector3.up);
			vector = Quaternion.Inverse(Quaternion.LookRotation(forward, upwards)) * vector;
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00039982 File Offset: 0x00037B82
		public static Quaternion Lerp(Quaternion fromRotation, Quaternion toRotation, float weight)
		{
			if (weight <= 0f)
			{
				return fromRotation;
			}
			if (weight >= 1f)
			{
				return toRotation;
			}
			return Quaternion.Lerp(fromRotation, toRotation, weight);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000399A0 File Offset: 0x00037BA0
		public static Quaternion Slerp(Quaternion fromRotation, Quaternion toRotation, float weight)
		{
			if (weight <= 0f)
			{
				return fromRotation;
			}
			if (weight >= 1f)
			{
				return toRotation;
			}
			return Quaternion.Slerp(fromRotation, toRotation, weight);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000399BE File Offset: 0x00037BBE
		public static Quaternion LinearBlend(Quaternion q, float weight)
		{
			if (weight <= 0f)
			{
				return Quaternion.identity;
			}
			if (weight >= 1f)
			{
				return q;
			}
			return Quaternion.Lerp(Quaternion.identity, q, weight);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000399E4 File Offset: 0x00037BE4
		public static Quaternion SphericalBlend(Quaternion q, float weight)
		{
			if (weight <= 0f)
			{
				return Quaternion.identity;
			}
			if (weight >= 1f)
			{
				return q;
			}
			return Quaternion.Slerp(Quaternion.identity, q, weight);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00039A0C File Offset: 0x00037C0C
		public static Quaternion FromToAroundAxis(Vector3 fromDirection, Vector3 toDirection, Vector3 axis)
		{
			Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection);
			float num = 0f;
			Vector3 zero = Vector3.zero;
			quaternion.ToAngleAxis(out num, out zero);
			if (Vector3.Dot(zero, axis) < 0f)
			{
				num = -num;
			}
			return Quaternion.AngleAxis(num, axis);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00039A50 File Offset: 0x00037C50
		public static Quaternion RotationToLocalSpace(Quaternion space, Quaternion rotation)
		{
			return Quaternion.Inverse(Quaternion.Inverse(space) * rotation);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00039A63 File Offset: 0x00037C63
		public static Quaternion FromToRotation(Quaternion from, Quaternion to)
		{
			if (to == from)
			{
				return Quaternion.identity;
			}
			return to * Quaternion.Inverse(from);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00039A80 File Offset: 0x00037C80
		public static Vector3 GetAxis(Vector3 v)
		{
			Vector3 vector = Vector3.right;
			bool flag = false;
			float num = Vector3.Dot(v, Vector3.right);
			float num2 = Mathf.Abs(num);
			if (num < 0f)
			{
				flag = true;
			}
			float num3 = Vector3.Dot(v, Vector3.up);
			float num4 = Mathf.Abs(num3);
			if (num4 > num2)
			{
				num2 = num4;
				vector = Vector3.up;
				flag = (num3 < 0f);
			}
			float num5 = Vector3.Dot(v, Vector3.forward);
			num4 = Mathf.Abs(num5);
			if (num4 > num2)
			{
				vector = Vector3.forward;
				flag = (num5 < 0f);
			}
			if (flag)
			{
				vector = -vector;
			}
			return vector;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00039B14 File Offset: 0x00037D14
		public static Quaternion ClampRotation(Quaternion rotation, float clampWeight, int clampSmoothing)
		{
			if (clampWeight >= 1f)
			{
				return Quaternion.identity;
			}
			if (clampWeight <= 0f)
			{
				return rotation;
			}
			float num = Quaternion.Angle(Quaternion.identity, rotation);
			float num2 = 1f - num / 180f;
			float num3 = Mathf.Clamp(1f - (clampWeight - num2) / (1f - num2), 0f, 1f);
			float num4 = Mathf.Clamp(num2 / clampWeight, 0f, 1f);
			for (int i = 0; i < clampSmoothing; i++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			return Quaternion.Slerp(Quaternion.identity, rotation, num4 * num3);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00039BB8 File Offset: 0x00037DB8
		public static float ClampAngle(float angle, float clampWeight, int clampSmoothing)
		{
			if (clampWeight >= 1f)
			{
				return 0f;
			}
			if (clampWeight <= 0f)
			{
				return angle;
			}
			float num = 1f - Mathf.Abs(angle) / 180f;
			float num2 = Mathf.Clamp(1f - (clampWeight - num) / (1f - num), 0f, 1f);
			float num3 = Mathf.Clamp(num / clampWeight, 0f, 1f);
			for (int i = 0; i < clampSmoothing; i++)
			{
				num3 = Mathf.Sin(num3 * 3.1415927f * 0.5f);
			}
			return Mathf.Lerp(0f, angle, num3 * num2);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00039C54 File Offset: 0x00037E54
		public static Quaternion MatchRotation(Quaternion targetRotation, Vector3 targetforwardAxis, Vector3 targetUpAxis, Vector3 forwardAxis, Vector3 upAxis)
		{
			Quaternion rotation = Quaternion.LookRotation(forwardAxis, upAxis);
			Quaternion rhs = Quaternion.LookRotation(targetforwardAxis, targetUpAxis);
			return targetRotation * rhs * Quaternion.Inverse(rotation);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00039C84 File Offset: 0x00037E84
		public static Vector3 ToBiPolar(Vector3 euler)
		{
			return new Vector3(QuaTools.ToBiPolar(euler.x), QuaTools.ToBiPolar(euler.y), QuaTools.ToBiPolar(euler.z));
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00039CAC File Offset: 0x00037EAC
		public static float ToBiPolar(float angle)
		{
			angle %= 360f;
			if (angle >= 180f)
			{
				return angle - 360f;
			}
			if (angle <= -180f)
			{
				return angle + 360f;
			}
			return angle;
		}
	}
}
