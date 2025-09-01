using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001A RID: 26
	internal class TriangulationPoint
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004BE5 File Offset: 0x00002DE5
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004BED File Offset: 0x00002DED
		public List<DTSweepConstraint> Edges
		{
			[CompilerGenerated]
			get
			{
				return this.<Edges>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Edges>k__BackingField = value;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BF6 File Offset: 0x00002DF6
		public TriangulationPoint(double x, double y, int index = -1)
		{
			this.X = x;
			this.Y = y;
			this.Index = index;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004C14 File Offset: 0x00002E14
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.X.ToString(),
				",",
				this.Y.ToString(),
				"]"
			});
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004C60 File Offset: 0x00002E60
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004C69 File Offset: 0x00002E69
		public float Xf
		{
			get
			{
				return (float)this.X;
			}
			set
			{
				this.X = (double)value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004C73 File Offset: 0x00002E73
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004C7C File Offset: 0x00002E7C
		public float Yf
		{
			get
			{
				return (float)this.Y;
			}
			set
			{
				this.Y = (double)value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C86 File Offset: 0x00002E86
		public void AddEdge(DTSweepConstraint e)
		{
			if (this.Edges == null)
			{
				this.Edges = new List<DTSweepConstraint>();
			}
			this.Edges.Add(e);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public bool HasEdges
		{
			get
			{
				return this.Edges != null;
			}
		}

		// Token: 0x04000046 RID: 70
		public const int INSERTED_INDEX = -1;

		// Token: 0x04000047 RID: 71
		public const int INVALID_INDEX = -2;

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private List<DTSweepConstraint> <Edges>k__BackingField;

		// Token: 0x04000049 RID: 73
		public double X;

		// Token: 0x0400004A RID: 74
		public double Y;

		// Token: 0x0400004B RID: 75
		public int Index;
	}
}
