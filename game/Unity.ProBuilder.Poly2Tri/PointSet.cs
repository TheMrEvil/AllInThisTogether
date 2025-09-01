using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000014 RID: 20
	internal class PointSet : Triangulatable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004A06 File Offset: 0x00002C06
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004A0E File Offset: 0x00002C0E
		public IList<TriangulationPoint> Points
		{
			[CompilerGenerated]
			get
			{
				return this.<Points>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Points>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004A17 File Offset: 0x00002C17
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004A1F File Offset: 0x00002C1F
		public IList<DelaunayTriangle> Triangles
		{
			[CompilerGenerated]
			get
			{
				return this.<Triangles>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Triangles>k__BackingField = value;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004A28 File Offset: 0x00002C28
		public PointSet(List<TriangulationPoint> points)
		{
			this.Points = new List<TriangulationPoint>(points);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004A3C File Offset: 0x00002C3C
		public virtual TriangulationMode TriangulationMode
		{
			get
			{
				return TriangulationMode.Unconstrained;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004A3F File Offset: 0x00002C3F
		public void AddTriangle(DelaunayTriangle t)
		{
			this.Triangles.Add(t);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A50 File Offset: 0x00002C50
		public void AddTriangles(IEnumerable<DelaunayTriangle> list)
		{
			foreach (DelaunayTriangle item in list)
			{
				this.Triangles.Add(item);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public void ClearTriangles()
		{
			this.Triangles.Clear();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004AAD File Offset: 0x00002CAD
		public virtual void Prepare(TriangulationContext tcx)
		{
			if (this.Triangles == null)
			{
				this.Triangles = new List<DelaunayTriangle>(this.Points.Count);
			}
			else
			{
				this.Triangles.Clear();
			}
			tcx.Points.AddRange(this.Points);
		}

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		private IList<TriangulationPoint> <Points>k__BackingField;

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		private IList<DelaunayTriangle> <Triangles>k__BackingField;
	}
}
