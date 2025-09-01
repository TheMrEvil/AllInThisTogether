using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000065 RID: 101
	public static class VertexPositioning
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x00023CDC File Offset: 0x00021EDC
		public static Vector3[] VerticesInWorldSpace(this ProBuilderMesh mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			int vertexCount = mesh.vertexCount;
			Vector3[] array = new Vector3[vertexCount];
			Vector3[] positionsInternal = mesh.positionsInternal;
			for (int i = 0; i < vertexCount; i++)
			{
				array[i] = mesh.transform.TransformPoint(positionsInternal[i]);
			}
			return array;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00023D38 File Offset: 0x00021F38
		public static void TranslateVerticesInWorldSpace(this ProBuilderMesh mesh, int[] indexes, Vector3 offset)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			mesh.TranslateVerticesInWorldSpace(indexes, offset, 0f, false);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00023D5C File Offset: 0x00021F5C
		internal static void TranslateVerticesInWorldSpace(this ProBuilderMesh mesh, int[] indexes, Vector3 offset, float snapValue, bool snapAxisOnly)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			mesh.GetCoincidentVertices(indexes, VertexPositioning.s_CoincidentVertices);
			Matrix4x4 worldToLocalMatrix = mesh.transform.worldToLocalMatrix;
			Vector3 b = worldToLocalMatrix * offset;
			Vector3[] positionsInternal = mesh.positionsInternal;
			if (Mathf.Abs(snapValue) > Mathf.Epsilon)
			{
				Matrix4x4 localToWorldMatrix = mesh.transform.localToWorldMatrix;
				Vector3Mask mask = snapAxisOnly ? new Vector3Mask(offset, 0.0001f) : Vector3Mask.XYZ;
				for (int i = 0; i < VertexPositioning.s_CoincidentVertices.Count; i++)
				{
					Vector3 val = localToWorldMatrix.MultiplyPoint3x4(positionsInternal[VertexPositioning.s_CoincidentVertices[i]] + b);
					positionsInternal[VertexPositioning.s_CoincidentVertices[i]] = worldToLocalMatrix.MultiplyPoint3x4(ProBuilderSnapping.Snap(val, mask * snapValue));
				}
			}
			else
			{
				for (int i = 0; i < VertexPositioning.s_CoincidentVertices.Count; i++)
				{
					positionsInternal[VertexPositioning.s_CoincidentVertices[i]] += b;
				}
			}
			mesh.positionsInternal = positionsInternal;
			mesh.mesh.vertices = positionsInternal;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00023E92 File Offset: 0x00022092
		public static void TranslateVertices(this ProBuilderMesh mesh, IEnumerable<int> indexes, Vector3 offset)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			mesh.GetCoincidentVertices(indexes, VertexPositioning.s_CoincidentVertices);
			VertexPositioning.TranslateVerticesInternal(mesh, VertexPositioning.s_CoincidentVertices, offset);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00023EC0 File Offset: 0x000220C0
		public static void TranslateVertices(this ProBuilderMesh mesh, IEnumerable<Edge> edges, Vector3 offset)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			mesh.GetCoincidentVertices(edges, VertexPositioning.s_CoincidentVertices);
			VertexPositioning.TranslateVerticesInternal(mesh, VertexPositioning.s_CoincidentVertices, offset);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00023EEE File Offset: 0x000220EE
		public static void TranslateVertices(this ProBuilderMesh mesh, IEnumerable<Face> faces, Vector3 offset)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			mesh.GetCoincidentVertices(faces, VertexPositioning.s_CoincidentVertices);
			VertexPositioning.TranslateVerticesInternal(mesh, VertexPositioning.s_CoincidentVertices, offset);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00023F1C File Offset: 0x0002211C
		private static void TranslateVerticesInternal(ProBuilderMesh mesh, IEnumerable<int> indices, Vector3 offset)
		{
			Vector3[] positionsInternal = mesh.positionsInternal;
			int i = 0;
			int count = VertexPositioning.s_CoincidentVertices.Count;
			while (i < count)
			{
				positionsInternal[VertexPositioning.s_CoincidentVertices[i]] += offset;
				i++;
			}
			mesh.mesh.vertices = positionsInternal;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00023F78 File Offset: 0x00022178
		public static void SetSharedVertexPosition(this ProBuilderMesh mesh, int sharedVertexHandle, Vector3 position)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Vector3[] positionsInternal = mesh.positionsInternal;
			foreach (int num in mesh.sharedVerticesInternal[sharedVertexHandle])
			{
				positionsInternal[num] = position;
			}
			mesh.positionsInternal = positionsInternal;
			mesh.mesh.vertices = positionsInternal;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00023FF8 File Offset: 0x000221F8
		internal static void SetSharedVertexValues(this ProBuilderMesh mesh, int sharedVertexHandle, Vertex vertex)
		{
			Vertex[] vertices = mesh.GetVertices(null);
			foreach (int num in mesh.sharedVerticesInternal[sharedVertexHandle])
			{
				vertices[num] = vertex;
			}
			mesh.SetVertices(vertices, false);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00024054 File Offset: 0x00022254
		// Note: this type is marked as 'beforefieldinit'.
		static VertexPositioning()
		{
		}

		// Token: 0x04000225 RID: 549
		private static List<int> s_CoincidentVertices = new List<int>();
	}
}
