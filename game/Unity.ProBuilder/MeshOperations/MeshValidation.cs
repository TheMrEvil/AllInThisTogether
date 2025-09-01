using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000085 RID: 133
	public static class MeshValidation
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0003267F File Offset: 0x0003087F
		public static bool ContainsDegenerateTriangles(this ProBuilderMesh mesh)
		{
			return mesh.ContainsDegenerateTriangles(mesh.facesInternal);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00032690 File Offset: 0x00030890
		public static bool ContainsDegenerateTriangles(this ProBuilderMesh mesh, IList<Face> faces)
		{
			Vector3[] positionsInternal = mesh.positionsInternal;
			foreach (Face face in faces)
			{
				int[] indexesInternal = face.indexesInternal;
				for (int i = 0; i < indexesInternal.Length; i += 3)
				{
					if (Math.TriangleArea(positionsInternal[indexesInternal[i]], positionsInternal[indexesInternal[i + 1]], positionsInternal[indexesInternal[i + 2]]) <= Mathf.Epsilon)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00032720 File Offset: 0x00030920
		public static bool ContainsDegenerateTriangles(this ProBuilderMesh mesh, Face face)
		{
			Vector3[] positionsInternal = mesh.positionsInternal;
			int[] indexesInternal = face.indexesInternal;
			for (int i = 0; i < indexesInternal.Length; i += 3)
			{
				if (Math.TriangleArea(positionsInternal[indexesInternal[i]], positionsInternal[indexesInternal[i + 1]], positionsInternal[indexesInternal[i + 2]]) <= Mathf.Epsilon)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00032778 File Offset: 0x00030978
		public static bool ContainsNonContiguousTriangles(this ProBuilderMesh mesh, Face face)
		{
			Edge edge = face.edgesInternal[0];
			Edge b = edge;
			int a = edge.a;
			int num = 1;
			while (face.TryGetNextEdge(edge, edge.b, ref edge, ref a) && edge != b && num < face.edgesInternal.Length)
			{
				num++;
			}
			return num != face.edgesInternal.Length;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000327D8 File Offset: 0x000309D8
		public static List<Face> EnsureFacesAreComposedOfContiguousTriangles(this ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			List<Face> list = new List<Face>();
			foreach (Face face in faces)
			{
				if (mesh.ContainsNonContiguousTriangles(face))
				{
					List<List<Triangle>> list2 = mesh.CollectFaceGroups(face);
					if (list2.Count >= 2)
					{
						face.SetIndexes(list2[0].SelectMany((Triangle x) => x.indices));
						for (int i = 1; i < list2.Count; i++)
						{
							Face face2 = new Face(face);
							face2.SetIndexes(list2[i].SelectMany((Triangle x) => x.indices));
							list.Add(face2);
						}
					}
				}
			}
			List<Face> list3 = new List<Face>(mesh.facesInternal);
			list3.AddRange(list);
			mesh.faces = list3;
			return list;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000328F0 File Offset: 0x00030AF0
		internal static List<List<Triangle>> CollectFaceGroups(this ProBuilderMesh mesh, Face face)
		{
			List<List<Triangle>> list = new List<List<Triangle>>();
			int[] indexesInternal = face.indexesInternal;
			for (int i = 0; i < indexesInternal.Length; i += 3)
			{
				Triangle triangle = new Triangle(indexesInternal[i], indexesInternal[i + 1], indexesInternal[i + 2]);
				bool flag = false;
				Func<Triangle, bool> <>9__0;
				for (int j = 0; j < list.Count; j++)
				{
					IEnumerable<Triangle> source = list[j];
					Func<Triangle, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((Triangle x) => x.IsAdjacent(triangle)));
					}
					if (source.Any(predicate))
					{
						list[j].Add(triangle);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(new List<Triangle>
					{
						triangle
					});
				}
			}
			return list;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000329B8 File Offset: 0x00030BB8
		public static bool RemoveDegenerateTriangles(ProBuilderMesh mesh, List<int> removed = null)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = mesh.sharedTextureLookup;
			Vector3[] positionsInternal = mesh.positionsInternal;
			Dictionary<int, int> dictionary = new Dictionary<int, int>(sharedVertexLookup.Count);
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>(sharedTextureLookup.Count);
			List<Face> list = new List<Face>(mesh.faceCount);
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>(8);
			foreach (Face face in mesh.facesInternal)
			{
				dictionary3.Clear();
				List<int> list2 = new List<int>();
				int[] indexesInternal = face.indexesInternal;
				for (int j = 0; j < indexesInternal.Length; j += 3)
				{
					if (Math.TriangleArea(positionsInternal[indexesInternal[j]], positionsInternal[indexesInternal[j + 1]], positionsInternal[indexesInternal[j + 2]]) > Mathf.Epsilon)
					{
						int num = indexesInternal[j];
						int num2 = indexesInternal[j + 1];
						int num3 = indexesInternal[j + 2];
						int num4 = sharedVertexLookup[num];
						int num5 = sharedVertexLookup[num2];
						int num6 = sharedVertexLookup[num3];
						if (num4 != num5 && num4 != num6 && num5 != num6)
						{
							int num7;
							if (!dictionary3.TryGetValue(num4, out num7))
							{
								dictionary3.Add(num4, num);
							}
							else
							{
								num = num7;
							}
							if (!dictionary3.TryGetValue(num5, out num7))
							{
								dictionary3.Add(num5, num2);
							}
							else
							{
								num2 = num7;
							}
							if (!dictionary3.TryGetValue(num6, out num7))
							{
								dictionary3.Add(num6, num3);
							}
							else
							{
								num3 = num7;
							}
							list2.Add(num);
							list2.Add(num2);
							list2.Add(num3);
							if (!dictionary.ContainsKey(num))
							{
								dictionary.Add(num, num4);
							}
							if (!dictionary.ContainsKey(num2))
							{
								dictionary.Add(num2, num5);
							}
							if (!dictionary.ContainsKey(num3))
							{
								dictionary.Add(num3, num6);
							}
							if (sharedTextureLookup.ContainsKey(num) && !dictionary2.ContainsKey(num))
							{
								dictionary2.Add(num, sharedTextureLookup[num]);
							}
							if (sharedTextureLookup.ContainsKey(num2) && !dictionary2.ContainsKey(num2))
							{
								dictionary2.Add(num2, sharedTextureLookup[num2]);
							}
							if (sharedTextureLookup.ContainsKey(num3) && !dictionary2.ContainsKey(num3))
							{
								dictionary2.Add(num3, sharedTextureLookup[num3]);
							}
						}
					}
				}
				if (list2.Count > 0)
				{
					face.indexesInternal = list2.ToArray();
					list.Add(face);
				}
			}
			mesh.faces = list;
			mesh.SetSharedVertices(dictionary);
			mesh.SetSharedTextures(dictionary2);
			return MeshValidation.RemoveUnusedVertices(mesh, removed);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00032C5C File Offset: 0x00030E5C
		public static bool RemoveUnusedVertices(ProBuilderMesh mesh, List<int> removed = null)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag = removed != null;
			if (flag)
			{
				removed.Clear();
			}
			List<int> list = flag ? removed : new List<int>();
			HashSet<int> hashSet = new HashSet<int>(mesh.facesInternal.SelectMany((Face x) => x.indexes));
			for (int i = 0; i < mesh.positionsInternal.Length; i++)
			{
				if (!hashSet.Contains(i))
				{
					list.Add(i);
				}
			}
			mesh.DeleteVertices(list);
			return list.Count > 0;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00032CF8 File Offset: 0x00030EF8
		internal static List<int> RebuildIndexes(IEnumerable<int> indices, List<int> removed)
		{
			List<int> list = new List<int>();
			int count = removed.Count;
			foreach (int num in indices)
			{
				int num2 = ArrayUtility.NearestIndexPriorToValue<int>(removed, num) + 1;
				if (num2 <= -1 || num2 >= count || removed[num2] != num)
				{
					list.Add(num - num2);
				}
			}
			return list;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00032D74 File Offset: 0x00030F74
		internal static List<Edge> RebuildEdges(IEnumerable<Edge> edges, List<int> removed)
		{
			List<Edge> list = new List<Edge>();
			int count = removed.Count;
			foreach (Edge edge in edges)
			{
				int num = ArrayUtility.NearestIndexPriorToValue<int>(removed, edge.a) + 1;
				int num2 = ArrayUtility.NearestIndexPriorToValue<int>(removed, edge.b) + 1;
				if ((num <= -1 || num >= count || removed[num] != edge.a) && (num2 <= -1 || num2 >= count || removed[num2] != edge.b))
				{
					list.Add(new Edge(edge.a - num, edge.b - num2));
				}
			}
			return list;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00032E34 File Offset: 0x00031034
		internal static void RebuildSelectionIndexes(ProBuilderMesh mesh, ref Face[] faces, ref Edge[] edges, ref int[] indices, IEnumerable<int> removed)
		{
			List<int> list = removed.ToList<int>();
			list.Sort();
			if (faces != null && faces.Length != 0)
			{
				faces = (from x in faces
				where mesh.facesInternal.Contains(x)
				select x).ToArray<Face>();
			}
			if (edges != null && edges.Length != 0)
			{
				edges = MeshValidation.RebuildEdges(edges, list).ToArray();
			}
			if (indices != null && indices.Length != 0)
			{
				indices = MeshValidation.RebuildIndexes(indices, list).ToArray();
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00032EB0 File Offset: 0x000310B0
		internal static bool EnsureMeshIsValid(ProBuilderMesh mesh, out int removedVertices)
		{
			removedVertices = 0;
			if (mesh.ContainsDegenerateTriangles())
			{
				Face[] selectedFacesInternal = mesh.selectedFacesInternal;
				Edge[] selectedEdgesInternal = mesh.selectedEdgesInternal;
				int[] selectedIndexesInternal = mesh.selectedIndexesInternal;
				List<int> list = new List<int>();
				if (MeshValidation.RemoveDegenerateTriangles(mesh, list))
				{
					mesh.sharedVertices = SharedVertex.GetSharedVerticesWithPositions(mesh.positionsInternal);
					MeshValidation.RebuildSelectionIndexes(mesh, ref selectedFacesInternal, ref selectedEdgesInternal, ref selectedIndexesInternal, list);
					mesh.selectedFacesInternal = selectedFacesInternal;
					mesh.selectedEdgesInternal = selectedEdgesInternal;
					mesh.selectedIndexesInternal = selectedIndexesInternal;
					removedVertices = list.Count;
					return false;
				}
			}
			MeshValidation.EnsureValidAttributes(mesh);
			return true;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00032F30 File Offset: 0x00031130
		private static void EnsureRealNumbers(IList<Vector2> attribute)
		{
			int i = 0;
			int num = (attribute != null) ? attribute.Count : 0;
			while (i < num)
			{
				attribute[i] = Math.FixNaN(attribute[i]);
				i++;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00032F74 File Offset: 0x00031174
		private static void EnsureRealNumbers(IList<Vector3> attribute)
		{
			int i = 0;
			int num = (attribute != null) ? attribute.Count : 0;
			while (i < num)
			{
				attribute[i] = Math.FixNaN(attribute[i]);
				i++;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00032FB8 File Offset: 0x000311B8
		private static void EnsureRealNumbers(IList<Vector4> attribute)
		{
			int i = 0;
			int num = (attribute != null) ? attribute.Count : 0;
			while (i < num)
			{
				attribute[i] = Math.FixNaN(attribute[i]);
				i++;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00032FF4 File Offset: 0x000311F4
		private static void EnsureArraySize<T>(ref T[] attribute, int expectedVertexCount, MeshValidation.AttributeValidationStrategy strategy = MeshValidation.AttributeValidationStrategy.Nullify, T fill = default(T))
		{
			if (attribute == null || attribute.Length == expectedVertexCount)
			{
				return;
			}
			if (strategy == MeshValidation.AttributeValidationStrategy.Nullify)
			{
				attribute = null;
				return;
			}
			int num = attribute.Length;
			Array.Resize<T>(ref attribute, expectedVertexCount);
			for (int i = num - 1; i < expectedVertexCount; i++)
			{
				attribute[i] = fill;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00033038 File Offset: 0x00031238
		private static void EnsureListSize<T>(ref List<T> attribute, int expectedVertexCount, MeshValidation.AttributeValidationStrategy strategy = MeshValidation.AttributeValidationStrategy.Nullify, T fill = default(T))
		{
			if (attribute == null || attribute.Count == expectedVertexCount)
			{
				return;
			}
			if (strategy == MeshValidation.AttributeValidationStrategy.Nullify)
			{
				attribute = null;
				return;
			}
			int count = attribute.Count;
			List<T> list = new List<T>(expectedVertexCount);
			int i = 0;
			int num = Mathf.Min(count, expectedVertexCount);
			while (i < num)
			{
				list.Add(attribute[i]);
				i++;
			}
			for (int j = list.Count - 1; j < expectedVertexCount; j++)
			{
				list.Add(fill);
			}
			attribute = list;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000330A8 File Offset: 0x000312A8
		private static void EnsureValidAttributes(ProBuilderMesh mesh)
		{
			int vertexCount = mesh.vertexCount;
			Vector3[] normalsInternal = mesh.normalsInternal;
			Color[] colorsInternal = mesh.colorsInternal;
			Vector4[] tangentsInternal = mesh.tangentsInternal;
			Vector2[] texturesInternal = mesh.texturesInternal;
			List<Vector4> textures2Internal = mesh.textures2Internal;
			List<Vector4> textures3Internal = mesh.textures3Internal;
			MeshValidation.EnsureArraySize<Vector3>(ref normalsInternal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector3));
			MeshValidation.EnsureArraySize<Color>(ref colorsInternal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Color));
			MeshValidation.EnsureArraySize<Vector4>(ref tangentsInternal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector4));
			MeshValidation.EnsureArraySize<Vector3>(ref normalsInternal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector3));
			MeshValidation.EnsureArraySize<Vector2>(ref texturesInternal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector2));
			MeshValidation.EnsureListSize<Vector4>(ref textures2Internal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector4));
			MeshValidation.EnsureListSize<Vector4>(ref textures3Internal, vertexCount, MeshValidation.AttributeValidationStrategy.Nullify, default(Vector4));
			MeshValidation.EnsureRealNumbers(normalsInternal);
			MeshValidation.EnsureRealNumbers(tangentsInternal);
			MeshValidation.EnsureRealNumbers(normalsInternal);
			MeshValidation.EnsureRealNumbers(texturesInternal);
			MeshValidation.EnsureRealNumbers(textures2Internal);
			MeshValidation.EnsureRealNumbers(textures3Internal);
		}

		// Token: 0x020000C0 RID: 192
		private enum AttributeValidationStrategy
		{
			// Token: 0x04000318 RID: 792
			Resize,
			// Token: 0x04000319 RID: 793
			Nullify
		}

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005CC RID: 1484 RVA: 0x00036553 File Offset: 0x00034753
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0003655F File Offset: 0x0003475F
			public <>c()
			{
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x00036567 File Offset: 0x00034767
			internal IEnumerable<int> <EnsureFacesAreComposedOfContiguousTriangles>b__4_0(Triangle x)
			{
				return x.indices;
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x00036570 File Offset: 0x00034770
			internal IEnumerable<int> <EnsureFacesAreComposedOfContiguousTriangles>b__4_1(Triangle x)
			{
				return x.indices;
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00036579 File Offset: 0x00034779
			internal IEnumerable<int> <RemoveUnusedVertices>b__7_0(Face x)
			{
				return x.indexes;
			}

			// Token: 0x0400031A RID: 794
			public static readonly MeshValidation.<>c <>9 = new MeshValidation.<>c();

			// Token: 0x0400031B RID: 795
			public static Func<Triangle, IEnumerable<int>> <>9__4_0;

			// Token: 0x0400031C RID: 796
			public static Func<Triangle, IEnumerable<int>> <>9__4_1;

			// Token: 0x0400031D RID: 797
			public static Func<Face, IEnumerable<int>> <>9__7_0;
		}

		// Token: 0x020000C2 RID: 194
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060005D1 RID: 1489 RVA: 0x00036581 File Offset: 0x00034781
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x00036589 File Offset: 0x00034789
			internal bool <CollectFaceGroups>b__0(Triangle x)
			{
				return x.IsAdjacent(this.triangle);
			}

			// Token: 0x0400031E RID: 798
			public Triangle triangle;

			// Token: 0x0400031F RID: 799
			public Func<Triangle, bool> <>9__0;
		}

		// Token: 0x020000C3 RID: 195
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x00036598 File Offset: 0x00034798
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x000365A0 File Offset: 0x000347A0
			internal bool <RebuildSelectionIndexes>b__0(Face x)
			{
				return this.mesh.facesInternal.Contains(x);
			}

			// Token: 0x04000320 RID: 800
			public ProBuilderMesh mesh;
		}
	}
}
