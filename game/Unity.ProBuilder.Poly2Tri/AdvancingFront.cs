using System;
using System.Text;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000007 RID: 7
	internal class AdvancingFront
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002D08 File Offset: 0x00000F08
		public AdvancingFront(AdvancingFrontNode head, AdvancingFrontNode tail)
		{
			this.Head = head;
			this.Tail = tail;
			this.Search = head;
			this.AddNode(head);
			this.AddNode(tail);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D33 File Offset: 0x00000F33
		public void AddNode(AdvancingFrontNode node)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D35 File Offset: 0x00000F35
		public void RemoveNode(AdvancingFrontNode node)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D38 File Offset: 0x00000F38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (AdvancingFrontNode advancingFrontNode = this.Head; advancingFrontNode != this.Tail; advancingFrontNode = advancingFrontNode.Next)
			{
				stringBuilder.Append(advancingFrontNode.Point.X).Append("->");
			}
			stringBuilder.Append(this.Tail.Point.X);
			return stringBuilder.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D9D File Offset: 0x00000F9D
		private AdvancingFrontNode FindSearchNode(double x)
		{
			return this.Search;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DA5 File Offset: 0x00000FA5
		public AdvancingFrontNode LocateNode(TriangulationPoint point)
		{
			return this.LocateNode(point.X);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private AdvancingFrontNode LocateNode(double x)
		{
			AdvancingFrontNode advancingFrontNode = this.FindSearchNode(x);
			if (x < advancingFrontNode.Value)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null)
				{
					if (x >= advancingFrontNode.Value)
					{
						this.Search = advancingFrontNode;
						return advancingFrontNode;
					}
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null)
				{
					if (x < advancingFrontNode.Value)
					{
						this.Search = advancingFrontNode.Prev;
						return advancingFrontNode.Prev;
					}
				}
			}
			return null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E1C File Offset: 0x0000101C
		public AdvancingFrontNode LocatePoint(TriangulationPoint point)
		{
			double x = point.X;
			AdvancingFrontNode advancingFrontNode = this.FindSearchNode(x);
			double x2 = advancingFrontNode.Point.X;
			if (x == x2)
			{
				if (point != advancingFrontNode.Point)
				{
					if (point == advancingFrontNode.Prev.Point)
					{
						advancingFrontNode = advancingFrontNode.Prev;
					}
					else
					{
						if (point != advancingFrontNode.Next.Point)
						{
							throw new Exception("Failed to find Node for given afront point");
						}
						advancingFrontNode = advancingFrontNode.Next;
					}
				}
			}
			else if (x < x2)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null)
				{
					if (point == advancingFrontNode.Point)
					{
						break;
					}
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null && point != advancingFrontNode.Point)
				{
				}
			}
			this.Search = advancingFrontNode;
			return advancingFrontNode;
		}

		// Token: 0x0400000F RID: 15
		public AdvancingFrontNode Head;

		// Token: 0x04000010 RID: 16
		public AdvancingFrontNode Tail;

		// Token: 0x04000011 RID: 17
		protected AdvancingFrontNode Search;
	}
}
