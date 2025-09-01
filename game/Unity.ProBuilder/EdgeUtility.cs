using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000016 RID: 22
	internal static class EdgeUtility
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		public static IEnumerable<Edge> GetSharedVertexHandleEdges(this ProBuilderMesh mesh, IEnumerable<Edge> edges)
		{
			return from x in edges
			select mesh.GetSharedVertexHandleEdge(x);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public static Edge GetSharedVertexHandleEdge(this ProBuilderMesh mesh, Edge edge)
		{
			return new Edge(mesh.sharedVertexLookup[edge.a], mesh.sharedVertexLookup[edge.b]);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000F519 File Offset: 0x0000D719
		internal static Edge GetEdgeWithSharedVertexHandles(this ProBuilderMesh mesh, Edge edge)
		{
			return new Edge(mesh.sharedVerticesInternal[edge.a][0], mesh.sharedVerticesInternal[edge.b][0]);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000F548 File Offset: 0x0000D748
		public static bool ValidateEdge(ProBuilderMesh mesh, Edge edge, out SimpleTuple<Face, Edge> validEdge)
		{
			Face[] facesInternal = mesh.facesInternal;
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			Edge sharedVertexHandleEdge = mesh.GetSharedVertexHandleEdge(edge);
			for (int i = 0; i < facesInternal.Length; i++)
			{
				int num = -1;
				int num2 = -1;
				int num3 = -1;
				int num4 = -1;
				if (facesInternal[i].distinctIndexesInternal.ContainsMatch(sharedVerticesInternal[sharedVertexHandleEdge.a].arrayInternal, out num, out num3) && facesInternal[i].distinctIndexesInternal.ContainsMatch(sharedVerticesInternal[sharedVertexHandleEdge.b].arrayInternal, out num2, out num4))
				{
					int a = facesInternal[i].distinctIndexesInternal[num];
					int b = facesInternal[i].distinctIndexesInternal[num2];
					validEdge = new SimpleTuple<Face, Edge>(facesInternal[i], new Edge(a, b));
					return true;
				}
			}
			validEdge = default(SimpleTuple<Face, Edge>);
			return false;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000F608 File Offset: 0x0000D808
		internal static bool Contains(this Edge[] edges, Edge edge)
		{
			for (int i = 0; i < edges.Length; i++)
			{
				if (edges[i].Equals(edge))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000F638 File Offset: 0x0000D838
		internal static bool Contains(this Edge[] edges, int x, int y)
		{
			for (int i = 0; i < edges.Length; i++)
			{
				if ((x == edges[i].a && y == edges[i].b) || (x == edges[i].b && y == edges[i].a))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000F694 File Offset: 0x0000D894
		internal static int IndexOf(this ProBuilderMesh mesh, IList<Edge> edges, Edge edge)
		{
			for (int i = 0; i < edges.Count; i++)
			{
				if (edges[i].Equals(edge, mesh.sharedVertexLookup))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		internal static int[] AllTriangles(this Edge[] edges)
		{
			int[] array = new int[edges.Length * 2];
			int num = 0;
			for (int i = 0; i < edges.Length; i++)
			{
				array[num++] = edges[i].a;
				array[num++] = edges[i].b;
			}
			return array;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000F720 File Offset: 0x0000D920
		internal static Face GetFace(this ProBuilderMesh mesh, Edge edge)
		{
			Face result = null;
			foreach (Face face in mesh.facesInternal)
			{
				Edge[] edgesInternal = face.edgesInternal;
				int j = 0;
				int num = edgesInternal.Length;
				while (j < num)
				{
					if (edge.Equals(edgesInternal[j]))
					{
						return face;
					}
					if (edgesInternal.Contains(edgesInternal[j]))
					{
						result = face;
					}
					j++;
				}
			}
			return result;
		}

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06000533 RID: 1331 RVA: 0x00035B48 File Offset: 0x00033D48
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x00035B50 File Offset: 0x00033D50
			internal Edge <GetSharedVertexHandleEdges>b__0(Edge x)
			{
				return this.mesh.GetSharedVertexHandleEdge(x);
			}

			// Token: 0x04000299 RID: 665
			public ProBuilderMesh mesh;
		}
	}
}
