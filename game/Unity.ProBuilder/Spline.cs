using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000059 RID: 89
	internal static class Spline
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0002095C File Offset: 0x0001EB5C
		internal static ProBuilderMesh Extrude(IList<BezierPoint> points, float radius = 0.5f, int columns = 32, int rows = 16, bool closeLoop = false, bool smooth = true)
		{
			ProBuilderMesh result = null;
			Spline.Extrude(points, radius, columns, rows, closeLoop, smooth, ref result);
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0002097C File Offset: 0x0001EB7C
		internal static void Extrude(IList<BezierPoint> bezierPoints, float radius, int columns, int rows, bool closeLoop, bool smooth, ref ProBuilderMesh target)
		{
			List<Quaternion> list = new List<Quaternion>();
			Spline.Extrude(Spline.GetControlPoints(bezierPoints, columns, closeLoop, list), radius, rows, closeLoop, smooth, ref target, list);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000209A8 File Offset: 0x0001EBA8
		internal static List<Vector3> GetControlPoints(IList<BezierPoint> bezierPoints, int subdivisionsPerSegment, bool closeLoop, List<Quaternion> rotations)
		{
			int count = bezierPoints.Count;
			List<Vector3> list = new List<Vector3>(subdivisionsPerSegment * count);
			if (rotations != null)
			{
				rotations.Clear();
				rotations.Capacity = subdivisionsPerSegment * count;
			}
			int num = closeLoop ? count : (count - 1);
			for (int i = 0; i < num; i++)
			{
				int num2 = (!closeLoop && i >= count - 2) ? (subdivisionsPerSegment + 1) : subdivisionsPerSegment;
				for (int j = 0; j < num2; j++)
				{
					float num3 = (float)subdivisionsPerSegment;
					list.Add(BezierPoint.CubicPosition(bezierPoints[i], bezierPoints[(i + 1) % count], (float)j / num3));
					if (rotations != null)
					{
						rotations.Add(Quaternion.Slerp(bezierPoints[i].rotation, bezierPoints[(i + 1) % count].rotation, (float)j / (float)(num2 - 1)));
					}
				}
			}
			return list;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00020A7C File Offset: 0x0001EC7C
		internal static void Extrude(IList<Vector3> points, float radius, int radiusRows, bool closeLoop, bool smooth, ref ProBuilderMesh target, IList<Quaternion> pointRotations = null)
		{
			if (points == null || points.Count < 2)
			{
				return;
			}
			int count = points.Count;
			int num = Math.Max(3, radiusRows);
			int segments = num + 1;
			int num2 = num * 2;
			int num3 = (closeLoop ? count : (count - 1)) * 2 * num2;
			bool flag = false;
			bool flag2 = pointRotations != null && pointRotations.Count == points.Count;
			Vector3[] array = new Vector3[num3];
			Face[] array2 = flag ? null : new Face[(closeLoop ? count : (count - 1)) * num];
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = closeLoop ? count : (count - 1);
			for (int i = 0; i < num7; i++)
			{
				float num8;
				Quaternion quaternion = Spline.GetRingRotation(points, i, closeLoop, out num8);
				float num9;
				Quaternion quaternion2 = Spline.GetRingRotation(points, (i + 1) % count, closeLoop, out num9);
				if (flag2)
				{
					quaternion *= pointRotations[i];
					quaternion2 *= pointRotations[(i + 1) % count];
				}
				Array sourceArray = Spline.VertexRing(quaternion, points[i], radius, segments);
				Vector3[] sourceArray2 = Spline.VertexRing(quaternion2, points[(i + 1) % count], radius, segments);
				Array.Copy(sourceArray, 0, array, num6, num2);
				num6 += num2;
				Array.Copy(sourceArray2, 0, array, num6, num2);
				num6 += num2;
				if (!flag)
				{
					for (int j = 0; j < num2; j += 2)
					{
						array2[num5] = new Face(new int[]
						{
							num4,
							num4 + 1,
							num4 + num2,
							num4 + num2,
							num4 + 1,
							num4 + num2 + 1
						});
						if (smooth)
						{
							array2[num5].smoothingGroup = 2;
						}
						num5++;
						num4 += 2;
					}
					num4 += num2;
				}
			}
			if (!(target != null))
			{
				target = ProBuilderMesh.Create(array, array2);
				return;
			}
			if (array2 != null)
			{
				target.RebuildWithPositionsAndFaces(array, array2);
				return;
			}
			target.positions = array;
			target.ToMesh(MeshTopology.Triangles);
			target.Refresh(RefreshMask.UV | RefreshMask.Colors | RefreshMask.Normals | RefreshMask.Tangents);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00020C6C File Offset: 0x0001EE6C
		private static Quaternion GetRingRotation(IList<Vector3> points, int i, bool closeLoop, out float secant)
		{
			int count = points.Count;
			Vector3 vector;
			if (closeLoop || (i > 0 && i < count - 1))
			{
				int index = (i < 1) ? (count - 1) : (i - 1);
				int index2 = (i + 1) % count;
				Vector3 normalized = (points[i] - points[index]).normalized;
				Vector3 normalized2 = (points[index2] - points[i]).normalized;
				vector = (normalized + normalized2) * 0.5f;
				secant = Math.Secant(Vector3.Angle(normalized, vector) * 0.017453292f);
			}
			else
			{
				if (i < 1)
				{
					vector = points[i + 1] - points[i];
				}
				else
				{
					vector = points[i] - points[i - 1];
				}
				secant = 1f;
			}
			vector.Normalize();
			if (vector.Approx3(Vector3.up, 0.0001f) || vector.Approx3(Vector3.zero, 0.0001f))
			{
				return Quaternion.identity;
			}
			return Quaternion.LookRotation(vector);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00020D80 File Offset: 0x0001EF80
		private static Vector3[] VertexRing(Quaternion orientation, Vector3 offset, float radius, int segments)
		{
			Vector3[] array = new Vector3[segments * 2];
			for (int i = 0; i < segments; i++)
			{
				float f = (float)i / (float)(segments - 1) * 360f * 0.017453292f;
				float f2 = (float)((i + 1) % segments) / (float)(segments - 1) * 360f * 0.017453292f;
				array[i * 2] = offset + orientation * new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f);
				array[i * 2 + 1] = offset + orientation * new Vector3(Mathf.Cos(f2) * radius, Mathf.Sin(f2) * radius, 0f);
			}
			return array;
		}
	}
}
