using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.ProBuilder.KdTree;
using UnityEngine.ProBuilder.KdTree.Math;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200008B RID: 139
	public static class VertexEditing
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0003521C File Offset: 0x0003341C
		public static int MergeVertices(this ProBuilderMesh mesh, int[] indexes, bool collapseToFirst = false)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			Vertex[] vertices = mesh.GetVertices(null);
			Vertex vertex = collapseToFirst ? vertices[indexes[0]] : Vertex.Average(vertices, indexes);
			mesh.SetVerticesCoincident(indexes);
			mesh.SplitUVs(indexes);
			int sharedVertexHandle = mesh.GetSharedVertexHandle(indexes[0]);
			mesh.SetSharedVertexValues(sharedVertexHandle, vertex);
			SharedVertex sharedVertex = mesh.sharedVerticesInternal[sharedVertexHandle];
			List<int> list = new List<int>();
			MeshValidation.RemoveDegenerateTriangles(mesh, list);
			int num = -1;
			for (int i = 0; i < sharedVertex.Count; i++)
			{
				if (!list.Contains(sharedVertex[i]))
				{
					num = sharedVertex[i];
				}
			}
			int num2 = num;
			for (int j = 0; j < list.Count; j++)
			{
				if (num > list[j])
				{
					num2--;
				}
			}
			return num2;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00035300 File Offset: 0x00033500
		public static void SplitVertices(this ProBuilderMesh mesh, Edge edge)
		{
			mesh.SplitVertices(new int[]
			{
				edge.a,
				edge.b
			});
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00035320 File Offset: 0x00033520
		public static void SplitVertices(this ProBuilderMesh mesh, IEnumerable<int> vertices)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			int num = sharedVertexLookup.Count;
			foreach (int key in vertices)
			{
				num = (sharedVertexLookup[key] = num + 1);
			}
			mesh.SetSharedVertices(sharedVertexLookup);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000353A4 File Offset: 0x000335A4
		public static int[] WeldVertices(this ProBuilderMesh mesh, IEnumerable<int> indexes, float neighborRadius)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			Vertex[] vertices = mesh.GetVertices(null);
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			HashSet<int> sharedVertexHandles = mesh.GetSharedVertexHandles(indexes);
			int count = sharedVertexHandles.Count;
			int num = Math.Min(32, sharedVertexHandles.Count);
			KdTree<float, int> kdTree = new KdTree<float, int>(3, new FloatMath(), AddDuplicateBehavior.Collect);
			foreach (int num2 in sharedVertexHandles)
			{
				Vector3 position = vertices[sharedVerticesInternal[num2][0]].position;
				kdTree.Add(new float[]
				{
					position.x,
					position.y,
					position.z
				}, num2);
			}
			float[] array = new float[3];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, Vector3> dictionary2 = new Dictionary<int, Vector3>();
			int num3 = sharedVerticesInternal.Length;
			foreach (int num4 in sharedVertexHandles)
			{
				if (!dictionary.ContainsKey(num4))
				{
					Vector3 position2 = vertices[sharedVerticesInternal[num4][0]].position;
					array[0] = position2.x;
					array[1] = position2.y;
					array[2] = position2.z;
					KdTreeNode<float, int>[] array2 = kdTree.RadialSearch(array, neighborRadius, num);
					if (num < count && array2.Length >= num)
					{
						array2 = kdTree.RadialSearch(array, neighborRadius, count);
						num = Math.Min(count, array2.Length + array2.Length / 2);
					}
					Vector3 zero = Vector3.zero;
					float num5 = 0f;
					for (int i = 0; i < array2.Length; i++)
					{
						int value = array2[i].Value;
						if (!dictionary.ContainsKey(value))
						{
							zero.x += array2[i].Point[0];
							zero.y += array2[i].Point[1];
							zero.z += array2[i].Point[2];
							dictionary.Add(value, num3);
							num5 += 1f;
							if (array2[i].Duplicates != null)
							{
								for (int j = 0; j < array2[i].Duplicates.Count; j++)
								{
									dictionary.Add(array2[i].Duplicates[j], num3);
								}
							}
						}
					}
					zero.x /= num5;
					zero.y /= num5;
					zero.z /= num5;
					dictionary2.Add(num3, zero);
					num3++;
				}
			}
			int[] array3 = new int[dictionary.Count];
			int num6 = 0;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				SharedVertex sharedVertex = sharedVerticesInternal[keyValuePair.Key];
				array3[num6++] = sharedVertex[0];
				for (int k = 0; k < sharedVertex.Count; k++)
				{
					sharedVertexLookup[sharedVertex[k]] = keyValuePair.Value;
					vertices[sharedVertex[k]].position = dictionary2[keyValuePair.Value];
				}
			}
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetVertices(vertices, false);
			return array3;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00035768 File Offset: 0x00033968
		internal static FaceRebuildData ExplodeVertex(IList<Vertex> vertices, IList<SimpleTuple<WingedEdge, int>> edgeAndCommonIndex, float distance, out Dictionary<int, List<int>> appendedVertices)
		{
			Face face = edgeAndCommonIndex.FirstOrDefault<SimpleTuple<WingedEdge, int>>().item1.face;
			List<Edge> list = WingedEdge.SortEdgesByAdjacency(face);
			appendedVertices = new Dictionary<int, List<int>>();
			Vector3 lhs = Math.Normal(vertices, face.indexesInternal);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (SimpleTuple<WingedEdge, int> simpleTuple in edgeAndCommonIndex)
			{
				if (simpleTuple.item2 == simpleTuple.item1.edge.common.a)
				{
					dictionary.Add(simpleTuple.item1.edge.local.a, simpleTuple.item2);
				}
				else
				{
					dictionary.Add(simpleTuple.item1.edge.local.b, simpleTuple.item2);
				}
			}
			int count = list.Count;
			List<Vertex> list2 = new List<Vertex>();
			for (int i = 0; i < count; i++)
			{
				int b = list[i].b;
				if (dictionary.ContainsKey(b))
				{
					Vertex a = vertices[list[i].a];
					Vertex b2 = vertices[list[i].b];
					Vertex a2 = vertices[list[(i + 1) % count].b];
					Vertex vertex = a - b2;
					Vertex vertex2 = a2 - b2;
					vertex.Normalize();
					vertex2.Normalize();
					Vertex item = vertices[b] + vertex * distance;
					Vertex item2 = vertices[b] + vertex2 * distance;
					appendedVertices.AddOrAppend(dictionary[b], list2.Count);
					list2.Add(item);
					appendedVertices.AddOrAppend(dictionary[b], list2.Count);
					list2.Add(item2);
				}
				else
				{
					list2.Add(vertices[b]);
				}
			}
			List<int> list3;
			if (Triangulation.TriangulateVertices(list2, out list3, false, false))
			{
				FaceRebuildData faceRebuildData = new FaceRebuildData();
				faceRebuildData.vertices = list2;
				faceRebuildData.face = new Face(face);
				Vector3 rhs = Math.Normal(list2, list3);
				if (Vector3.Dot(lhs, rhs) < 0f)
				{
					list3.Reverse();
				}
				faceRebuildData.face.indexesInternal = list3.ToArray();
				return faceRebuildData;
			}
			return null;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000359DC File Offset: 0x00033BDC
		private static Edge AlignEdgeWithDirection(EdgeLookup edge, int commonIndex)
		{
			if (edge.common.a == commonIndex)
			{
				return new Edge(edge.local.a, edge.local.b);
			}
			return new Edge(edge.local.b, edge.local.a);
		}
	}
}
