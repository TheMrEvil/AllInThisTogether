using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200000B RID: 11
	internal class DTSweepConstraint : TriangulationConstraint
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000440C File Offset: 0x0000260C
		public DTSweepConstraint(TriangulationPoint p1, TriangulationPoint p2)
		{
			this.P = p1;
			this.Q = p2;
			if (p1.Y > p2.Y)
			{
				this.Q = p1;
				this.P = p2;
			}
			else if (p1.Y == p2.Y)
			{
				if (p1.X > p2.X)
				{
					this.Q = p1;
					this.P = p2;
				}
				else
				{
					double x = p1.X;
					double x2 = p2.X;
				}
			}
			this.Q.AddEdge(this);
		}
	}
}
