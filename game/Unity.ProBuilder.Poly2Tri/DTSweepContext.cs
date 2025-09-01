using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200000C RID: 12
	internal class DTSweepContext : TriangulationContext
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004491 File Offset: 0x00002691
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00004499 File Offset: 0x00002699
		public TriangulationPoint Head
		{
			[CompilerGenerated]
			get
			{
				return this.<Head>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Head>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000044A2 File Offset: 0x000026A2
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000044AA File Offset: 0x000026AA
		public TriangulationPoint Tail
		{
			[CompilerGenerated]
			get
			{
				return this.<Tail>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tail>k__BackingField = value;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044B3 File Offset: 0x000026B3
		public DTSweepContext()
		{
			this.Clear();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000044ED File Offset: 0x000026ED
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000044F5 File Offset: 0x000026F5
		public override bool IsDebugEnabled
		{
			get
			{
				return base.IsDebugEnabled;
			}
			protected set
			{
				if (value && base.DebugContext == null)
				{
					base.DebugContext = new DTSweepDebugContext(this);
				}
				base.IsDebugEnabled = value;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004515 File Offset: 0x00002715
		public void RemoveFromList(DelaunayTriangle triangle)
		{
			this.Triangles.Remove(triangle);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004524 File Offset: 0x00002724
		public void MeshClean(DelaunayTriangle triangle)
		{
			this.MeshCleanReq(triangle);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004530 File Offset: 0x00002730
		private void MeshCleanReq(DelaunayTriangle triangle)
		{
			if (triangle != null && !triangle.IsInterior)
			{
				triangle.IsInterior = true;
				base.Triangulatable.AddTriangle(triangle);
				for (int i = 0; i < 3; i++)
				{
					if (!triangle.EdgeIsConstrained[i])
					{
						this.MeshCleanReq(triangle.Neighbors[i]);
					}
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004587 File Offset: 0x00002787
		public override void Clear()
		{
			base.Clear();
			this.Triangles.Clear();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000459A File Offset: 0x0000279A
		public void AddNode(AdvancingFrontNode node)
		{
			this.Front.AddNode(node);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000045A8 File Offset: 0x000027A8
		public void RemoveNode(AdvancingFrontNode node)
		{
			this.Front.RemoveNode(node);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000045B6 File Offset: 0x000027B6
		public AdvancingFrontNode LocateNode(TriangulationPoint point)
		{
			return this.Front.LocateNode(point);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000045C4 File Offset: 0x000027C4
		public void CreateAdvancingFront()
		{
			DelaunayTriangle delaunayTriangle = new DelaunayTriangle(this.Points[0], this.Tail, this.Head);
			this.Triangles.Add(delaunayTriangle);
			AdvancingFrontNode advancingFrontNode = new AdvancingFrontNode(delaunayTriangle.Points[1]);
			advancingFrontNode.Triangle = delaunayTriangle;
			AdvancingFrontNode advancingFrontNode2 = new AdvancingFrontNode(delaunayTriangle.Points[0]);
			advancingFrontNode2.Triangle = delaunayTriangle;
			AdvancingFrontNode tail = new AdvancingFrontNode(delaunayTriangle.Points[2]);
			this.Front = new AdvancingFront(advancingFrontNode, tail);
			this.Front.AddNode(advancingFrontNode2);
			this.Front.Head.Next = advancingFrontNode2;
			advancingFrontNode2.Next = this.Front.Tail;
			advancingFrontNode2.Prev = this.Front.Head;
			this.Front.Tail.Prev = advancingFrontNode2;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000469C File Offset: 0x0000289C
		public void MapTriangleToNodes(DelaunayTriangle t)
		{
			for (int i = 0; i < 3; i++)
			{
				if (t.Neighbors[i] == null)
				{
					AdvancingFrontNode advancingFrontNode = this.Front.LocatePoint(t.PointCWFrom(t.Points[i]));
					if (advancingFrontNode != null)
					{
						advancingFrontNode.Triangle = t;
					}
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000046EC File Offset: 0x000028EC
		public override void PrepareTriangulation(Triangulatable t)
		{
			base.PrepareTriangulation(t);
			double x;
			double num = x = this.Points[0].X;
			double y;
			double num2 = y = this.Points[0].Y;
			foreach (TriangulationPoint triangulationPoint in this.Points)
			{
				if (triangulationPoint.X > x)
				{
					x = triangulationPoint.X;
				}
				if (triangulationPoint.X < num)
				{
					num = triangulationPoint.X;
				}
				if (triangulationPoint.Y > y)
				{
					y = triangulationPoint.Y;
				}
				if (triangulationPoint.Y < num2)
				{
					num2 = triangulationPoint.Y;
				}
			}
			double num3 = (double)this.ALPHA * (x - num);
			double num4 = (double)this.ALPHA * (y - num2);
			TriangulationPoint head = new TriangulationPoint(x + num3, num2 - num4, -1);
			TriangulationPoint tail = new TriangulationPoint(num - num3, num2 - num4, -1);
			this.Head = head;
			this.Tail = tail;
			this.Points.Sort(this._comparator);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000480C File Offset: 0x00002A0C
		public void FinalizeTriangulation()
		{
			base.Triangulatable.AddTriangles(this.Triangles);
			this.Triangles.Clear();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000482A File Offset: 0x00002A2A
		public override TriangulationConstraint NewConstraint(TriangulationPoint a, TriangulationPoint b)
		{
			return new DTSweepConstraint(a, b);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004833 File Offset: 0x00002A33
		public override TriangulationAlgorithm Algorithm
		{
			get
			{
				return TriangulationAlgorithm.DTSweep;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly float ALPHA = 0.3f;

		// Token: 0x0400001F RID: 31
		public AdvancingFront Front;

		// Token: 0x04000020 RID: 32
		[CompilerGenerated]
		private TriangulationPoint <Head>k__BackingField;

		// Token: 0x04000021 RID: 33
		[CompilerGenerated]
		private TriangulationPoint <Tail>k__BackingField;

		// Token: 0x04000022 RID: 34
		public DTSweepBasin Basin = new DTSweepBasin();

		// Token: 0x04000023 RID: 35
		public DTSweepEdgeEvent EdgeEvent = new DTSweepEdgeEvent();

		// Token: 0x04000024 RID: 36
		private DTSweepPointComparator _comparator = new DTSweepPointComparator();
	}
}
