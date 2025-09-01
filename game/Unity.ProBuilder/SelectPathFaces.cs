using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200004F RID: 79
	internal static class SelectPathFaces
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0001BF48 File Offset: 0x0001A148
		public static List<int> GetPath(ProBuilderMesh mesh, int start, int end)
		{
			if (mesh == null)
			{
				throw new ArgumentException("Parameter cannot be null", "mesh");
			}
			if (start < 0 || start > mesh.faceCount - 1)
			{
				throw new ArgumentException("Parameter is out of bounds", "start");
			}
			if (end < 0 || end > mesh.faceCount - 1)
			{
				throw new ArgumentException("Parameter is out of bounds", "end");
			}
			if (start == SelectPathFaces.s_cachedStart && mesh == SelectPathFaces.s_cachedMesh && mesh.faceCount == SelectPathFaces.s_cachedFacesCount)
			{
				return SelectPathFaces.GetMinimalPath(SelectPathFaces.s_cachedPredecessors, start, end);
			}
			int[] predecessors = SelectPathFaces.Dijkstra(mesh, start);
			List<int> minimalPath = SelectPathFaces.GetMinimalPath(predecessors, start, end);
			SelectPathFaces.s_cachedPredecessors = predecessors;
			SelectPathFaces.s_cachedStart = start;
			SelectPathFaces.s_cachedMesh = mesh;
			return minimalPath;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001C000 File Offset: 0x0001A200
		private static int[] Dijkstra(ProBuilderMesh mesh, int start)
		{
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			if (SelectPathFaces.s_cachedMesh != mesh || SelectPathFaces.s_cachedFacesCount != mesh.faceCount)
			{
				SelectPathFaces.s_cachedWings = WingedEdge.GetWingedEdges(mesh, true);
				SelectPathFaces.s_cachedFacesIndex.Clear();
				SelectPathFaces.s_cachedFacesCount = mesh.faceCount;
				for (int i = 0; i < mesh.facesInternal.Length; i++)
				{
					SelectPathFaces.s_cachedFacesIndex.Add(mesh.facesInternal[i], i);
				}
			}
			int count = SelectPathFaces.s_cachedWings.Count;
			float[] array = new float[count];
			int[] array2 = new int[count];
			for (int j = 0; j < count; j++)
			{
				array[j] = float.MaxValue;
				array2[j] = -1;
			}
			int num = start;
			array[num] = 0f;
			hashSet.Add(num);
			while (hashSet.Count < count)
			{
				WingedEdge wingedEdge = SelectPathFaces.s_cachedWings[num];
				WingedEdge wingedEdge2 = wingedEdge;
				do
				{
					WingedEdge opposite = wingedEdge2.opposite;
					if (opposite == null)
					{
						wingedEdge2 = wingedEdge2.next;
					}
					else
					{
						int num2 = SelectPathFaces.s_cachedFacesIndex[opposite.face];
						float weight = SelectPathFaces.GetWeight(num, num2, mesh);
						if (array[num] + weight < array[num2])
						{
							array[num2] = array[num] + weight;
							array2[num2] = num;
						}
						if (!hashSet2.Contains(num2) && !hashSet.Contains(num2))
						{
							hashSet2.Add(num2);
						}
						wingedEdge2 = wingedEdge2.next;
					}
				}
				while (wingedEdge2 != wingedEdge);
				if (hashSet2.Count == 0)
				{
					return array2;
				}
				float num3 = float.MaxValue;
				foreach (int num4 in hashSet2)
				{
					if (array[num4] < num3)
					{
						num3 = array[num4];
						num = num4;
					}
				}
				hashSet.Add(num);
				hashSet2.Remove(num);
			}
			return array2;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		private static float GetWeight(int face1, int face2, ProBuilderMesh mesh)
		{
			Vector3 vector = Math.Normal(mesh, mesh.facesInternal[face1]);
			Vector3 vector2 = Math.Normal(mesh, mesh.facesInternal[face2]);
			float num = (1f - Vector3.Dot(vector.normalized, vector2.normalized)) * 2f;
			Vector3 vector3 = Vector3.zero;
			Vector3 a = Vector3.zero;
			foreach (int num2 in mesh.facesInternal[face1].indexesInternal)
			{
				vector3 += mesh.positionsInternal[num2] / (float)mesh.facesInternal[face1].indexesInternal.Length;
			}
			foreach (int num3 in mesh.facesInternal[face2].indexesInternal)
			{
				a += mesh.positionsInternal[num3] / (float)mesh.facesInternal[face2].indexesInternal.Length;
			}
			float num4 = (a - vector3).magnitude * 1f;
			return 10f + num4 + num;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001C310 File Offset: 0x0001A510
		private static List<int> GetMinimalPath(int[] predecessors, int start, int end)
		{
			if (predecessors[end] == -1)
			{
				return null;
			}
			Stack<int> stack = new Stack<int>();
			for (int num = end; num != start; num = predecessors[num])
			{
				stack.Push(num);
			}
			return stack.ToList<int>();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001C344 File Offset: 0x0001A544
		// Note: this type is marked as 'beforefieldinit'.
		static SelectPathFaces()
		{
		}

		// Token: 0x040001CF RID: 463
		private static int[] s_cachedPredecessors;

		// Token: 0x040001D0 RID: 464
		private static int s_cachedStart;

		// Token: 0x040001D1 RID: 465
		private static ProBuilderMesh s_cachedMesh;

		// Token: 0x040001D2 RID: 466
		private static int s_cachedFacesCount;

		// Token: 0x040001D3 RID: 467
		private static List<WingedEdge> s_cachedWings;

		// Token: 0x040001D4 RID: 468
		private static Dictionary<Face, int> s_cachedFacesIndex = new Dictionary<Face, int>();
	}
}
