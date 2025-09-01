using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal struct Triangle : IEquatable<Triangle>
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00021557 File Offset: 0x0001F757
		public int a
		{
			get
			{
				return this.m_A;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0002155F File Offset: 0x0001F75F
		public int b
		{
			get
			{
				return this.m_B;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00021567 File Offset: 0x0001F767
		public int c
		{
			get
			{
				return this.m_C;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0002156F File Offset: 0x0001F76F
		public IEnumerable<int> indices
		{
			get
			{
				return new int[]
				{
					this.m_A,
					this.m_B,
					this.m_C
				};
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00021592 File Offset: 0x0001F792
		public Triangle(int a, int b, int c)
		{
			this.m_A = a;
			this.m_B = b;
			this.m_C = c;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000215A9 File Offset: 0x0001F7A9
		public bool Equals(Triangle other)
		{
			return this.m_A == other.a && this.m_B == other.b && this.m_C == other.c;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000215DC File Offset: 0x0001F7DC
		public override bool Equals(object obj)
		{
			if (obj is Triangle)
			{
				Triangle other = (Triangle)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00021601 File Offset: 0x0001F801
		public override int GetHashCode()
		{
			return (this.m_A * 397 ^ this.m_B) * 397 ^ this.m_C;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00021624 File Offset: 0x0001F824
		public bool IsAdjacent(Triangle other)
		{
			return other.ContainsEdge(new Edge(this.a, this.b)) || other.ContainsEdge(new Edge(this.b, this.c)) || other.ContainsEdge(new Edge(this.c, this.a));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00021680 File Offset: 0x0001F880
		private bool ContainsEdge(Edge edge)
		{
			return new Edge(this.a, this.b) == edge || new Edge(this.b, this.c) == edge || new Edge(this.c, this.a) == edge;
		}

		// Token: 0x04000204 RID: 516
		[SerializeField]
		private int m_A;

		// Token: 0x04000205 RID: 517
		[SerializeField]
		private int m_B;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		private int m_C;
	}
}
