using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000008 RID: 8
	internal class AdvancingFrontNode
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002EC0 File Offset: 0x000010C0
		public AdvancingFrontNode(TriangulationPoint point)
		{
			this.Point = point;
			this.Value = point.X;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002EDB File Offset: 0x000010DB
		public bool HasNext
		{
			get
			{
				return this.Next != null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EE6 File Offset: 0x000010E6
		public bool HasPrev
		{
			get
			{
				return this.Prev != null;
			}
		}

		// Token: 0x04000012 RID: 18
		public AdvancingFrontNode Next;

		// Token: 0x04000013 RID: 19
		public AdvancingFrontNode Prev;

		// Token: 0x04000014 RID: 20
		public double Value;

		// Token: 0x04000015 RID: 21
		public TriangulationPoint Point;

		// Token: 0x04000016 RID: 22
		public DelaunayTriangle Triangle;
	}
}
