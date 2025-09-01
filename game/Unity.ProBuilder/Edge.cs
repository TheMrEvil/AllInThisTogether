using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public struct Edge : IEquatable<Edge>
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		public Edge(int a, int b)
		{
			this.a = a;
			this.b = b;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000EFCB File Offset: 0x0000D1CB
		public bool IsValid()
		{
			return this.a > -1 && this.b > -1 && this.a != this.b;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.a.ToString(),
				", ",
				this.b.ToString(),
				"]"
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000F040 File Offset: 0x0000D240
		public bool Equals(Edge other)
		{
			return (this.a == other.a && this.b == other.b) || (this.a == other.b && this.b == other.a);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000F07E File Offset: 0x0000D27E
		public override bool Equals(object obj)
		{
			return obj is Edge && this.Equals((Edge)obj);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000F098 File Offset: 0x0000D298
		public override int GetHashCode()
		{
			return (27 * 29 + ((this.a < this.b) ? this.a : this.b)) * 29 + ((this.a < this.b) ? this.b : this.a);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		public static Edge operator +(Edge a, Edge b)
		{
			return new Edge(a.a + b.a, a.b + b.b);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000F108 File Offset: 0x0000D308
		public static Edge operator -(Edge a, Edge b)
		{
			return new Edge(a.a - b.a, a.b - b.b);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000F129 File Offset: 0x0000D329
		public static Edge operator +(Edge a, int b)
		{
			return new Edge(a.a + b, a.b + b);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000F140 File Offset: 0x0000D340
		public static Edge operator -(Edge a, int b)
		{
			return new Edge(a.a - b, a.b - b);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000F157 File Offset: 0x0000D357
		public static bool operator ==(Edge a, Edge b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000F161 File Offset: 0x0000D361
		public static bool operator !=(Edge a, Edge b)
		{
			return !(a == b);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000F16D File Offset: 0x0000D36D
		public static Edge Add(Edge a, Edge b)
		{
			return a + b;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000F176 File Offset: 0x0000D376
		public static Edge Subtract(Edge a, Edge b)
		{
			return a - b;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000F180 File Offset: 0x0000D380
		public bool Equals(Edge other, Dictionary<int, int> lookup)
		{
			if (lookup == null)
			{
				return this.Equals(other);
			}
			int num = lookup[this.a];
			int num2 = lookup[this.b];
			int num3 = lookup[other.a];
			int num4 = lookup[other.b];
			return (num == num3 && num2 == num4) || (num == num4 && num2 == num3);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		public bool Contains(int index)
		{
			return this.a == index || this.b == index;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		public bool Contains(Edge other)
		{
			return this.a == other.a || this.b == other.a || this.a == other.b || this.b == other.a;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000F234 File Offset: 0x0000D434
		internal bool Contains(int index, Dictionary<int, int> lookup)
		{
			int num = lookup[index];
			return lookup[this.a] == num || lookup[this.b] == num;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000F26C File Offset: 0x0000D46C
		internal static void GetIndices(IEnumerable<Edge> edges, List<int> indices)
		{
			indices.Clear();
			foreach (Edge edge in edges)
			{
				indices.Add(edge.a);
				indices.Add(edge.b);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		// Note: this type is marked as 'beforefieldinit'.
		static Edge()
		{
		}

		// Token: 0x0400004B RID: 75
		public int a;

		// Token: 0x0400004C RID: 76
		public int b;

		// Token: 0x0400004D RID: 77
		public static readonly Edge Empty = new Edge(-1, -1);
	}
}
