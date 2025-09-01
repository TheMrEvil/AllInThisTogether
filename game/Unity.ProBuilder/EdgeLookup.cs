using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000015 RID: 21
	public struct EdgeLookup : IEquatable<EdgeLookup>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000F2DA File Offset: 0x0000D4DA
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
		public Edge local
		{
			get
			{
				return this.m_Local;
			}
			set
			{
				this.m_Local = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000F2EB File Offset: 0x0000D4EB
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000F2F3 File Offset: 0x0000D4F3
		public Edge common
		{
			get
			{
				return this.m_Common;
			}
			set
			{
				this.m_Common = value;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		public EdgeLookup(Edge common, Edge local)
		{
			this.m_Common = common;
			this.m_Local = local;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000F30C File Offset: 0x0000D50C
		public EdgeLookup(int cx, int cy, int x, int y)
		{
			this.m_Common = new Edge(cx, cy);
			this.m_Local = new Edge(x, y);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000F32C File Offset: 0x0000D52C
		public bool Equals(EdgeLookup other)
		{
			return other.common.Equals(this.common);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000F34E File Offset: 0x0000D54E
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals((EdgeLookup)obj);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000F364 File Offset: 0x0000D564
		public override int GetHashCode()
		{
			return this.common.GetHashCode();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000F385 File Offset: 0x0000D585
		public static bool operator ==(EdgeLookup a, EdgeLookup b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000F398 File Offset: 0x0000D598
		public static bool operator !=(EdgeLookup a, EdgeLookup b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public override string ToString()
		{
			return string.Format("Common: ({0}, {1}), local: ({2}, {3})", new object[]
			{
				this.common.a,
				this.common.b,
				this.local.a,
				this.local.b
			});
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000F41C File Offset: 0x0000D61C
		public static IEnumerable<EdgeLookup> GetEdgeLookup(IEnumerable<Edge> edges, Dictionary<int, int> lookup)
		{
			return from x in edges
			select new EdgeLookup(new Edge(lookup[x.a], lookup[x.b]), x);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000F448 File Offset: 0x0000D648
		public static HashSet<EdgeLookup> GetEdgeLookupHashSet(IEnumerable<Edge> edges, Dictionary<int, int> lookup)
		{
			if (lookup == null || edges == null)
			{
				return null;
			}
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>();
			foreach (Edge edge in edges)
			{
				hashSet.Add(new EdgeLookup(new Edge(lookup[edge.a], lookup[edge.b]), edge));
			}
			return hashSet;
		}

		// Token: 0x0400004E RID: 78
		private Edge m_Local;

		// Token: 0x0400004F RID: 79
		private Edge m_Common;

		// Token: 0x02000094 RID: 148
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x00035B11 File Offset: 0x00033D11
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x00035B19 File Offset: 0x00033D19
			internal EdgeLookup <GetEdgeLookup>b__0(Edge x)
			{
				return new EdgeLookup(new Edge(this.lookup[x.a], this.lookup[x.b]), x);
			}

			// Token: 0x04000298 RID: 664
			public Dictionary<int, int> lookup;
		}
	}
}
