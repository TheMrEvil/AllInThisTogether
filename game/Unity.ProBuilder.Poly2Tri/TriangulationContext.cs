using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000017 RID: 23
	internal abstract class TriangulationContext
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004AF3 File Offset: 0x00002CF3
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004AFB File Offset: 0x00002CFB
		public TriangulationDebugContext DebugContext
		{
			[CompilerGenerated]
			get
			{
				return this.<DebugContext>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DebugContext>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004B04 File Offset: 0x00002D04
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004B0C File Offset: 0x00002D0C
		public TriangulationMode TriangulationMode
		{
			[CompilerGenerated]
			get
			{
				return this.<TriangulationMode>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<TriangulationMode>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B15 File Offset: 0x00002D15
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004B1D File Offset: 0x00002D1D
		public Triangulatable Triangulatable
		{
			[CompilerGenerated]
			get
			{
				return this.<Triangulatable>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Triangulatable>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004B26 File Offset: 0x00002D26
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004B2E File Offset: 0x00002D2E
		public int StepCount
		{
			[CompilerGenerated]
			get
			{
				return this.<StepCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StepCount>k__BackingField = value;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004B38 File Offset: 0x00002D38
		public void Done()
		{
			int stepCount = this.StepCount;
			this.StepCount = stepCount + 1;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BF RID: 191
		public abstract TriangulationAlgorithm Algorithm { get; }

		// Token: 0x060000C0 RID: 192 RVA: 0x00004B55 File Offset: 0x00002D55
		public virtual void PrepareTriangulation(Triangulatable t)
		{
			this.Triangulatable = t;
			this.TriangulationMode = t.TriangulationMode;
			t.Prepare(this);
		}

		// Token: 0x060000C1 RID: 193
		public abstract TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b);

		// Token: 0x060000C2 RID: 194 RVA: 0x00004B71 File Offset: 0x00002D71
		public void Update(string message)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004B73 File Offset: 0x00002D73
		public virtual void Clear()
		{
			this.Points.Clear();
			if (this.DebugContext != null)
			{
				this.DebugContext.Clear();
			}
			this.StepCount = 0;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004B9A File Offset: 0x00002D9A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004BA2 File Offset: 0x00002DA2
		public virtual bool IsDebugEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDebugEnabled>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsDebugEnabled>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004BAB File Offset: 0x00002DAB
		public DTSweepDebugContext DTDebugContext
		{
			get
			{
				return this.DebugContext as DTSweepDebugContext;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004BB8 File Offset: 0x00002DB8
		protected TriangulationContext()
		{
		}

		// Token: 0x0400003A RID: 58
		[CompilerGenerated]
		private TriangulationDebugContext <DebugContext>k__BackingField;

		// Token: 0x0400003B RID: 59
		public readonly List<DelaunayTriangle> Triangles = new List<DelaunayTriangle>();

		// Token: 0x0400003C RID: 60
		public readonly List<TriangulationPoint> Points = new List<TriangulationPoint>();

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private TriangulationMode <TriangulationMode>k__BackingField;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		private Triangulatable <Triangulatable>k__BackingField;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private int <StepCount>k__BackingField;

		// Token: 0x04000040 RID: 64
		[CompilerGenerated]
		private bool <IsDebugEnabled>k__BackingField;
	}
}
