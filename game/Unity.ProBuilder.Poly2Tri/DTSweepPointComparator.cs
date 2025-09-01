using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200000F RID: 15
	internal class DTSweepPointComparator : IComparer<TriangulationPoint>
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004914 File Offset: 0x00002B14
		public int Compare(TriangulationPoint p1, TriangulationPoint p2)
		{
			if (p1.Y < p2.Y)
			{
				return -1;
			}
			if (p1.Y > p2.Y)
			{
				return 1;
			}
			if (p1.X < p2.X)
			{
				return -1;
			}
			if (p1.X > p2.X)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004962 File Offset: 0x00002B62
		public DTSweepPointComparator()
		{
		}
	}
}
