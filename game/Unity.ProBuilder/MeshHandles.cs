using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000029 RID: 41
	internal static class MeshHandles
	{
		// Token: 0x060001AF RID: 431 RVA: 0x000138E8 File Offset: 0x00011AE8
		internal static void CreateFaceMesh(ProBuilderMesh mesh, Mesh target)
		{
			target.Clear();
			target.vertices = mesh.positionsInternal;
			target.triangles = mesh.selectedFacesInternal.SelectMany((Face x) => x.indexes).ToArray<int>();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0001393C File Offset: 0x00011B3C
		internal static void CreateFaceMeshFromFaces(ProBuilderMesh mesh, IList<Face> faces, Mesh target)
		{
			target.Clear();
			target.vertices = mesh.positionsInternal;
			target.triangles = faces.SelectMany((Face x) => x.indexes).ToArray<int>();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001398C File Offset: 0x00011B8C
		internal static void CreateEdgeMesh(ProBuilderMesh mesh, Mesh target)
		{
			int num = 0;
			int faceCount = mesh.faceCount;
			for (int i = 0; i < faceCount; i++)
			{
				num += mesh.facesInternal[i].edgesInternal.Length;
			}
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_IndexList.Capacity = num * 2;
			int num2 = 0;
			int num3 = 0;
			while (num3 < faceCount && num2 < num)
			{
				int num4 = 0;
				while (num4 < mesh.facesInternal[num3].edgesInternal.Length && num2 < num)
				{
					Edge edge = mesh.facesInternal[num3].edgesInternal[num4];
					MeshHandles.s_IndexList.Add(edge.a);
					MeshHandles.s_IndexList.Add(edge.b);
					num2++;
					num4++;
				}
				num3++;
			}
			target.Clear();
			target.indexFormat = ((num * 2 > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.name = "ProBuilder::EdgeMesh" + target.GetInstanceID().ToString();
			target.vertices = mesh.positionsInternal;
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Lines, 0, true, 0);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00013AA8 File Offset: 0x00011CA8
		internal static void CreateEdgeMesh(ProBuilderMesh mesh, Mesh target, Edge[] edges)
		{
			int num = edges.Length;
			int num2 = num * 2;
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_IndexList.Capacity = num2;
			for (int i = 0; i < num; i++)
			{
				Edge edge = edges[i];
				MeshHandles.s_IndexList.Add(edge.a);
				MeshHandles.s_IndexList.Add(edge.b);
			}
			target.Clear();
			target.indexFormat = ((num2 > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.name = "ProBuilder::EdgeMesh" + target.GetInstanceID().ToString();
			target.vertices = mesh.positionsInternal;
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Lines, 0, true, 0);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00013B60 File Offset: 0x00011D60
		internal static void CreateVertexMesh(ProBuilderMesh mesh, Mesh target)
		{
			MeshHandles.s_SharedVertexIndexList.Clear();
			int num = mesh.sharedVerticesInternal.Length;
			MeshHandles.s_SharedVertexIndexList.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				MeshHandles.s_SharedVertexIndexList.Add(mesh.sharedVerticesInternal[i][0]);
			}
			MeshHandles.CreateVertexMesh(mesh, target, MeshHandles.s_SharedVertexIndexList);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00013BBB File Offset: 0x00011DBB
		internal static void CreateVertexMesh(ProBuilderMesh mesh, Mesh target, IList<int> indexes)
		{
			if (BuiltinMaterials.geometryShadersSupported)
			{
				MeshHandles.CreatePointMesh(mesh.positionsInternal, indexes, target);
				return;
			}
			MeshHandles.CreatePointBillboardMesh(mesh.positionsInternal, indexes, target);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00013BE0 File Offset: 0x00011DE0
		private static void CreatePointMesh(Vector3[] positions, IList<int> indexes, Mesh target)
		{
			int num = positions.Length;
			target.Clear();
			target.indexFormat = ((num > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.name = "ProBuilder::PointMesh";
			target.vertices = positions;
			target.subMeshCount = 1;
			if (indexes is int[])
			{
				target.SetIndices((int[])indexes, MeshTopology.Points, 0);
				return;
			}
			if (indexes is List<int>)
			{
				target.SetIndices((List<int>)indexes, MeshTopology.Points, 0, true, 0);
				return;
			}
			target.SetIndices(indexes.ToArray<int>(), MeshTopology.Points, 0);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00013C60 File Offset: 0x00011E60
		internal static void CreatePointBillboardMesh(IList<Vector3> positions, Mesh target)
		{
			int count = positions.Count;
			int num = count * 4;
			MeshHandles.s_Vector2List.Clear();
			MeshHandles.s_Vector3List.Clear();
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_Vector2List.Capacity = num;
			MeshHandles.s_Vector3List.Capacity = num;
			MeshHandles.s_IndexList.Capacity = num;
			for (int i = 0; i < count; i++)
			{
				MeshHandles.s_Vector3List.Add(positions[i]);
				MeshHandles.s_Vector3List.Add(positions[i]);
				MeshHandles.s_Vector3List.Add(positions[i]);
				MeshHandles.s_Vector3List.Add(positions[i]);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard0);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard1);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard2);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard3);
				MeshHandles.s_IndexList.Add(i * 4);
				MeshHandles.s_IndexList.Add(i * 4 + 1);
				MeshHandles.s_IndexList.Add(i * 4 + 3);
				MeshHandles.s_IndexList.Add(i * 4 + 2);
			}
			target.Clear();
			target.indexFormat = ((num > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.SetVertices(MeshHandles.s_Vector3List);
			target.SetUVs(0, MeshHandles.s_Vector2List);
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Quads, 0, true, 0);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00013DDC File Offset: 0x00011FDC
		private static void CreatePointBillboardMesh(IList<Vector3> positions, IList<int> indexes, Mesh target)
		{
			int count = indexes.Count;
			int num = count * 4;
			MeshHandles.s_Vector2List.Clear();
			MeshHandles.s_Vector3List.Clear();
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_Vector2List.Capacity = num;
			MeshHandles.s_Vector3List.Capacity = num;
			MeshHandles.s_IndexList.Capacity = num;
			for (int i = 0; i < count; i++)
			{
				int index = indexes[i];
				MeshHandles.s_Vector3List.Add(positions[index]);
				MeshHandles.s_Vector3List.Add(positions[index]);
				MeshHandles.s_Vector3List.Add(positions[index]);
				MeshHandles.s_Vector3List.Add(positions[index]);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard0);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard1);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard2);
				MeshHandles.s_Vector2List.Add(MeshHandles.k_Billboard3);
				MeshHandles.s_IndexList.Add(i * 4);
				MeshHandles.s_IndexList.Add(i * 4 + 1);
				MeshHandles.s_IndexList.Add(i * 4 + 3);
				MeshHandles.s_IndexList.Add(i * 4 + 2);
			}
			target.Clear();
			target.indexFormat = ((num > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.SetVertices(MeshHandles.s_Vector3List);
			target.SetUVs(0, MeshHandles.s_Vector2List);
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Quads, 0, true, 0);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00013F60 File Offset: 0x00012160
		internal static void CreateEdgeBillboardMesh(ProBuilderMesh mesh, Mesh target)
		{
			target.Clear();
			int edgeCount = mesh.edgeCount;
			target.indexFormat = ((edgeCount > 16383) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			Vector3[] positionsInternal = mesh.positionsInternal;
			MeshHandles.s_Vector3List.Clear();
			MeshHandles.s_Vector4List.Clear();
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_Vector3List.Capacity = edgeCount * 4;
			MeshHandles.s_Vector4List.Capacity = edgeCount * 4;
			MeshHandles.s_IndexList.Capacity = edgeCount * 4;
			int num = 0;
			Face[] facesInternal = mesh.facesInternal;
			for (int i = 0; i < facesInternal.Length; i++)
			{
				foreach (Edge edge in facesInternal[i].edgesInternal)
				{
					Vector3 vector = positionsInternal[edge.a];
					Vector3 vector2 = positionsInternal[edge.b];
					Vector3 vector3 = vector2 + (vector2 - vector);
					MeshHandles.s_Vector3List.Add(vector);
					MeshHandles.s_Vector3List.Add(vector);
					MeshHandles.s_Vector3List.Add(vector2);
					MeshHandles.s_Vector3List.Add(vector2);
					MeshHandles.s_Vector4List.Add(new Vector4(vector2.x, vector2.y, vector2.z, 1f));
					MeshHandles.s_Vector4List.Add(new Vector4(vector2.x, vector2.y, vector2.z, -1f));
					MeshHandles.s_Vector4List.Add(new Vector4(vector3.x, vector3.y, vector3.z, 1f));
					MeshHandles.s_Vector4List.Add(new Vector4(vector3.x, vector3.y, vector3.z, -1f));
					MeshHandles.s_IndexList.Add(num);
					MeshHandles.s_IndexList.Add(num + 1);
					MeshHandles.s_IndexList.Add(num + 3);
					MeshHandles.s_IndexList.Add(num + 2);
					num += 4;
				}
			}
			target.SetVertices(MeshHandles.s_Vector3List);
			target.SetTangents(MeshHandles.s_Vector4List);
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Quads, 0, true, 0);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00014190 File Offset: 0x00012390
		internal static void CreateEdgeBillboardMesh(ProBuilderMesh mesh, Mesh target, ICollection<Edge> edges)
		{
			target.Clear();
			int count = edges.Count;
			target.indexFormat = ((count > 16383) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			Vector3[] positionsInternal = mesh.positionsInternal;
			MeshHandles.s_Vector3List.Clear();
			MeshHandles.s_Vector4List.Clear();
			MeshHandles.s_IndexList.Clear();
			MeshHandles.s_Vector3List.Capacity = count * 4;
			MeshHandles.s_Vector4List.Capacity = count * 4;
			MeshHandles.s_IndexList.Capacity = count * 4;
			int num = 0;
			foreach (Edge edge in edges)
			{
				Vector3 vector = positionsInternal[edge.a];
				Vector3 vector2 = positionsInternal[edge.b];
				Vector3 vector3 = vector2 + (vector2 - vector);
				MeshHandles.s_Vector3List.Add(vector);
				MeshHandles.s_Vector3List.Add(vector);
				MeshHandles.s_Vector3List.Add(vector2);
				MeshHandles.s_Vector3List.Add(vector2);
				MeshHandles.s_Vector4List.Add(new Vector4(vector2.x, vector2.y, vector2.z, 1f));
				MeshHandles.s_Vector4List.Add(new Vector4(vector2.x, vector2.y, vector2.z, -1f));
				MeshHandles.s_Vector4List.Add(new Vector4(vector3.x, vector3.y, vector3.z, 1f));
				MeshHandles.s_Vector4List.Add(new Vector4(vector3.x, vector3.y, vector3.z, -1f));
				MeshHandles.s_IndexList.Add(num);
				MeshHandles.s_IndexList.Add(num + 1);
				MeshHandles.s_IndexList.Add(num + 3);
				MeshHandles.s_IndexList.Add(num + 2);
				num += 4;
			}
			target.SetVertices(MeshHandles.s_Vector3List);
			target.SetTangents(MeshHandles.s_Vector4List);
			target.subMeshCount = 1;
			target.SetIndices(MeshHandles.s_IndexList, MeshTopology.Quads, 0, true, 0);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000143B8 File Offset: 0x000125B8
		// Note: this type is marked as 'beforefieldinit'.
		static MeshHandles()
		{
		}

		// Token: 0x04000086 RID: 134
		private static List<Vector3> s_Vector2List = new List<Vector3>();

		// Token: 0x04000087 RID: 135
		private static List<Vector3> s_Vector3List = new List<Vector3>();

		// Token: 0x04000088 RID: 136
		private static List<Vector4> s_Vector4List = new List<Vector4>();

		// Token: 0x04000089 RID: 137
		private static List<int> s_IndexList = new List<int>();

		// Token: 0x0400008A RID: 138
		private static List<int> s_SharedVertexIndexList = new List<int>();

		// Token: 0x0400008B RID: 139
		private static readonly Vector2 k_Billboard0 = new Vector2(-1f, -1f);

		// Token: 0x0400008C RID: 140
		private static readonly Vector2 k_Billboard1 = new Vector2(-1f, 1f);

		// Token: 0x0400008D RID: 141
		private static readonly Vector2 k_Billboard2 = new Vector2(1f, -1f);

		// Token: 0x0400008E RID: 142
		private static readonly Vector2 k_Billboard3 = new Vector2(1f, 1f);

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000537 RID: 1335 RVA: 0x00035B74 File Offset: 0x00033D74
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x00035B80 File Offset: 0x00033D80
			public <>c()
			{
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x00035B88 File Offset: 0x00033D88
			internal IEnumerable<int> <CreateFaceMesh>b__9_0(Face x)
			{
				return x.indexes;
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x00035B90 File Offset: 0x00033D90
			internal IEnumerable<int> <CreateFaceMeshFromFaces>b__10_0(Face x)
			{
				return x.indexes;
			}

			// Token: 0x0400029B RID: 667
			public static readonly MeshHandles.<>c <>9 = new MeshHandles.<>c();

			// Token: 0x0400029C RID: 668
			public static Func<Face, IEnumerable<int>> <>9__9_0;

			// Token: 0x0400029D RID: 669
			public static Func<Face, IEnumerable<int>> <>9__10_0;
		}
	}
}
