using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public sealed class Face
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000F82F File Offset: 0x0000DA2F
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000F837 File Offset: 0x0000DA37
		public bool manualUV
		{
			get
			{
				return this.m_ManualUV;
			}
			set
			{
				this.m_ManualUV = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000F840 File Offset: 0x0000DA40
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000F848 File Offset: 0x0000DA48
		public int textureGroup
		{
			get
			{
				return this.m_TextureGroup;
			}
			set
			{
				this.m_TextureGroup = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000F851 File Offset: 0x0000DA51
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000F859 File Offset: 0x0000DA59
		internal int[] indexesInternal
		{
			get
			{
				return this.m_Indexes;
			}
			set
			{
				if (this.m_Indexes == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.m_Indexes.Length % 3 != 0)
				{
					throw new ArgumentException("Face indexes must be a multiple of 3.");
				}
				this.m_Indexes = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000F892 File Offset: 0x0000DA92
		public ReadOnlyCollection<int> indexes
		{
			get
			{
				return new ReadOnlyCollection<int>(this.m_Indexes);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		public void SetIndexes(IEnumerable<int> indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			int[] array = indices.ToArray<int>();
			if (array.Length % 3 != 0)
			{
				throw new ArgumentException("Face indexes must be a multiple of 3.");
			}
			this.m_Indexes = array;
			this.InvalidateCache();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000F8E1 File Offset: 0x0000DAE1
		internal int[] distinctIndexesInternal
		{
			get
			{
				if (this.m_DistinctIndexes != null)
				{
					return this.m_DistinctIndexes;
				}
				return this.CacheDistinctIndexes();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public ReadOnlyCollection<int> distinctIndexes
		{
			get
			{
				return new ReadOnlyCollection<int>(this.distinctIndexesInternal);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000F905 File Offset: 0x0000DB05
		internal Edge[] edgesInternal
		{
			get
			{
				if (this.m_Edges != null)
				{
					return this.m_Edges;
				}
				return this.CacheEdges();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000F91C File Offset: 0x0000DB1C
		public ReadOnlyCollection<Edge> edges
		{
			get
			{
				return new ReadOnlyCollection<Edge>(this.edgesInternal);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000F929 File Offset: 0x0000DB29
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000F931 File Offset: 0x0000DB31
		public int smoothingGroup
		{
			get
			{
				return this.m_SmoothingGroup;
			}
			set
			{
				this.m_SmoothingGroup = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000F93A File Offset: 0x0000DB3A
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000F942 File Offset: 0x0000DB42
		[Obsolete("Face.material is deprecated. Please use submeshIndex instead.")]
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000F94B File Offset: 0x0000DB4B
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000F953 File Offset: 0x0000DB53
		public int submeshIndex
		{
			get
			{
				return this.m_SubmeshIndex;
			}
			set
			{
				this.m_SubmeshIndex = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000F95C File Offset: 0x0000DB5C
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000F964 File Offset: 0x0000DB64
		public AutoUnwrapSettings uv
		{
			get
			{
				return this.m_Uv;
			}
			set
			{
				this.m_Uv = value;
			}
		}

		// Token: 0x17000039 RID: 57
		public int this[int i]
		{
			get
			{
				return this.indexesInternal[i];
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000F977 File Offset: 0x0000DB77
		public Face()
		{
			this.m_SubmeshIndex = 0;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000F988 File Offset: 0x0000DB88
		public Face(IEnumerable<int> indices)
		{
			this.SetIndexes(indices);
			this.m_Uv = AutoUnwrapSettings.tile;
			this.m_Material = BuiltinMaterials.defaultMaterial;
			this.m_SmoothingGroup = 0;
			this.m_SubmeshIndex = 0;
			this.textureGroup = -1;
			this.elementGroup = 0;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		[Obsolete("Face.material is deprecated. Please use \"submeshIndex\" instead.")]
		internal Face(int[] triangles, Material m, AutoUnwrapSettings u, int smoothing, int texture, int element, bool manualUVs)
		{
			this.SetIndexes(triangles);
			this.m_Uv = new AutoUnwrapSettings(u);
			this.m_Material = m;
			this.m_SmoothingGroup = smoothing;
			this.textureGroup = texture;
			this.elementGroup = element;
			this.manualUV = manualUVs;
			this.m_SubmeshIndex = 0;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000FA28 File Offset: 0x0000DC28
		internal Face(IEnumerable<int> triangles, int submeshIndex, AutoUnwrapSettings u, int smoothing, int texture, int element, bool manualUVs)
		{
			this.SetIndexes(triangles);
			this.m_Uv = new AutoUnwrapSettings(u);
			this.m_SmoothingGroup = smoothing;
			this.textureGroup = texture;
			this.elementGroup = element;
			this.manualUV = manualUVs;
			this.m_SubmeshIndex = submeshIndex;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000FA75 File Offset: 0x0000DC75
		public Face(Face other)
		{
			this.CopyFrom(other);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public void CopyFrom(Face other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			int num = other.indexesInternal.Length;
			this.m_Indexes = new int[num];
			Array.Copy(other.indexesInternal, this.m_Indexes, num);
			this.m_SmoothingGroup = other.smoothingGroup;
			this.m_Uv = new AutoUnwrapSettings(other.uv);
			this.m_Material = other.material;
			this.manualUV = other.manualUV;
			this.m_TextureGroup = other.textureGroup;
			this.elementGroup = other.elementGroup;
			this.m_SubmeshIndex = other.m_SubmeshIndex;
			this.InvalidateCache();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000FB25 File Offset: 0x0000DD25
		internal void InvalidateCache()
		{
			this.m_Edges = null;
			this.m_DistinctIndexes = null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000FB38 File Offset: 0x0000DD38
		private Edge[] CacheEdges()
		{
			if (this.m_Indexes == null)
			{
				return null;
			}
			HashSet<Edge> hashSet = new HashSet<Edge>();
			List<Edge> list = new List<Edge>();
			for (int i = 0; i < this.indexesInternal.Length; i += 3)
			{
				Edge item = new Edge(this.indexesInternal[i], this.indexesInternal[i + 1]);
				Edge item2 = new Edge(this.indexesInternal[i + 1], this.indexesInternal[i + 2]);
				Edge item3 = new Edge(this.indexesInternal[i + 2], this.indexesInternal[i]);
				if (!hashSet.Add(item))
				{
					list.Add(item);
				}
				if (!hashSet.Add(item2))
				{
					list.Add(item2);
				}
				if (!hashSet.Add(item3))
				{
					list.Add(item3);
				}
			}
			hashSet.ExceptWith(list);
			this.m_Edges = hashSet.ToArray<Edge>();
			return this.m_Edges;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000FC0E File Offset: 0x0000DE0E
		private int[] CacheDistinctIndexes()
		{
			if (this.m_Indexes == null)
			{
				return null;
			}
			this.m_DistinctIndexes = this.m_Indexes.Distinct<int>().ToArray<int>();
			return this.distinctIndexesInternal;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000FC38 File Offset: 0x0000DE38
		public bool Contains(int a, int b, int c)
		{
			int i = 0;
			int num = this.indexesInternal.Length;
			while (i < num)
			{
				if (a == this.indexesInternal[i] && b == this.indexesInternal[i + 1] && c == this.indexesInternal[i + 2])
				{
					return true;
				}
				i += 3;
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000FC82 File Offset: 0x0000DE82
		public bool IsQuad()
		{
			return this.edgesInternal != null && this.edgesInternal.Length == 4;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public int[] ToQuad()
		{
			if (!this.IsQuad())
			{
				throw new InvalidOperationException("Face is not representable as a quad. Use Face.IsQuad to check for validity.");
			}
			int[] array = new int[]
			{
				this.edgesInternal[0].a,
				this.edgesInternal[0].b,
				-1,
				-1
			};
			if (this.edgesInternal[1].a == array[1])
			{
				array[2] = this.edgesInternal[1].b;
			}
			else if (this.edgesInternal[2].a == array[1])
			{
				array[2] = this.edgesInternal[2].b;
			}
			else if (this.edgesInternal[3].a == array[1])
			{
				array[2] = this.edgesInternal[3].b;
			}
			if (this.edgesInternal[1].a == array[2])
			{
				array[3] = this.edgesInternal[1].b;
			}
			else if (this.edgesInternal[2].a == array[2])
			{
				array[3] = this.edgesInternal[2].b;
			}
			else if (this.edgesInternal[3].a == array[2])
			{
				array[3] = this.edgesInternal[3].b;
			}
			return array;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.indexesInternal.Length; i += 3)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(this.indexesInternal[i]);
				stringBuilder.Append(", ");
				stringBuilder.Append(this.indexesInternal[i + 1]);
				stringBuilder.Append(", ");
				stringBuilder.Append(this.indexesInternal[i + 2]);
				stringBuilder.Append("]");
				if (i < this.indexesInternal.Length - 3)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public void ShiftIndexes(int offset)
		{
			int i = 0;
			int num = this.m_Indexes.Length;
			while (i < num)
			{
				this.m_Indexes[i] += offset;
				i++;
			}
			this.InvalidateCache();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		private int SmallestIndexValue()
		{
			int num = this.m_Indexes[0];
			for (int i = 1; i < this.m_Indexes.Length; i++)
			{
				if (this.m_Indexes[i] < num)
				{
					num = this.m_Indexes[i];
				}
			}
			return num;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000FF20 File Offset: 0x0000E120
		public void ShiftIndexesToZero()
		{
			int num = this.SmallestIndexValue();
			for (int i = 0; i < this.m_Indexes.Length; i++)
			{
				this.m_Indexes[i] -= num;
			}
			this.InvalidateCache();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000FF5E File Offset: 0x0000E15E
		public void Reverse()
		{
			Array.Reverse<int>(this.m_Indexes);
			this.InvalidateCache();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000FF74 File Offset: 0x0000E174
		internal static void GetIndices(IEnumerable<Face> faces, List<int> indices)
		{
			indices.Clear();
			foreach (Face face in faces)
			{
				int i = 0;
				int num = face.indexesInternal.Length;
				while (i < num)
				{
					indices.Add(face.indexesInternal[i]);
					i++;
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		internal static void GetDistinctIndices(IEnumerable<Face> faces, List<int> indices)
		{
			indices.Clear();
			foreach (Face face in faces)
			{
				int i = 0;
				int num = face.distinctIndexesInternal.Length;
				while (i < num)
				{
					indices.Add(face.distinctIndexesInternal[i]);
					i++;
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0001004C File Offset: 0x0000E24C
		internal bool TryGetNextEdge(Edge source, int index, ref Edge nextEdge, ref int nextIndex)
		{
			int i = 0;
			int num = this.edgesInternal.Length;
			while (i < num)
			{
				if (!(this.edgesInternal[i] == source))
				{
					nextEdge = this.edgesInternal[i];
					if (nextEdge.Contains(index))
					{
						nextIndex = ((nextEdge.a == index) ? nextEdge.b : nextEdge.a);
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x04000052 RID: 82
		[FormerlySerializedAs("_indices")]
		[SerializeField]
		private int[] m_Indexes;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		[FormerlySerializedAs("_smoothingGroup")]
		private int m_SmoothingGroup;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		[FormerlySerializedAs("_uv")]
		private AutoUnwrapSettings m_Uv;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		[FormerlySerializedAs("_mat")]
		private Material m_Material;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private int m_SubmeshIndex;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		[FormerlySerializedAs("manualUV")]
		private bool m_ManualUV;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		internal int elementGroup;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private int m_TextureGroup;

		// Token: 0x0400005A RID: 90
		[NonSerialized]
		private int[] m_DistinctIndexes;

		// Token: 0x0400005B RID: 91
		[NonSerialized]
		private Edge[] m_Edges;
	}
}
