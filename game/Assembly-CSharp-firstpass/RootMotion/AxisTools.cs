using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B3 RID: 179
	public class AxisTools
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00036E86 File Offset: 0x00035086
		public static Vector3 ToVector3(Axis axis)
		{
			if (axis == Axis.X)
			{
				return Vector3.right;
			}
			if (axis == Axis.Y)
			{
				return Vector3.up;
			}
			return Vector3.forward;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00036EA0 File Offset: 0x000350A0
		public static Axis ToAxis(Vector3 v)
		{
			float num = Mathf.Abs(v.x);
			float num2 = Mathf.Abs(v.y);
			float num3 = Mathf.Abs(v.z);
			Axis result = Axis.X;
			if (num2 > num && num2 > num3)
			{
				result = Axis.Y;
			}
			if (num3 > num && num3 > num2)
			{
				result = Axis.Z;
			}
			return result;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00036EE8 File Offset: 0x000350E8
		public static Axis GetAxisToPoint(Transform t, Vector3 worldPosition)
		{
			Vector3 axisVectorToPoint = AxisTools.GetAxisVectorToPoint(t, worldPosition);
			if (axisVectorToPoint == Vector3.right)
			{
				return Axis.X;
			}
			if (axisVectorToPoint == Vector3.up)
			{
				return Axis.Y;
			}
			return Axis.Z;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00036F1C File Offset: 0x0003511C
		public static Axis GetAxisToDirection(Transform t, Vector3 direction)
		{
			Vector3 axisVectorToDirection = AxisTools.GetAxisVectorToDirection(t, direction);
			if (axisVectorToDirection == Vector3.right)
			{
				return Axis.X;
			}
			if (axisVectorToDirection == Vector3.up)
			{
				return Axis.Y;
			}
			return Axis.Z;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00036F50 File Offset: 0x00035150
		public static Vector3 GetAxisVectorToPoint(Transform t, Vector3 worldPosition)
		{
			return AxisTools.GetAxisVectorToDirection(t, worldPosition - t.position);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00036F64 File Offset: 0x00035164
		public static Vector3 GetAxisVectorToDirection(Transform t, Vector3 direction)
		{
			return AxisTools.GetAxisVectorToDirection(t.rotation, direction);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00036F74 File Offset: 0x00035174
		public static Vector3 GetAxisVectorToDirection(Quaternion r, Vector3 direction)
		{
			direction = direction.normalized;
			Vector3 result = Vector3.right;
			float num = Mathf.Abs(Vector3.Dot(Vector3.Normalize(r * Vector3.right), direction));
			float num2 = Mathf.Abs(Vector3.Dot(Vector3.Normalize(r * Vector3.up), direction));
			if (num2 > num)
			{
				result = Vector3.up;
			}
			float num3 = Mathf.Abs(Vector3.Dot(Vector3.Normalize(r * Vector3.forward), direction));
			if (num3 > num && num3 > num2)
			{
				result = Vector3.forward;
			}
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00036FFD File Offset: 0x000351FD
		public AxisTools()
		{
		}
	}
}
