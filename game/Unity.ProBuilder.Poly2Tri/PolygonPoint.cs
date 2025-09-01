using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000004 RID: 4
	internal class PolygonPoint : TriangulationPoint
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002574 File Offset: 0x00000774
		public PolygonPoint(double x, double y, int index = -1) : base(x, y, index)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002587 File Offset: 0x00000787
		public PolygonPoint Next
		{
			[CompilerGenerated]
			get
			{
				return this.<Next>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Next>k__BackingField = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002598 File Offset: 0x00000798
		public PolygonPoint Previous
		{
			[CompilerGenerated]
			get
			{
				return this.<Previous>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Previous>k__BackingField = value;
			}
		}

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private PolygonPoint <Next>k__BackingField;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		private PolygonPoint <Previous>k__BackingField;
	}
}
