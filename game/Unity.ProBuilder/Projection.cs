using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000047 RID: 71
	public static class Projection
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00019690 File Offset: 0x00017890
		public static Vector2[] PlanarProject(IList<Vector3> positions, IList<int> indexes = null)
		{
			return Projection.PlanarProject(positions, indexes, Projection.FindBestPlane(positions, indexes).normal);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000196B4 File Offset: 0x000178B4
		public static Vector2[] PlanarProject(IList<Vector3> positions, IList<int> indexes, Vector3 direction)
		{
			List<Vector2> list = new List<Vector2>((indexes != null) ? indexes.Count : positions.Count);
			Projection.PlanarProject(positions, indexes, direction, list);
			return list.ToArray();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000196E8 File Offset: 0x000178E8
		internal static void PlanarProject(IList<Vector3> positions, IList<int> indexes, Vector3 direction, List<Vector2> results)
		{
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			Vector3 vector = Math.EnsureUnitVector(direction);
			Vector3 tangentToAxis = Projection.GetTangentToAxis(Projection.VectorToProjectionAxis(vector));
			int num = (indexes == null) ? positions.Count : indexes.Count;
			results.Clear();
			Vector3 lhs = Vector3.Cross(vector, tangentToAxis);
			Vector3 lhs2 = Vector3.Cross(lhs, vector);
			lhs.Normalize();
			lhs2.Normalize();
			if (indexes != null)
			{
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					results.Add(new Vector2(Vector3.Dot(lhs, positions[indexes[i]]), Vector3.Dot(lhs2, positions[indexes[i]])));
					i++;
				}
				return;
			}
			int j = 0;
			int num3 = num;
			while (j < num3)
			{
				results.Add(new Vector2(Vector3.Dot(lhs, positions[j]), Vector3.Dot(lhs2, positions[j])));
				j++;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000197E4 File Offset: 0x000179E4
		internal static void PlanarProject(ProBuilderMesh mesh, int textureGroup, AutoUnwrapSettings unwrapSettings)
		{
			bool useWorldSpace = unwrapSettings.useWorldSpace;
			Transform transform = null;
			Face[] facesInternal = mesh.facesInternal;
			Vector3 vector = Vector3.zero;
			int i = 0;
			int num = facesInternal.Length;
			while (i < num)
			{
				if (facesInternal[i].textureGroup == textureGroup)
				{
					Vector3 b = Math.Normal(mesh, facesInternal[i]);
					vector += b;
				}
				i++;
			}
			if (useWorldSpace)
			{
				transform = mesh.transform;
				vector = transform.TransformDirection(vector);
			}
			Vector3 tangentToAxis = Projection.GetTangentToAxis(Projection.VectorToProjectionAxis(vector));
			Vector3 lhs = Vector3.Cross(vector, tangentToAxis);
			Vector3 lhs2 = Vector3.Cross(lhs, vector);
			lhs.Normalize();
			lhs2.Normalize();
			Vector3[] positionsInternal = mesh.positionsInternal;
			Vector2[] texturesInternal = mesh.texturesInternal;
			int j = 0;
			int num2 = facesInternal.Length;
			while (j < num2)
			{
				if (facesInternal[j].textureGroup == textureGroup)
				{
					int[] distinctIndexesInternal = facesInternal[j].distinctIndexesInternal;
					int k = 0;
					int num3 = distinctIndexesInternal.Length;
					while (k < num3)
					{
						Vector3 rhs = useWorldSpace ? transform.TransformPoint(positionsInternal[distinctIndexesInternal[k]]) : positionsInternal[distinctIndexesInternal[k]];
						texturesInternal[distinctIndexesInternal[k]].x = Vector3.Dot(lhs, rhs);
						texturesInternal[distinctIndexesInternal[k]].y = Vector3.Dot(lhs2, rhs);
						k++;
					}
				}
				j++;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00019934 File Offset: 0x00017B34
		internal static void PlanarProject(ProBuilderMesh mesh, Face face, Vector3 projection = default(Vector3))
		{
			Vector3 vector = Math.EnsureUnitVector(Math.Normal(mesh, face));
			Transform transform = null;
			bool useWorldSpace = face.uv.useWorldSpace;
			if (useWorldSpace)
			{
				transform = mesh.transform;
				vector = transform.TransformDirection(vector);
			}
			Vector3 vector2 = projection;
			if (vector2 == Vector3.zero)
			{
				vector2 = Projection.GetTangentToAxis(Projection.VectorToProjectionAxis(vector));
			}
			Vector3 lhs = Vector3.Cross(vector, vector2);
			Vector3 lhs2 = Vector3.Cross(lhs, vector);
			lhs.Normalize();
			lhs2.Normalize();
			Vector3[] positionsInternal = mesh.positionsInternal;
			Vector2[] texturesInternal = mesh.texturesInternal;
			int[] distinctIndexesInternal = face.distinctIndexesInternal;
			int i = 0;
			int num = distinctIndexesInternal.Length;
			while (i < num)
			{
				Vector3 rhs = useWorldSpace ? transform.TransformPoint(positionsInternal[distinctIndexesInternal[i]]) : positionsInternal[distinctIndexesInternal[i]];
				texturesInternal[distinctIndexesInternal[i]].x = Vector3.Dot(lhs, rhs);
				texturesInternal[distinctIndexesInternal[i]].y = Vector3.Dot(lhs2, rhs);
				i++;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00019A38 File Offset: 0x00017C38
		internal static Vector2[] SphericalProject(IList<Vector3> vertices, IList<int> indexes = null)
		{
			int num = (indexes == null) ? vertices.Count : indexes.Count;
			Vector2[] array = new Vector2[num];
			Vector3 b = Math.Average(vertices, indexes);
			for (int i = 0; i < num; i++)
			{
				int index = (indexes == null) ? i : indexes[i];
				Vector3 vector = vertices[index] - b;
				vector.Normalize();
				array[i].x = 0.5f + Mathf.Atan2(vector.z, vector.x) / 6.2831855f;
				array[i].y = 0.5f - Mathf.Asin(vector.y) / 3.1415927f;
			}
			return array;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00019AE8 File Offset: 0x00017CE8
		internal static IList<Vector2> Sort(IList<Vector2> verts, SortMethod method = SortMethod.CounterClockwise)
		{
			Vector2 b2 = Math.Average(verts, null);
			Vector2 up = Vector2.up;
			int count = verts.Count;
			List<SimpleTuple<float, Vector2>> list = new List<SimpleTuple<float, Vector2>>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(new SimpleTuple<float, Vector2>(Math.SignedAngle(up, verts[i] - b2), verts[i]));
			}
			list.Sort(delegate(SimpleTuple<float, Vector2> a, SimpleTuple<float, Vector2> b)
			{
				if (a.item1 >= b.item1)
				{
					return 1;
				}
				return -1;
			});
			IList<Vector2> list2 = (from x in list
			select x.item2).ToList<Vector2>();
			if (method == SortMethod.Clockwise)
			{
				list2 = list2.Reverse<Vector2>().ToList<Vector2>();
			}
			return list2;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00019BAC File Offset: 0x00017DAC
		internal static Vector3 GetTangentToAxis(ProjectionAxis axis)
		{
			switch (axis)
			{
			case ProjectionAxis.X:
			case ProjectionAxis.XNegative:
				return Vector3.up;
			case ProjectionAxis.Y:
			case ProjectionAxis.YNegative:
				return Vector3.forward;
			case ProjectionAxis.Z:
			case ProjectionAxis.ZNegative:
				return Vector3.up;
			default:
				return Vector3.up;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00019BE8 File Offset: 0x00017DE8
		internal static Vector3 ProjectionAxisToVector(ProjectionAxis axis)
		{
			switch (axis)
			{
			case ProjectionAxis.X:
				return Vector3.right;
			case ProjectionAxis.Y:
				return Vector3.up;
			case ProjectionAxis.Z:
				return Vector3.forward;
			case ProjectionAxis.XNegative:
				return -Vector3.right;
			case ProjectionAxis.YNegative:
				return -Vector3.up;
			case ProjectionAxis.ZNegative:
				return -Vector3.forward;
			default:
				return Vector3.forward;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00019C50 File Offset: 0x00017E50
		internal static ProjectionAxis VectorToProjectionAxis(Vector3 direction)
		{
			float num = Math.Abs(direction.x);
			float num2 = Math.Abs(direction.y);
			float num3 = Math.Abs(direction.z);
			if (!num.Approx(num2, 0.0001f) && num > num2 && !num.Approx(num3, 0.0001f) && num > num3)
			{
				if (direction.x <= 0f)
				{
					return ProjectionAxis.XNegative;
				}
				return ProjectionAxis.X;
			}
			else if (!num2.Approx(num3, 0.0001f) && num2 > num3)
			{
				if (direction.y <= 0f)
				{
					return ProjectionAxis.YNegative;
				}
				return ProjectionAxis.Y;
			}
			else
			{
				if (direction.z <= 0f)
				{
					return ProjectionAxis.ZNegative;
				}
				return ProjectionAxis.Z;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00019CEC File Offset: 0x00017EEC
		public static Plane FindBestPlane(IList<Vector3> points, IList<int> indexes = null)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			bool flag = indexes != null && indexes.Count > 0;
			int num7 = flag ? indexes.Count : points.Count;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int i = 0; i < num7; i++)
			{
				zero.x += points[flag ? indexes[i] : i].x;
				zero.y += points[flag ? indexes[i] : i].y;
				zero.z += points[flag ? indexes[i] : i].z;
			}
			zero.x /= (float)num7;
			zero.y /= (float)num7;
			zero.z /= (float)num7;
			for (int j = 0; j < num7; j++)
			{
				Vector3 vector = points[flag ? indexes[j] : j] - zero;
				num += vector.x * vector.x;
				num2 += vector.x * vector.y;
				num3 += vector.x * vector.z;
				num4 += vector.y * vector.y;
				num5 += vector.y * vector.z;
				num6 += vector.z * vector.z;
			}
			float num8 = num4 * num6 - num5 * num5;
			float num9 = num * num6 - num3 * num3;
			float num10 = num * num4 - num2 * num2;
			if (num8 > num9 && num8 > num10)
			{
				zero2.x = 1f;
				zero2.y = (num3 * num5 - num2 * num6) / num8;
				zero2.z = (num2 * num5 - num3 * num4) / num8;
			}
			else if (num9 > num10)
			{
				zero2.x = (num5 * num3 - num2 * num6) / num9;
				zero2.y = 1f;
				zero2.z = (num2 * num3 - num5 * num) / num9;
			}
			else
			{
				zero2.x = (num5 * num2 - num3 * num4) / num10;
				zero2.y = (num3 * num2 - num5 * num) / num10;
				zero2.z = 1f;
			}
			zero2.Normalize();
			return new Plane(zero2, zero);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00019F8C File Offset: 0x0001818C
		internal static Plane FindBestPlane(ProBuilderMesh mesh, int textureGroup)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Vector3 zero = Vector3.zero;
			int num7 = 0;
			Vector3[] positionsInternal = mesh.positionsInternal;
			int faceCount = mesh.faceCount;
			Face[] facesInternal = mesh.facesInternal;
			for (int i = 0; i < faceCount; i++)
			{
				if (facesInternal[i].textureGroup == textureGroup)
				{
					int[] indexesInternal = facesInternal[i].indexesInternal;
					int j = 0;
					int num8 = indexesInternal.Length;
					while (j < num8)
					{
						zero.x += positionsInternal[indexesInternal[j]].x;
						zero.y += positionsInternal[indexesInternal[j]].y;
						zero.z += positionsInternal[indexesInternal[j]].z;
						num7++;
						j++;
					}
				}
			}
			zero.x /= (float)num7;
			zero.y /= (float)num7;
			zero.z /= (float)num7;
			for (int k = 0; k < faceCount; k++)
			{
				if (facesInternal[k].textureGroup == textureGroup)
				{
					int[] indexesInternal2 = facesInternal[k].indexesInternal;
					int l = 0;
					int num9 = indexesInternal2.Length;
					while (l < num9)
					{
						Vector3 vector = positionsInternal[indexesInternal2[l]] - zero;
						num += vector.x * vector.x;
						num2 += vector.x * vector.y;
						num3 += vector.x * vector.z;
						num4 += vector.y * vector.y;
						num5 += vector.y * vector.z;
						num6 += vector.z * vector.z;
						l++;
					}
				}
			}
			float num10 = num4 * num6 - num5 * num5;
			float num11 = num * num6 - num3 * num3;
			float num12 = num * num4 - num2 * num2;
			Vector3 zero2 = Vector3.zero;
			if (num10 > num11 && num10 > num12)
			{
				zero2.x = 1f;
				zero2.y = (num3 * num5 - num2 * num6) / num10;
				zero2.z = (num2 * num5 - num3 * num4) / num10;
			}
			else if (num11 > num12)
			{
				zero2.x = (num5 * num3 - num2 * num6) / num11;
				zero2.y = 1f;
				zero2.z = (num2 * num3 - num5 * num) / num11;
			}
			else
			{
				zero2.x = (num5 * num2 - num3 * num4) / num12;
				zero2.y = (num3 * num2 - num5 * num) / num12;
				zero2.z = 1f;
			}
			zero2.Normalize();
			return new Plane(zero2, zero);
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600054B RID: 1355 RVA: 0x00035C69 File Offset: 0x00033E69
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x00035C75 File Offset: 0x00033E75
			public <>c()
			{
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x00035C7D File Offset: 0x00033E7D
			internal int <Sort>b__6_0(SimpleTuple<float, Vector2> a, SimpleTuple<float, Vector2> b)
			{
				if (a.item1 >= b.item1)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00035C92 File Offset: 0x00033E92
			internal Vector2 <Sort>b__6_1(SimpleTuple<float, Vector2> x)
			{
				return x.item2;
			}

			// Token: 0x040002B3 RID: 691
			public static readonly Projection.<>c <>9 = new Projection.<>c();

			// Token: 0x040002B4 RID: 692
			public static Comparison<SimpleTuple<float, Vector2>> <>9__6_0;

			// Token: 0x040002B5 RID: 693
			public static Func<SimpleTuple<float, Vector2>, Vector2> <>9__6_1;
		}
	}
}
