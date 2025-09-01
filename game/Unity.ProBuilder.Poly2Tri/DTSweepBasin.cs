using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200000A RID: 10
	internal class DTSweepBasin
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004402 File Offset: 0x00002602
		public DTSweepBasin()
		{
		}

		// Token: 0x04000019 RID: 25
		public AdvancingFrontNode leftNode;

		// Token: 0x0400001A RID: 26
		public AdvancingFrontNode bottomNode;

		// Token: 0x0400001B RID: 27
		public AdvancingFrontNode rightNode;

		// Token: 0x0400001C RID: 28
		public double width;

		// Token: 0x0400001D RID: 29
		public bool leftHighest;
	}
}
