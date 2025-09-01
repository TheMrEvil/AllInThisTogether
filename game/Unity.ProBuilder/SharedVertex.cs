using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public sealed class SharedVertex : ICollection<int>, IEnumerable<int>, IEnumerable
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0001FF59 File Offset: 0x0001E159
		internal int[] arrayInternal
		{
			get
			{
				return this.m_Vertices;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001FF61 File Offset: 0x0001E161
		public SharedVertex(IEnumerable<int> indexes)
		{
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			this.m_Vertices = indexes.ToArray<int>();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001FF83 File Offset: 0x0001E183
		public SharedVertex(SharedVertex sharedVertex)
		{
			if (sharedVertex == null)
			{
				throw new ArgumentNullException("sharedVertex");
			}
			this.m_Vertices = new int[sharedVertex.Count];
			Array.Copy(sharedVertex.m_Vertices, this.m_Vertices, this.m_Vertices.Length);
		}

		// Token: 0x17000090 RID: 144
		public int this[int i]
		{
			get
			{
				return this.m_Vertices[i];
			}
			set
			{
				this.m_Vertices[i] = value;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
		public IEnumerator<int> GetEnumerator()
		{
			return ((IEnumerable<int>)this.m_Vertices).GetEnumerator();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001FFE5 File Offset: 0x0001E1E5
		public override string ToString()
		{
			return this.m_Vertices.ToString(",");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001FFF7 File Offset: 0x0001E1F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001FFFF File Offset: 0x0001E1FF
		public void Add(int item)
		{
			this.m_Vertices = this.m_Vertices.Add(item);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00020013 File Offset: 0x0001E213
		public void Clear()
		{
			this.m_Vertices = new int[0];
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00020021 File Offset: 0x0001E221
		public bool Contains(int item)
		{
			return Array.IndexOf<int>(this.m_Vertices, item) > -1;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00020032 File Offset: 0x0001E232
		public void CopyTo(int[] array, int arrayIndex)
		{
			this.m_Vertices.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00020041 File Offset: 0x0001E241
		public bool Remove(int item)
		{
			if (Array.IndexOf<int>(this.m_Vertices, item) < 0)
			{
				return false;
			}
			this.m_Vertices = this.m_Vertices.RemoveAt(item);
			return true;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00020067 File Offset: 0x0001E267
		public int Count
		{
			get
			{
				return this.m_Vertices.Length;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00020071 File Offset: 0x0001E271
		public bool IsReadOnly
		{
			get
			{
				return this.m_Vertices.IsReadOnly;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00020080 File Offset: 0x0001E280
		public static void GetSharedVertexLookup(IList<SharedVertex> sharedVertices, Dictionary<int, int> lookup)
		{
			lookup.Clear();
			int i = 0;
			int count = sharedVertices.Count;
			while (i < count)
			{
				foreach (int key in sharedVertices[i])
				{
					if (!lookup.ContainsKey(key))
					{
						lookup.Add(key, i);
					}
				}
				i++;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000200F4 File Offset: 0x0001E2F4
		internal void ShiftIndexes(int offset)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				this.m_Vertices[i] += offset;
				i++;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00020128 File Offset: 0x0001E328
		internal static SharedVertex[] ToSharedVertices(IEnumerable<KeyValuePair<int, int>> lookup)
		{
			if (lookup == null)
			{
				return new SharedVertex[0];
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<List<int>> list = new List<List<int>>();
			foreach (KeyValuePair<int, int> keyValuePair in lookup)
			{
				if (keyValuePair.Value < 0)
				{
					list.Add(new List<int>
					{
						keyValuePair.Key
					});
				}
				else
				{
					int index = -1;
					if (dictionary.TryGetValue(keyValuePair.Value, out index))
					{
						list[index].Add(keyValuePair.Key);
					}
					else
					{
						dictionary.Add(keyValuePair.Value, list.Count);
						list.Add(new List<int>
						{
							keyValuePair.Key
						});
					}
				}
			}
			return SharedVertex.ToSharedVertices(list);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00020204 File Offset: 0x0001E404
		private static SharedVertex[] ToSharedVertices(List<List<int>> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			SharedVertex[] array = new SharedVertex[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SharedVertex(list[i]);
			}
			return array;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0002024C File Offset: 0x0001E44C
		public static SharedVertex[] GetSharedVerticesWithPositions(IList<Vector3> positions)
		{
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			Dictionary<IntVec3, List<int>> dictionary = new Dictionary<IntVec3, List<int>>();
			for (int i = 0; i < positions.Count; i++)
			{
				List<int> list;
				if (dictionary.TryGetValue(positions[i], out list))
				{
					list.Add(i);
				}
				else
				{
					dictionary.Add(new IntVec3(positions[i]), new List<int>
					{
						i
					});
				}
			}
			SharedVertex[] array = new SharedVertex[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<IntVec3, List<int>> keyValuePair in dictionary)
			{
				array[num++] = new SharedVertex(keyValuePair.Value.ToArray());
			}
			return array;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00020320 File Offset: 0x0001E520
		internal static SharedVertex[] RemoveAndShift(Dictionary<int, int> lookup, IEnumerable<int> remove)
		{
			List<int> list = new List<int>(remove);
			list.Sort();
			return SharedVertex.SortedRemoveAndShift(lookup, list);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00020344 File Offset: 0x0001E544
		internal static SharedVertex[] SortedRemoveAndShift(Dictionary<int, int> lookup, List<int> remove)
		{
			foreach (int key in remove)
			{
				lookup[key] = -1;
			}
			SharedVertex[] array = SharedVertex.ToSharedVertices(from x in lookup
			where x.Value > -1
			select x);
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				int j = 0;
				int count = array[i].Count;
				while (j < count)
				{
					int num2 = ArrayUtility.NearestIndexPriorToValue<int>(remove, array[i][j]);
					SharedVertex sharedVertex = array[i];
					int i2 = j;
					sharedVertex[i2] -= num2 + 1;
					j++;
				}
				i++;
			}
			return array;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00020420 File Offset: 0x0001E620
		internal static void SetCoincident(ref Dictionary<int, int> lookup, IEnumerable<int> vertices)
		{
			int count = lookup.Count;
			foreach (int key in vertices)
			{
				lookup[key] = count;
			}
		}

		// Token: 0x040001F2 RID: 498
		[SerializeField]
		[FormerlySerializedAs("array")]
		[FormerlySerializedAs("m_Vertexes")]
		private int[] m_Vertices;

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000559 RID: 1369 RVA: 0x00035E81 File Offset: 0x00034081
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x00035E8D File Offset: 0x0003408D
			public <>c()
			{
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x00035E95 File Offset: 0x00034095
			internal bool <SortedRemoveAndShift>b__26_0(KeyValuePair<int, int> x)
			{
				return x.Value > -1;
			}

			// Token: 0x040002BA RID: 698
			public static readonly SharedVertex.<>c <>9 = new SharedVertex.<>c();

			// Token: 0x040002BB RID: 699
			public static Func<KeyValuePair<int, int>, bool> <>9__26_0;
		}
	}
}
