using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal struct BezierPoint
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000029E8 File Offset: 0x00000BE8
		public BezierPoint(Vector3 position, Vector3 tangentIn, Vector3 tangentOut, Quaternion rotation)
		{
			this.position = position;
			this.tangentIn = tangentIn;
			this.tangentOut = tangentOut;
			this.rotation = rotation;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A08 File Offset: 0x00000C08
		public void EnforceTangentMode(BezierTangentDirection master, BezierTangentMode mode)
		{
			if (mode != BezierTangentMode.Aligned)
			{
				if (mode == BezierTangentMode.Mirrored)
				{
					if (master == BezierTangentDirection.In)
					{
						this.tangentOut = this.position - (this.tangentIn - this.position);
						return;
					}
					this.tangentIn = this.position - (this.tangentOut - this.position);
				}
				return;
			}
			if (master == BezierTangentDirection.In)
			{
				this.tangentOut = this.position + (this.tangentOut - this.position).normalized * (this.tangentIn - this.position).magnitude;
				return;
			}
			this.tangentIn = this.position + (this.tangentIn - this.position).normalized * (this.tangentOut - this.position).magnitude;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B00 File Offset: 0x00000D00
		public void SetPosition(Vector3 position)
		{
			Vector3 b = position - this.position;
			this.position = position;
			this.tangentIn += b;
			this.tangentOut += b;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B45 File Offset: 0x00000D45
		public void SetTangentIn(Vector3 tangent, BezierTangentMode mode)
		{
			this.tangentIn = tangent;
			this.EnforceTangentMode(BezierTangentDirection.In, mode);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B56 File Offset: 0x00000D56
		public void SetTangentOut(Vector3 tangent, BezierTangentMode mode)
		{
			this.tangentOut = tangent;
			this.EnforceTangentMode(BezierTangentDirection.Out, mode);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B68 File Offset: 0x00000D68
		public static Vector3 QuadraticPosition(BezierPoint a, BezierPoint b, float t)
		{
			float x = (1f - t) * (1f - t) * a.position.x + 2f * (1f - t) * t * a.tangentOut.x + t * t * b.position.x;
			float y = (1f - t) * (1f - t) * a.position.y + 2f * (1f - t) * t * a.tangentOut.y + t * t * b.position.y;
			float z = (1f - t) * (1f - t) * a.position.z + 2f * (1f - t) * t * a.tangentOut.z + t * t * b.position.z;
			return new Vector3(x, y, z);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C54 File Offset: 0x00000E54
		public static Vector3 CubicPosition(BezierPoint a, BezierPoint b, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * a.position + 3f * num * num * t * a.tangentOut + 3f * num * t * t * b.tangentIn + t * t * t * b.position;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CCC File Offset: 0x00000ECC
		public static Vector3 GetLookDirection(IList<BezierPoint> points, int index, int previous, int next)
		{
			if (previous < 0)
			{
				return (points[index].position - BezierPoint.QuadraticPosition(points[index], points[next], 0.1f)).normalized;
			}
			if (next < 0)
			{
				return (BezierPoint.QuadraticPosition(points[index], points[previous], 0.1f) - points[index].position).normalized;
			}
			if (next > -1 && previous > -1)
			{
				Vector3 normalized = (BezierPoint.QuadraticPosition(points[index], points[previous], 0.1f) - points[index].position).normalized;
				Vector3 normalized2 = (BezierPoint.QuadraticPosition(points[index], points[next], 0.1f) - points[index].position).normalized;
				return ((normalized + normalized2) * 0.5f).normalized;
			}
			return Vector3.forward;
		}

		// Token: 0x04000013 RID: 19
		public Vector3 position;

		// Token: 0x04000014 RID: 20
		public Vector3 tangentIn;

		// Token: 0x04000015 RID: 21
		public Vector3 tangentOut;

		// Token: 0x04000016 RID: 22
		public Quaternion rotation;
	}
}
