using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000081 RID: 129
	public static class MergeElements
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x00031D0C File Offset: 0x0002FF0C
		public static List<Face> MergePairs(ProBuilderMesh target, IEnumerable<SimpleTuple<Face, Face>> pairs, bool collapseCoincidentVertices = true)
		{
			HashSet<Face> remove = new HashSet<Face>();
			List<Face> list = new List<Face>();
			foreach (SimpleTuple<Face, Face> simpleTuple in pairs)
			{
				Face item = simpleTuple.item1;
				Face item2 = simpleTuple.item2;
				int num = item.indexesInternal.Length;
				int num2 = item2.indexesInternal.Length;
				int[] array = new int[num + num2];
				Array.Copy(item.indexesInternal, 0, array, 0, num);
				Array.Copy(item2.indexesInternal, 0, array, num, num2);
				list.Add(new Face(array, item.submeshIndex, item.uv, item.smoothingGroup, item.textureGroup, item.elementGroup, item.manualUV));
				remove.Add(item);
				remove.Add(item2);
			}
			List<Face> list2 = (from x in target.facesInternal
			where !remove.Contains(x)
			select x).ToList<Face>();
			list2.AddRange(list);
			target.faces = list2;
			if (collapseCoincidentVertices)
			{
				MergeElements.CollapseCoincidentVertices(target, list);
			}
			return list;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00031E50 File Offset: 0x00030050
		public static Face Merge(ProBuilderMesh target, IEnumerable<Face> faces)
		{
			int num = (faces != null) ? faces.Count<Face>() : 0;
			if (num < 1)
			{
				return null;
			}
			Face face = faces.First<Face>();
			Face face2 = new Face(faces.SelectMany((Face x) => x.indexesInternal).ToArray<int>(), face.submeshIndex, face.uv, face.smoothingGroup, face.textureGroup, face.elementGroup, face.manualUV);
			Face[] array = new Face[target.facesInternal.Length - num + 1];
			int num2 = 0;
			HashSet<Face> hashSet = new HashSet<Face>(faces);
			foreach (Face face3 in target.facesInternal)
			{
				if (!hashSet.Contains(face3))
				{
					array[num2++] = face3;
				}
			}
			array[num2] = face2;
			target.faces = array;
			MergeElements.CollapseCoincidentVertices(target, new Face[]
			{
				face2
			});
			return face2;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00031F40 File Offset: 0x00030140
		internal static void CollapseCoincidentVertices(ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (Face face in faces)
			{
				dictionary.Clear();
				for (int i = 0; i < face.indexesInternal.Length; i++)
				{
					int key = sharedVertexLookup[face.indexesInternal[i]];
					if (dictionary.ContainsKey(key))
					{
						face.indexesInternal[i] = dictionary[key];
					}
					else
					{
						dictionary.Add(key, face.indexesInternal[i]);
					}
				}
				face.InvalidateCache();
			}
			MeshValidation.RemoveUnusedVertices(mesh, null);
		}

		// Token: 0x020000BD RID: 189
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x060005C4 RID: 1476 RVA: 0x00036502 File Offset: 0x00034702
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0003650A File Offset: 0x0003470A
			internal bool <MergePairs>b__0(Face x)
			{
				return !this.remove.Contains(x);
			}

			// Token: 0x04000312 RID: 786
			public HashSet<Face> remove;
		}

		// Token: 0x020000BE RID: 190
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C6 RID: 1478 RVA: 0x0003651B File Offset: 0x0003471B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x00036527 File Offset: 0x00034727
			public <>c()
			{
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0003652F File Offset: 0x0003472F
			internal IEnumerable<int> <Merge>b__1_0(Face x)
			{
				return x.indexesInternal;
			}

			// Token: 0x04000313 RID: 787
			public static readonly MergeElements.<>c <>9 = new MergeElements.<>c();

			// Token: 0x04000314 RID: 788
			public static Func<Face, IEnumerable<int>> <>9__1_0;
		}
	}
}
