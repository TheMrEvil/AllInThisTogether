using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200007D RID: 125
	public static class DeleteElements
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0002E644 File Offset: 0x0002C844
		public static void DeleteVertices(this ProBuilderMesh mesh, IEnumerable<int> distinctIndexes)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (distinctIndexes == null || !distinctIndexes.Any<int>())
			{
				return;
			}
			Vertex[] array = mesh.GetVertices(null);
			int num = array.Length;
			int[] offset = new int[num];
			List<int> sorted = new List<int>(distinctIndexes);
			sorted.Sort();
			array = array.SortedRemoveAt(sorted);
			for (int i = 0; i < num; i++)
			{
				offset[i] = ArrayUtility.NearestIndexPriorToValue<int>(sorted, i) + 1;
			}
			foreach (Face face in mesh.facesInternal)
			{
				int[] indexesInternal = face.indexesInternal;
				for (int k = 0; k < indexesInternal.Length; k++)
				{
					indexesInternal[k] -= offset[indexesInternal[k]];
				}
				face.InvalidateCache();
			}
			IEnumerable<KeyValuePair<int, int>> sharedVertices = from x in mesh.sharedVertexLookup
			where sorted.BinarySearch(x.Key) < 0
			select x into y
			select new KeyValuePair<int, int>(y.Key - offset[y.Key], y.Value);
			IEnumerable<KeyValuePair<int, int>> sharedTextures = from x in mesh.sharedTextureLookup
			where sorted.BinarySearch(x.Key) < 0
			select x into y
			select new KeyValuePair<int, int>(y.Key - offset[y.Key], y.Value);
			mesh.SetVertices(array, false);
			mesh.SetSharedVertices(sharedVertices);
			mesh.SetSharedTextures(sharedTextures);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0002E7A3 File Offset: 0x0002C9A3
		public static int[] DeleteFace(this ProBuilderMesh mesh, Face face)
		{
			return mesh.DeleteFaces(new Face[]
			{
				face
			});
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
		public static int[] DeleteFaces(this ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			return mesh.DeleteFaces((from x in faces
			select Array.IndexOf<Face>(mesh.facesInternal, x)).ToList<int>());
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		public static int[] DeleteFaces(this ProBuilderMesh mesh, IList<int> faceIndexes)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faceIndexes == null)
			{
				throw new ArgumentNullException("faceIndexes");
			}
			Face[] array = new Face[faceIndexes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = mesh.facesInternal[faceIndexes[i]];
			}
			List<int> list = array.SelectMany((Face x) => x.distinctIndexesInternal).Distinct<int>().ToList<int>();
			list.Sort();
			int num = mesh.positionsInternal.Length;
			Face[] array2 = mesh.facesInternal.RemoveAt(faceIndexes);
			Vertex[] vertices = mesh.GetVertices(null).SortedRemoveAt(list);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int j = 0; j < num; j++)
			{
				dictionary.Add(j, ArrayUtility.NearestIndexPriorToValue<int>(list, j) + 1);
			}
			for (int k = 0; k < array2.Length; k++)
			{
				int[] indexesInternal = array2[k].indexesInternal;
				for (int l = 0; l < indexesInternal.Length; l++)
				{
					indexesInternal[l] -= dictionary[indexesInternal[l]];
				}
				array2[k].indexesInternal = indexesInternal;
			}
			mesh.SetVertices(vertices, false);
			mesh.sharedVerticesInternal = SharedVertex.SortedRemoveAndShift(mesh.sharedVertexLookup, list);
			mesh.sharedTextures = SharedVertex.SortedRemoveAndShift(mesh.sharedTextureLookup, list);
			mesh.facesInternal = array2;
			return list.ToArray();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0002E968 File Offset: 0x0002CB68
		[Obsolete("Use MeshValidation.RemoveDegenerateTriangles")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int[] RemoveDegenerateTriangles(this ProBuilderMesh mesh)
		{
			List<int> list = new List<int>();
			MeshValidation.RemoveDegenerateTriangles(mesh, list);
			return list.ToArray();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002E98C File Offset: 0x0002CB8C
		[Obsolete("Use MeshValidation.RemoveUnusedVertices")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int[] RemoveUnusedVertices(this ProBuilderMesh mesh)
		{
			List<int> list = new List<int>();
			MeshValidation.RemoveUnusedVertices(mesh, list);
			return list.ToArray();
		}

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06000598 RID: 1432 RVA: 0x0003624F File Offset: 0x0003444F
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00036257 File Offset: 0x00034457
			internal bool <DeleteVertices>b__0(KeyValuePair<int, int> x)
			{
				return this.sorted.BinarySearch(x.Key) < 0;
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0003626E File Offset: 0x0003446E
			internal KeyValuePair<int, int> <DeleteVertices>b__1(KeyValuePair<int, int> y)
			{
				return new KeyValuePair<int, int>(y.Key - this.offset[y.Key], y.Value);
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x00036292 File Offset: 0x00034492
			internal bool <DeleteVertices>b__2(KeyValuePair<int, int> x)
			{
				return this.sorted.BinarySearch(x.Key) < 0;
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x000362A9 File Offset: 0x000344A9
			internal KeyValuePair<int, int> <DeleteVertices>b__3(KeyValuePair<int, int> y)
			{
				return new KeyValuePair<int, int>(y.Key - this.offset[y.Key], y.Value);
			}

			// Token: 0x040002EF RID: 751
			public List<int> sorted;

			// Token: 0x040002F0 RID: 752
			public int[] offset;
		}

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600059D RID: 1437 RVA: 0x000362CD File Offset: 0x000344CD
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x000362D5 File Offset: 0x000344D5
			internal int <DeleteFaces>b__0(Face x)
			{
				return Array.IndexOf<Face>(this.mesh.facesInternal, x);
			}

			// Token: 0x040002F1 RID: 753
			public ProBuilderMesh mesh;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600059F RID: 1439 RVA: 0x000362E8 File Offset: 0x000344E8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x000362F4 File Offset: 0x000344F4
			public <>c()
			{
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x000362FC File Offset: 0x000344FC
			internal IEnumerable<int> <DeleteFaces>b__3_0(Face x)
			{
				return x.distinctIndexesInternal;
			}

			// Token: 0x040002F2 RID: 754
			public static readonly DeleteElements.<>c <>9 = new DeleteElements.<>c();

			// Token: 0x040002F3 RID: 755
			public static Func<Face, IEnumerable<int>> <>9__3_0;
		}
	}
}
