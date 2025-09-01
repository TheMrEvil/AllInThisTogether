using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000086 RID: 134
	public static class QuadUtility
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x00033198 File Offset: 0x00031398
		public static List<Face> ToQuads(this ProBuilderMesh mesh, IList<Face> faces, bool smoothing = true)
		{
			HashSet<Face> hashSet = new HashSet<Face>();
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, faces, true);
			Dictionary<EdgeLookup, float> dictionary = new Dictionary<EdgeLookup, float>();
			for (int i = 0; i < wingedEdges.Count; i++)
			{
				using (WingedEdgeEnumerator wingedEdgeEnumerator = new WingedEdgeEnumerator(wingedEdges[i]))
				{
					while (wingedEdgeEnumerator.MoveNext())
					{
						WingedEdge wingedEdge = wingedEdgeEnumerator.Current;
						if (wingedEdge.opposite != null && !dictionary.ContainsKey(wingedEdge.edge))
						{
							float quadScore = mesh.GetQuadScore(wingedEdge, wingedEdge.opposite, 0.9f);
							dictionary.Add(wingedEdge.edge, quadScore);
						}
					}
				}
			}
			List<SimpleTuple<Face, Face>> list = new List<SimpleTuple<Face, Face>>();
			foreach (WingedEdge wingedEdge2 in wingedEdges)
			{
				if (hashSet.Add(wingedEdge2.face))
				{
					float num = 0f;
					Face face = null;
					using (WingedEdgeEnumerator wingedEdgeEnumerator2 = new WingedEdgeEnumerator(wingedEdge2))
					{
						while (wingedEdgeEnumerator2.MoveNext())
						{
							WingedEdge wingedEdge3 = wingedEdgeEnumerator2.Current;
							float num2;
							if ((wingedEdge3.opposite == null || !hashSet.Contains(wingedEdge3.opposite.face)) && dictionary.TryGetValue(wingedEdge3.edge, out num2) && num2 > num && wingedEdge2.face == QuadUtility.GetBestQuadConnection(wingedEdge3.opposite, dictionary))
							{
								num = num2;
								face = wingedEdge3.opposite.face;
							}
						}
					}
					if (face != null)
					{
						hashSet.Add(face);
						list.Add(new SimpleTuple<Face, Face>(wingedEdge2.face, face));
					}
				}
			}
			return MergeElements.MergePairs(mesh, list, smoothing);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0003336C File Offset: 0x0003156C
		private static Face GetBestQuadConnection(WingedEdge wing, Dictionary<EdgeLookup, float> connections)
		{
			float num = 0f;
			Face result = null;
			using (WingedEdgeEnumerator wingedEdgeEnumerator = new WingedEdgeEnumerator(wing))
			{
				while (wingedEdgeEnumerator.MoveNext())
				{
					WingedEdge wingedEdge = wingedEdgeEnumerator.Current;
					float num2 = 0f;
					if (connections.TryGetValue(wingedEdge.edge, out num2) && num2 > num)
					{
						num = connections[wingedEdge.edge];
						result = wingedEdge.opposite.face;
					}
				}
			}
			return result;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000333EC File Offset: 0x000315EC
		private static float GetQuadScore(this ProBuilderMesh mesh, WingedEdge left, WingedEdge right, float normalThreshold = 0.9f)
		{
			Vertex[] vertices = mesh.GetVertices(null);
			int[] array = WingedEdge.MakeQuad(left, right);
			if (array == null)
			{
				return 0f;
			}
			Vector3 lhs = Math.Normal(vertices[array[0]].position, vertices[array[1]].position, vertices[array[2]].position);
			Vector3 rhs = Math.Normal(vertices[array[2]].position, vertices[array[3]].position, vertices[array[0]].position);
			float num = Vector3.Dot(lhs, rhs);
			if (num < normalThreshold)
			{
				return 0f;
			}
			Vector3 vector = vertices[array[1]].position - vertices[array[0]].position;
			Vector3 vector2 = vertices[array[2]].position - vertices[array[1]].position;
			Vector3 vector3 = vertices[array[3]].position - vertices[array[2]].position;
			Vector3 vector4 = vertices[array[0]].position - vertices[array[3]].position;
			vector.Normalize();
			vector2.Normalize();
			vector3.Normalize();
			vector4.Normalize();
			float num2 = Mathf.Abs(Vector3.Dot(vector, vector2));
			float num3 = Mathf.Abs(Vector3.Dot(vector2, vector3));
			float num4 = Mathf.Abs(Vector3.Dot(vector3, vector4));
			float num5 = Mathf.Abs(Vector3.Dot(vector4, vector));
			num += 1f - (num2 + num3 + num4 + num5) * 0.25f;
			num += Mathf.Abs(Vector3.Dot(vector, vector3)) * 0.5f;
			num += Mathf.Abs(Vector3.Dot(vector2, vector4)) * 0.5f;
			return num * 0.33f;
		}
	}
}
