using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000013 RID: 19
	internal class ConstrainedPointSet : PointSet
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004989 File Offset: 0x00002B89
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004991 File Offset: 0x00002B91
		public int[] EdgeIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<EdgeIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EdgeIndex>k__BackingField = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000499A File Offset: 0x00002B9A
		public ConstrainedPointSet(List<TriangulationPoint> points, int[] index) : base(points)
		{
			this.EdgeIndex = index;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000049AA File Offset: 0x00002BAA
		public override TriangulationMode TriangulationMode
		{
			get
			{
				return TriangulationMode.Constrained;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000049B0 File Offset: 0x00002BB0
		public override void Prepare(TriangulationContext tcx)
		{
			base.Prepare(tcx);
			for (int i = 0; i < this.EdgeIndex.Length; i += 2)
			{
				tcx.NewConstraint(base.Points[this.EdgeIndex[i]], base.Points[this.EdgeIndex[i + 1]]);
			}
		}

		// Token: 0x04000033 RID: 51
		[CompilerGenerated]
		private int[] <EdgeIndex>k__BackingField;
	}
}
