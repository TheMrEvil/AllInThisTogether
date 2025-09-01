using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200007E RID: 126
	public static class ElementSelection
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		public static void GetNeighborFaces(ProBuilderMesh mesh, Edge edge, List<Face> neighborFaces)
		{
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Edge edge2 = new Edge(sharedVertexLookup[edge.a], sharedVertexLookup[edge.b]);
			Edge edge3 = new Edge(0, 0);
			for (int i = 0; i < mesh.facesInternal.Length; i++)
			{
				Edge[] edgesInternal = mesh.facesInternal[i].edgesInternal;
				for (int j = 0; j < edgesInternal.Length; j++)
				{
					edge3.a = edgesInternal[j].a;
					edge3.b = edgesInternal[j].b;
					if ((edge2.a == sharedVertexLookup[edge3.a] && edge2.b == sharedVertexLookup[edge3.b]) || (edge2.a == sharedVertexLookup[edge3.b] && edge2.b == sharedVertexLookup[edge3.a]))
					{
						neighborFaces.Add(mesh.facesInternal[i]);
						break;
					}
				}
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
		internal static List<SimpleTuple<Face, Edge>> GetNeighborFaces(ProBuilderMesh mesh, Edge edge)
		{
			List<SimpleTuple<Face, Edge>> list = new List<SimpleTuple<Face, Edge>>();
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Edge edge2 = new Edge(sharedVertexLookup[edge.a], sharedVertexLookup[edge.b]);
			Edge edge3 = new Edge(0, 0);
			for (int i = 0; i < mesh.facesInternal.Length; i++)
			{
				Edge[] edgesInternal = mesh.facesInternal[i].edgesInternal;
				for (int j = 0; j < edgesInternal.Length; j++)
				{
					edge3.a = edgesInternal[j].a;
					edge3.b = edgesInternal[j].b;
					if ((edge2.a == sharedVertexLookup[edge3.a] && edge2.b == sharedVertexLookup[edge3.b]) || (edge2.a == sharedVertexLookup[edge3.b] && edge2.b == sharedVertexLookup[edge3.a]))
					{
						list.Add(new SimpleTuple<Face, Edge>(mesh.facesInternal[i], edgesInternal[j]));
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		internal static List<Face> GetNeighborFaces(ProBuilderMesh mesh, int[] indexes)
		{
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			List<Face> list = new List<Face>();
			HashSet<int> hashSet = new HashSet<int>();
			foreach (int key in indexes)
			{
				hashSet.Add(sharedVertexLookup[key]);
			}
			for (int j = 0; j < mesh.facesInternal.Length; j++)
			{
				int[] distinctIndexesInternal = mesh.facesInternal[j].distinctIndexesInternal;
				for (int k = 0; k < distinctIndexesInternal.Length; k++)
				{
					if (hashSet.Contains(sharedVertexLookup[distinctIndexesInternal[k]]))
					{
						list.Add(mesh.facesInternal[j]);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0002EC84 File Offset: 0x0002CE84
		internal static Edge[] GetConnectedEdges(ProBuilderMesh mesh, int[] indexes)
		{
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			List<Edge> list = new List<Edge>();
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < indexes.Length; i++)
			{
				hashSet.Add(sharedVertexLookup[indexes[i]]);
			}
			HashSet<Edge> hashSet2 = new HashSet<Edge>();
			Edge item = new Edge(0, 0);
			Face[] facesInternal = mesh.facesInternal;
			for (int j = 0; j < facesInternal.Length; j++)
			{
				foreach (Edge edge in facesInternal[j].edges)
				{
					Edge edge2 = new Edge(sharedVertexLookup[edge.a], sharedVertexLookup[edge.b]);
					if (hashSet.Contains(edge2.a) || (hashSet.Contains(edge2.b) && !hashSet2.Contains(item)))
					{
						list.Add(edge);
						hashSet2.Add(edge2);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		public static IEnumerable<Edge> GetPerimeterEdges(this ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			List<Edge> list = faces.SelectMany((Face x) => x.edgesInternal).ToList<Edge>();
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			int count = list.Count;
			Dictionary<Edge, List<Edge>> dictionary = new Dictionary<Edge, List<Edge>>();
			for (int i = 0; i < count; i++)
			{
				Edge key = new Edge(sharedVertexLookup[list[i].a], sharedVertexLookup[list[i].b]);
				List<Edge> list2;
				if (dictionary.TryGetValue(key, out list2))
				{
					list2.Add(list[i]);
				}
				else
				{
					dictionary.Add(key, new List<Edge>
					{
						list[i]
					});
				}
			}
			return from x in dictionary
			where x.Value.Count < 2
			select x.Value[0];
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		internal static int[] GetPerimeterEdges(ProBuilderMesh mesh, IList<Edge> edges)
		{
			int num = (edges != null) ? edges.Count : 0;
			Edge[] array = mesh.GetSharedVertexHandleEdges(edges).ToArray<Edge>();
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length - 1; i++)
			{
				for (int j = i + 1; j < array.Length; j++)
				{
					if (array[i].a == array[j].a || array[i].a == array[j].b || array[i].b == array[j].a || array[i].b == array[j].b)
					{
						array2[i]++;
						array2[j]++;
					}
				}
			}
			int num2 = Math.Min<int>(array2);
			List<int> list = new List<int>();
			for (int k = 0; k < array2.Length; k++)
			{
				if (array2[k] <= num2)
				{
					list.Add(k);
				}
			}
			if (list.Count == num)
			{
				return new int[0];
			}
			return list.ToArray();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002F000 File Offset: 0x0002D200
		internal static IEnumerable<Face> GetPerimeterFaces(ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<Edge, List<Face>> dictionary = new Dictionary<Edge, List<Face>>();
			foreach (Face face in faces)
			{
				foreach (Edge edge in face.edgesInternal)
				{
					Edge key = new Edge(sharedVertexLookup[edge.a], sharedVertexLookup[edge.b]);
					if (dictionary.ContainsKey(key))
					{
						dictionary[key].Add(face);
					}
					else
					{
						dictionary.Add(key, new List<Face>
						{
							face
						});
					}
				}
			}
			return (from x in dictionary
			where x.Value.Count < 2
			select x.Value[0]).Distinct<Face>();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0002F114 File Offset: 0x0002D314
		internal static int[] GetPerimeterVertices(ProBuilderMesh mesh, int[] indexes, Edge[] universal_edges_all)
		{
			int num = indexes.Length;
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = mesh.GetSharedVertexHandle(indexes[i]);
			}
			int[] array2 = new int[indexes.Length];
			for (int j = 0; j < indexes.Length - 1; j++)
			{
				for (int k = j + 1; k < indexes.Length; k++)
				{
					if (universal_edges_all.Contains(array[j], array[k]))
					{
						array2[j]++;
						array2[k]++;
					}
				}
			}
			int num2 = Math.Min<int>(array2);
			List<int> list = new List<int>();
			for (int l = 0; l < num; l++)
			{
				if (array2[l] <= num2)
				{
					list.Add(l);
				}
			}
			if (list.Count >= num)
			{
				return new int[0];
			}
			return list.ToArray();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0002F1F4 File Offset: 0x0002D3F4
		private static WingedEdge EdgeRingNext(WingedEdge edge)
		{
			if (edge == null)
			{
				return null;
			}
			WingedEdge wingedEdge = edge.next;
			WingedEdge previous = edge.previous;
			int num = 0;
			while (wingedEdge != previous && wingedEdge != edge)
			{
				wingedEdge = wingedEdge.next;
				if (wingedEdge == previous)
				{
					return null;
				}
				previous = previous.previous;
				num++;
			}
			if (num % 2 == 0 || wingedEdge == edge)
			{
				wingedEdge = null;
			}
			return wingedEdge;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0002F244 File Offset: 0x0002D444
		internal static IEnumerable<Edge> GetEdgeRing(ProBuilderMesh pb, IEnumerable<Edge> edges)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(pb, false);
			List<EdgeLookup> list = EdgeLookup.GetEdgeLookup(edges, pb.sharedVertexLookup).ToList<EdgeLookup>();
			list = list.Distinct<EdgeLookup>().ToList<EdgeLookup>();
			Dictionary<Edge, WingedEdge> dictionary = new Dictionary<Edge, WingedEdge>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (!dictionary.ContainsKey(wingedEdges[i].edge.common))
				{
					dictionary.Add(wingedEdges[i].edge.common, wingedEdges[i]);
				}
			}
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>();
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				WingedEdge wingedEdge;
				if (dictionary.TryGetValue(list[j].common, out wingedEdge) && !hashSet.Contains(wingedEdge.edge))
				{
					WingedEdge wingedEdge2 = wingedEdge;
					while (wingedEdge2 != null && hashSet.Add(wingedEdge2.edge))
					{
						wingedEdge2 = ElementSelection.EdgeRingNext(wingedEdge2);
						if (wingedEdge2 != null && wingedEdge2.opposite != null)
						{
							wingedEdge2 = wingedEdge2.opposite;
						}
					}
					wingedEdge2 = ElementSelection.EdgeRingNext(wingedEdge.opposite);
					if (wingedEdge2 != null && wingedEdge2.opposite != null)
					{
						wingedEdge2 = wingedEdge2.opposite;
					}
					while (wingedEdge2 != null && hashSet.Add(wingedEdge2.edge))
					{
						wingedEdge2 = ElementSelection.EdgeRingNext(wingedEdge2);
						if (wingedEdge2 != null && wingedEdge2.opposite != null)
						{
							wingedEdge2 = wingedEdge2.opposite;
						}
					}
				}
				j++;
			}
			return from x in hashSet
			select x.local;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
		internal static IEnumerable<Edge> GetEdgeRingIterative(ProBuilderMesh pb, IEnumerable<Edge> edges)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(pb, false);
			List<EdgeLookup> list = EdgeLookup.GetEdgeLookup(edges, pb.sharedVertexLookup).ToList<EdgeLookup>();
			list = list.Distinct<EdgeLookup>().ToList<EdgeLookup>();
			Dictionary<Edge, WingedEdge> dictionary = new Dictionary<Edge, WingedEdge>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (!dictionary.ContainsKey(wingedEdges[i].edge.common))
				{
					dictionary.Add(wingedEdges[i].edge.common, wingedEdges[i]);
				}
			}
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>();
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				WingedEdge wingedEdge;
				if (dictionary.TryGetValue(list[j].common, out wingedEdge))
				{
					WingedEdge wingedEdge2 = wingedEdge;
					if (!hashSet.Contains(wingedEdge2.edge))
					{
						hashSet.Add(wingedEdge2.edge);
					}
					WingedEdge wingedEdge3 = ElementSelection.EdgeRingNext(wingedEdge2);
					if (wingedEdge3 != null && wingedEdge3.opposite != null && !hashSet.Contains(wingedEdge3.edge))
					{
						hashSet.Add(wingedEdge3.edge);
					}
					WingedEdge wingedEdge4 = ElementSelection.EdgeRingNext(wingedEdge2.opposite);
					if (wingedEdge4 != null && wingedEdge4.opposite != null && !hashSet.Contains(wingedEdge4.edge))
					{
						hashSet.Add(wingedEdge4.edge);
					}
				}
				j++;
			}
			return from x in hashSet
			select x.local;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002F564 File Offset: 0x0002D764
		internal static bool GetEdgeLoop(ProBuilderMesh mesh, IEnumerable<Edge> edges, out Edge[] loop)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, false);
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>(EdgeLookup.GetEdgeLookup(edges, mesh.sharedVertexLookup));
			HashSet<EdgeLookup> hashSet2 = new HashSet<EdgeLookup>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (!hashSet2.Contains(wingedEdges[i].edge) && hashSet.Contains(wingedEdges[i].edge) && !ElementSelection.GetEdgeLoopInternal(wingedEdges[i], wingedEdges[i].edge.common.b, hashSet2))
				{
					ElementSelection.GetEdgeLoopInternal(wingedEdges[i], wingedEdges[i].edge.common.a, hashSet2);
				}
			}
			loop = (from x in hashSet2
			select x.local).ToArray<Edge>();
			return true;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002F64C File Offset: 0x0002D84C
		internal static bool GetEdgeLoopIterative(ProBuilderMesh mesh, IEnumerable<Edge> edges, out Edge[] loop)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, false);
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>(EdgeLookup.GetEdgeLookup(edges, mesh.sharedVertexLookup));
			HashSet<EdgeLookup> hashSet2 = new HashSet<EdgeLookup>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (hashSet.Contains(wingedEdges[i].edge))
				{
					ElementSelection.GetEdgeLoopInternalIterative(wingedEdges[i], wingedEdges[i].edge.common, hashSet2);
				}
			}
			loop = (from x in hashSet2
			select x.local).ToArray<Edge>();
			return true;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		private static bool GetEdgeLoopInternal(WingedEdge start, int startIndex, HashSet<EdgeLookup> used)
		{
			int num = startIndex;
			WingedEdge wingedEdge = start;
			do
			{
				used.Add(wingedEdge.edge);
				List<WingedEdge> list = ElementSelection.GetSpokes(wingedEdge, num, true).DistinctBy((WingedEdge x) => x.edge.common).ToList<WingedEdge>();
				wingedEdge = null;
				if (list.Count == 4)
				{
					wingedEdge = list[2];
					num = ((wingedEdge.edge.common.a == num) ? wingedEdge.edge.common.b : wingedEdge.edge.common.a);
				}
			}
			while (wingedEdge != null && !used.Contains(wingedEdge.edge));
			return wingedEdge != null;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		private static void GetEdgeLoopInternalIterative(WingedEdge start, Edge edge, HashSet<EdgeLookup> used)
		{
			int a = edge.a;
			int b = edge.b;
			if (!used.Contains(start.edge))
			{
				used.Add(start.edge);
			}
			List<WingedEdge> list = ElementSelection.GetSpokes(start, a, true).DistinctBy((WingedEdge x) => x.edge.common).ToList<WingedEdge>();
			List<WingedEdge> list2 = ElementSelection.GetSpokes(start, b, true).DistinctBy((WingedEdge x) => x.edge.common).ToList<WingedEdge>();
			if (list.Count == 4)
			{
				WingedEdge wingedEdge = list[2];
				if (!used.Contains(wingedEdge.edge))
				{
					used.Add(wingedEdge.edge);
				}
			}
			if (list2.Count == 4)
			{
				WingedEdge wingedEdge = list2[2];
				if (!used.Contains(wingedEdge.edge))
				{
					used.Add(wingedEdge.edge);
				}
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		private static WingedEdge NextSpoke(WingedEdge wing, int pivot, bool opp)
		{
			if (opp)
			{
				return wing.opposite;
			}
			if (wing.next.edge.common.Contains(pivot))
			{
				return wing.next;
			}
			if (wing.previous.edge.common.Contains(pivot))
			{
				return wing.previous;
			}
			return null;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002F908 File Offset: 0x0002DB08
		internal static List<WingedEdge> GetSpokes(WingedEdge wing, int sharedIndex, bool allowHoles = false)
		{
			List<WingedEdge> list = new List<WingedEdge>();
			WingedEdge wingedEdge = wing;
			bool flag = false;
			while (!list.Contains(wingedEdge))
			{
				list.Add(wingedEdge);
				wingedEdge = ElementSelection.NextSpoke(wingedEdge, sharedIndex, flag);
				flag = !flag;
				if (wingedEdge != null && wingedEdge.edge.common.Equals(wing.edge.common))
				{
					return list;
				}
				if (wingedEdge == null)
				{
					if (!allowHoles)
					{
						return null;
					}
					wingedEdge = wing.opposite;
					flag = false;
					List<WingedEdge> list2 = new List<WingedEdge>();
					while (wingedEdge != null && !wingedEdge.edge.common.Equals(wing.edge.common))
					{
						list2.Add(wingedEdge);
						wingedEdge = ElementSelection.NextSpoke(wingedEdge, sharedIndex, flag);
						flag = !flag;
					}
					list2.Reverse();
					list.AddRange(list2);
					return list;
				}
			}
			return list;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		public static HashSet<Face> GrowSelection(ProBuilderMesh mesh, IEnumerable<Face> faces, float maxAngleDiff = -1f)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, true);
			HashSet<Face> hashSet = new HashSet<Face>(faces);
			HashSet<Face> hashSet2 = new HashSet<Face>();
			Vector3 from = Vector3.zero;
			bool flag = maxAngleDiff > 0f;
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (hashSet.Contains(wingedEdges[i].face))
				{
					if (flag)
					{
						from = Math.Normal(mesh, wingedEdges[i].face);
					}
					using (WingedEdgeEnumerator wingedEdgeEnumerator = new WingedEdgeEnumerator(wingedEdges[i]))
					{
						while (wingedEdgeEnumerator.MoveNext())
						{
							WingedEdge wingedEdge = wingedEdgeEnumerator.Current;
							if (wingedEdge.opposite != null && !hashSet.Contains(wingedEdge.opposite.face))
							{
								if (flag)
								{
									Vector3 to = Math.Normal(mesh, wingedEdge.opposite.face);
									if (Vector3.Angle(from, to) < maxAngleDiff)
									{
										hashSet2.Add(wingedEdge.opposite.face);
									}
								}
								else
								{
									hashSet2.Add(wingedEdge.opposite.face);
								}
							}
						}
					}
				}
			}
			return hashSet2;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0002FB00 File Offset: 0x0002DD00
		internal static void Flood(WingedEdge wing, HashSet<Face> selection)
		{
			ElementSelection.Flood(null, wing, ElementSelection.Vector3_Zero, -1f, selection);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0002FB14 File Offset: 0x0002DD14
		internal static void Flood(ProBuilderMesh pb, WingedEdge wing, Vector3 wingNrm, float maxAngle, HashSet<Face> selection)
		{
			WingedEdge wingedEdge = wing;
			do
			{
				WingedEdge opposite = wingedEdge.opposite;
				if (opposite != null && !selection.Contains(opposite.face))
				{
					if (maxAngle > 0f)
					{
						Vector3 vector = Math.Normal(pb, opposite.face);
						if (Vector3.Angle(wingNrm, vector) < maxAngle && selection.Add(opposite.face))
						{
							ElementSelection.Flood(pb, opposite, vector, maxAngle, selection);
						}
					}
					else if (selection.Add(opposite.face))
					{
						ElementSelection.Flood(pb, opposite, wingNrm, maxAngle, selection);
					}
				}
				wingedEdge = wingedEdge.next;
			}
			while (wingedEdge != wing);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002FB9C File Offset: 0x0002DD9C
		public static HashSet<Face> FloodSelection(ProBuilderMesh mesh, IList<Face> faces, float maxAngleDiff)
		{
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, true);
			HashSet<Face> hashSet = new HashSet<Face>(faces);
			HashSet<Face> hashSet2 = new HashSet<Face>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				if (!hashSet2.Contains(wingedEdges[i].face) && hashSet.Contains(wingedEdges[i].face))
				{
					hashSet2.Add(wingedEdges[i].face);
					ElementSelection.Flood(mesh, wingedEdges[i], (maxAngleDiff > 0f) ? Math.Normal(mesh, wingedEdges[i].face) : ElementSelection.Vector3_Zero, maxAngleDiff, hashSet2);
				}
			}
			return hashSet2;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0002FC3C File Offset: 0x0002DE3C
		public static HashSet<Face> GetFaceLoop(ProBuilderMesh mesh, Face[] faces, bool ring = false)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			HashSet<Face> hashSet = new HashSet<Face>();
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, false);
			foreach (Face face in faces)
			{
				hashSet.UnionWith(ElementSelection.GetFaceLoop(wingedEdges, face, ring));
			}
			return hashSet;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
		public static HashSet<Face> GetFaceRingAndLoop(ProBuilderMesh mesh, Face[] faces)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			HashSet<Face> hashSet = new HashSet<Face>();
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, false);
			foreach (Face face in faces)
			{
				hashSet.UnionWith(ElementSelection.GetFaceLoop(wingedEdges, face, true));
				hashSet.UnionWith(ElementSelection.GetFaceLoop(wingedEdges, face, false));
			}
			return hashSet;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0002FD14 File Offset: 0x0002DF14
		private static HashSet<Face> GetFaceLoop(List<WingedEdge> wings, Face face, bool ring)
		{
			HashSet<Face> hashSet = new HashSet<Face>();
			if (face == null)
			{
				return hashSet;
			}
			WingedEdge wingedEdge = wings.FirstOrDefault((WingedEdge x) => x.face == face);
			if (wingedEdge == null)
			{
				return hashSet;
			}
			if (ring)
			{
				wingedEdge = (wingedEdge.next ?? wingedEdge.previous);
			}
			for (int i = 0; i < 2; i++)
			{
				WingedEdge wingedEdge2 = wingedEdge;
				if (i == 1)
				{
					if (wingedEdge.opposite == null || wingedEdge.opposite.face == null)
					{
						break;
					}
					wingedEdge2 = wingedEdge.opposite;
				}
				while (hashSet.Add(wingedEdge2.face) && wingedEdge2.Count() == 4)
				{
					wingedEdge2 = wingedEdge2.next.next.opposite;
					if (wingedEdge2 == null || wingedEdge2.face == null)
					{
						break;
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0002FDD4 File Offset: 0x0002DFD4
		internal static List<List<Edge>> FindHoles(ProBuilderMesh mesh, IEnumerable<int> indexes)
		{
			HashSet<int> sharedVertexHandles = mesh.GetSharedVertexHandles(indexes);
			List<List<Edge>> list = new List<List<Edge>>();
			foreach (List<WingedEdge> source in ElementSelection.FindHoles(WingedEdge.GetWingedEdges(mesh, false), sharedVertexHandles))
			{
				list.Add((from x in source
				select x.edge.local).ToList<Edge>());
			}
			return list;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0002FE68 File Offset: 0x0002E068
		internal static List<List<WingedEdge>> FindHoles(List<WingedEdge> wings, HashSet<int> common)
		{
			HashSet<WingedEdge> hashSet = new HashSet<WingedEdge>();
			List<List<WingedEdge>> list = new List<List<WingedEdge>>();
			Func<WingedEdge, bool> <>9__1;
			Func<WingedEdge, bool> <>9__2;
			for (int i = 0; i < wings.Count; i++)
			{
				WingedEdge wingedEdge = wings[i];
				if (wingedEdge.opposite == null && !hashSet.Contains(wingedEdge) && (common.Contains(wingedEdge.edge.common.a) || common.Contains(wingedEdge.edge.common.b)))
				{
					List<WingedEdge> list2 = new List<WingedEdge>();
					WingedEdge wingedEdge2 = wingedEdge;
					int num = wingedEdge2.edge.common.a;
					int num2 = 0;
					while (wingedEdge2 != null && num2++ < 2048)
					{
						hashSet.Add(wingedEdge2);
						list2.Add(wingedEdge2);
						num = ((wingedEdge2.edge.common.a == num) ? wingedEdge2.edge.common.b : wingedEdge2.edge.common.a);
						wingedEdge2 = ElementSelection.FindNextEdgeInHole(wingedEdge2, num);
						if (wingedEdge2 == wingedEdge)
						{
							break;
						}
					}
					List<SimpleTuple<int, int>> list3 = new List<SimpleTuple<int, int>>();
					for (int j = 0; j < list2.Count; j++)
					{
						WingedEdge wingedEdge3 = list2[j];
						for (int k = j - 1; k > -1; k--)
						{
							if (wingedEdge3.edge.common.b == list2[k].edge.common.a)
							{
								list3.Add(new SimpleTuple<int, int>(k, j));
								break;
							}
						}
					}
					int count = list3.Count;
					list3.Sort((SimpleTuple<int, int> x, SimpleTuple<int, int> y) => x.item1.CompareTo(y.item1));
					int[] array = new int[count];
					int l = count - 1;
					while (l > -1)
					{
						int item = list3[l].item1;
						int num3 = list3[l].item2 - array[l] - item + 1;
						List<WingedEdge> range = list2.GetRange(item, num3);
						list2.RemoveRange(item, num3);
						for (int m = l - 1; m > -1; m--)
						{
							if (list3[m].item2 > list3[l].item2)
							{
								array[m] += num3;
							}
						}
						if (count < 2)
						{
							goto IL_2DB;
						}
						IEnumerable<WingedEdge> source = range;
						Func<WingedEdge, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((WingedEdge w) => common.Contains(w.edge.common.a)));
						}
						if (source.Any(predicate))
						{
							goto IL_2DB;
						}
						IEnumerable<WingedEdge> source2 = range;
						Func<WingedEdge, bool> predicate2;
						if ((predicate2 = <>9__2) == null)
						{
							predicate2 = (<>9__2 = ((WingedEdge w) => common.Contains(w.edge.common.b)));
						}
						if (source2.Any(predicate2))
						{
							goto IL_2DB;
						}
						IL_2E3:
						l--;
						continue;
						IL_2DB:
						list.Add(range);
						goto IL_2E3;
					}
				}
			}
			return list;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00030178 File Offset: 0x0002E378
		private static WingedEdge FindNextEdgeInHole(WingedEdge wing, int common)
		{
			WingedEdge adjacentEdgeWithCommonIndex = wing.GetAdjacentEdgeWithCommonIndex(common);
			int num = 0;
			while (adjacentEdgeWithCommonIndex != null && adjacentEdgeWithCommonIndex != wing && num++ < 2048)
			{
				if (adjacentEdgeWithCommonIndex.opposite == null)
				{
					return adjacentEdgeWithCommonIndex;
				}
				adjacentEdgeWithCommonIndex = adjacentEdgeWithCommonIndex.opposite.GetAdjacentEdgeWithCommonIndex(common);
			}
			return null;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000301BC File Offset: 0x0002E3BC
		// Note: this type is marked as 'beforefieldinit'.
		static ElementSelection()
		{
		}

		// Token: 0x04000265 RID: 613
		private const int k_MaxHoleIterations = 2048;

		// Token: 0x04000266 RID: 614
		private static readonly Vector3 Vector3_Zero = new Vector3(0f, 0f, 0f);

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005A2 RID: 1442 RVA: 0x00036304 File Offset: 0x00034504
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00036310 File Offset: 0x00034510
			public <>c()
			{
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00036318 File Offset: 0x00034518
			internal IEnumerable<Edge> <GetPerimeterEdges>b__5_0(Face x)
			{
				return x.edgesInternal;
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00036320 File Offset: 0x00034520
			internal bool <GetPerimeterEdges>b__5_1(KeyValuePair<Edge, List<Edge>> x)
			{
				return x.Value.Count < 2;
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00036331 File Offset: 0x00034531
			internal Edge <GetPerimeterEdges>b__5_2(KeyValuePair<Edge, List<Edge>> x)
			{
				return x.Value[0];
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00036340 File Offset: 0x00034540
			internal bool <GetPerimeterFaces>b__7_0(KeyValuePair<Edge, List<Face>> x)
			{
				return x.Value.Count < 2;
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00036351 File Offset: 0x00034551
			internal Face <GetPerimeterFaces>b__7_1(KeyValuePair<Edge, List<Face>> x)
			{
				return x.Value[0];
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00036360 File Offset: 0x00034560
			internal Edge <GetEdgeRing>b__10_0(EdgeLookup x)
			{
				return x.local;
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00036369 File Offset: 0x00034569
			internal Edge <GetEdgeRingIterative>b__11_0(EdgeLookup x)
			{
				return x.local;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00036372 File Offset: 0x00034572
			internal Edge <GetEdgeLoop>b__12_0(EdgeLookup x)
			{
				return x.local;
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x0003637B File Offset: 0x0003457B
			internal Edge <GetEdgeLoopIterative>b__13_0(EdgeLookup x)
			{
				return x.local;
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00036384 File Offset: 0x00034584
			internal Edge <GetEdgeLoopInternal>b__14_0(WingedEdge x)
			{
				return x.edge.common;
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x000363A0 File Offset: 0x000345A0
			internal Edge <GetEdgeLoopInternalIterative>b__15_0(WingedEdge x)
			{
				return x.edge.common;
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x000363BC File Offset: 0x000345BC
			internal Edge <GetEdgeLoopInternalIterative>b__15_1(WingedEdge x)
			{
				return x.edge.common;
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x000363D8 File Offset: 0x000345D8
			internal Edge <FindHoles>b__26_0(WingedEdge x)
			{
				return x.edge.local;
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x000363F4 File Offset: 0x000345F4
			internal int <FindHoles>b__27_0(SimpleTuple<int, int> x, SimpleTuple<int, int> y)
			{
				return x.item1.CompareTo(y.item1);
			}

			// Token: 0x040002F4 RID: 756
			public static readonly ElementSelection.<>c <>9 = new ElementSelection.<>c();

			// Token: 0x040002F5 RID: 757
			public static Func<Face, IEnumerable<Edge>> <>9__5_0;

			// Token: 0x040002F6 RID: 758
			public static Func<KeyValuePair<Edge, List<Edge>>, bool> <>9__5_1;

			// Token: 0x040002F7 RID: 759
			public static Func<KeyValuePair<Edge, List<Edge>>, Edge> <>9__5_2;

			// Token: 0x040002F8 RID: 760
			public static Func<KeyValuePair<Edge, List<Face>>, bool> <>9__7_0;

			// Token: 0x040002F9 RID: 761
			public static Func<KeyValuePair<Edge, List<Face>>, Face> <>9__7_1;

			// Token: 0x040002FA RID: 762
			public static Func<EdgeLookup, Edge> <>9__10_0;

			// Token: 0x040002FB RID: 763
			public static Func<EdgeLookup, Edge> <>9__11_0;

			// Token: 0x040002FC RID: 764
			public static Func<EdgeLookup, Edge> <>9__12_0;

			// Token: 0x040002FD RID: 765
			public static Func<EdgeLookup, Edge> <>9__13_0;

			// Token: 0x040002FE RID: 766
			public static Func<WingedEdge, Edge> <>9__14_0;

			// Token: 0x040002FF RID: 767
			public static Func<WingedEdge, Edge> <>9__15_0;

			// Token: 0x04000300 RID: 768
			public static Func<WingedEdge, Edge> <>9__15_1;

			// Token: 0x04000301 RID: 769
			public static Func<WingedEdge, Edge> <>9__26_0;

			// Token: 0x04000302 RID: 770
			public static Comparison<SimpleTuple<int, int>> <>9__27_0;
		}

		// Token: 0x020000B9 RID: 185
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x060005B2 RID: 1458 RVA: 0x00036417 File Offset: 0x00034617
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0003641F File Offset: 0x0003461F
			internal bool <GetFaceLoop>b__0(WingedEdge x)
			{
				return x.face == this.face;
			}

			// Token: 0x04000303 RID: 771
			public Face face;
		}

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x060005B4 RID: 1460 RVA: 0x0003642F File Offset: 0x0003462F
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x00036438 File Offset: 0x00034638
			internal bool <FindHoles>b__1(WingedEdge w)
			{
				return this.common.Contains(w.edge.common.a);
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x00036464 File Offset: 0x00034664
			internal bool <FindHoles>b__2(WingedEdge w)
			{
				return this.common.Contains(w.edge.common.b);
			}

			// Token: 0x04000304 RID: 772
			public HashSet<int> common;

			// Token: 0x04000305 RID: 773
			public Func<WingedEdge, bool> <>9__1;

			// Token: 0x04000306 RID: 774
			public Func<WingedEdge, bool> <>9__2;
		}
	}
}
