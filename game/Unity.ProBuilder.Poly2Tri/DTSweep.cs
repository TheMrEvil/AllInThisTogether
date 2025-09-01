using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000009 RID: 9
	internal static class DTSweep
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002EF1 File Offset: 0x000010F1
		public static void Triangulate(DTSweepContext tcx)
		{
			tcx.CreateAdvancingFront();
			DTSweep.Sweep(tcx);
			if (tcx.TriangulationMode == TriangulationMode.Polygon)
			{
				DTSweep.FinalizationPolygon(tcx);
			}
			else
			{
				DTSweep.FinalizationConvexHull(tcx);
			}
			tcx.Done();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F1C File Offset: 0x0000111C
		private static void Sweep(DTSweepContext tcx)
		{
			List<TriangulationPoint> points = tcx.Points;
			for (int i = 1; i < points.Count; i++)
			{
				TriangulationPoint triangulationPoint = points[i];
				AdvancingFrontNode node = DTSweep.PointEvent(tcx, triangulationPoint);
				if (triangulationPoint.HasEdges)
				{
					foreach (DTSweepConstraint dtsweepConstraint in triangulationPoint.Edges)
					{
						if (tcx.IsDebugEnabled)
						{
							tcx.DTDebugContext.ActiveConstraint = dtsweepConstraint;
						}
						DTSweep.EdgeEvent(tcx, dtsweepConstraint, node);
					}
				}
				tcx.Update(null);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FC0 File Offset: 0x000011C0
		private static void FinalizationConvexHull(DTSweepContext tcx)
		{
			AdvancingFrontNode advancingFrontNode = tcx.Front.Head.Next;
			AdvancingFrontNode advancingFrontNode2 = advancingFrontNode.Next;
			TriangulationPoint point = advancingFrontNode.Point;
			DTSweep.TurnAdvancingFrontConvex(tcx, advancingFrontNode, advancingFrontNode2);
			advancingFrontNode = tcx.Front.Tail.Prev;
			DelaunayTriangle delaunayTriangle;
			if (advancingFrontNode.Triangle.Contains(advancingFrontNode.Next.Point) && advancingFrontNode.Triangle.Contains(advancingFrontNode.Prev.Point))
			{
				delaunayTriangle = advancingFrontNode.Triangle.NeighborAcrossFrom(advancingFrontNode.Point);
				DTSweep.RotateTrianglePair(advancingFrontNode.Triangle, advancingFrontNode.Point, delaunayTriangle, delaunayTriangle.OppositePoint(advancingFrontNode.Triangle, advancingFrontNode.Point));
				tcx.MapTriangleToNodes(advancingFrontNode.Triangle);
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			advancingFrontNode = tcx.Front.Head.Next;
			if (advancingFrontNode.Triangle.Contains(advancingFrontNode.Prev.Point) && advancingFrontNode.Triangle.Contains(advancingFrontNode.Next.Point))
			{
				delaunayTriangle = advancingFrontNode.Triangle.NeighborAcrossFrom(advancingFrontNode.Point);
				DTSweep.RotateTrianglePair(advancingFrontNode.Triangle, advancingFrontNode.Point, delaunayTriangle, delaunayTriangle.OppositePoint(advancingFrontNode.Triangle, advancingFrontNode.Point));
				tcx.MapTriangleToNodes(advancingFrontNode.Triangle);
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			point = tcx.Front.Head.Point;
			advancingFrontNode2 = tcx.Front.Tail.Prev;
			delaunayTriangle = advancingFrontNode2.Triangle;
			TriangulationPoint triangulationPoint = advancingFrontNode2.Point;
			for (;;)
			{
				tcx.RemoveFromList(delaunayTriangle);
				triangulationPoint = delaunayTriangle.PointCCWFrom(triangulationPoint);
				if (triangulationPoint == point)
				{
					break;
				}
				delaunayTriangle = delaunayTriangle.NeighborCCWFrom(triangulationPoint);
			}
			point = tcx.Front.Head.Next.Point;
			triangulationPoint = delaunayTriangle.PointCWFrom(tcx.Front.Head.Point);
			delaunayTriangle = delaunayTriangle.NeighborCWFrom(tcx.Front.Head.Point);
			do
			{
				tcx.RemoveFromList(delaunayTriangle);
				triangulationPoint = delaunayTriangle.PointCCWFrom(triangulationPoint);
				delaunayTriangle = delaunayTriangle.NeighborCCWFrom(triangulationPoint);
			}
			while (triangulationPoint != point);
			tcx.FinalizeTriangulation();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031C4 File Offset: 0x000013C4
		private static void TurnAdvancingFrontConvex(DTSweepContext tcx, AdvancingFrontNode b, AdvancingFrontNode c)
		{
			AdvancingFrontNode advancingFrontNode = b;
			while (c != tcx.Front.Tail)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = c;
				}
				if (TriangulationUtil.Orient2d(b.Point, c.Point, c.Next.Point) == Orientation.CCW)
				{
					DTSweep.Fill(tcx, c);
					c = c.Next;
				}
				else if (b != advancingFrontNode && TriangulationUtil.Orient2d(b.Prev.Point, b.Point, c.Point) == Orientation.CCW)
				{
					DTSweep.Fill(tcx, b);
					b = b.Prev;
				}
				else
				{
					b = c;
					c = c.Next;
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000326C File Offset: 0x0000146C
		private static void FinalizationPolygon(DTSweepContext tcx)
		{
			DelaunayTriangle delaunayTriangle = tcx.Front.Head.Next.Triangle;
			TriangulationPoint point = tcx.Front.Head.Next.Point;
			while (!delaunayTriangle.GetConstrainedEdgeCW(point))
			{
				delaunayTriangle = delaunayTriangle.NeighborCCWFrom(point);
			}
			tcx.MeshClean(delaunayTriangle);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000032C0 File Offset: 0x000014C0
		private static AdvancingFrontNode PointEvent(DTSweepContext tcx, TriangulationPoint point)
		{
			AdvancingFrontNode advancingFrontNode = tcx.LocateNode(point);
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = advancingFrontNode;
			}
			AdvancingFrontNode advancingFrontNode2 = DTSweep.NewFrontTriangle(tcx, point, advancingFrontNode);
			if (point.X <= advancingFrontNode.Point.X + TriangulationUtil.EPSILON)
			{
				DTSweep.Fill(tcx, advancingFrontNode);
			}
			tcx.AddNode(advancingFrontNode2);
			DTSweep.FillAdvancingFront(tcx, advancingFrontNode2);
			return advancingFrontNode2;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003324 File Offset: 0x00001524
		private static AdvancingFrontNode NewFrontTriangle(DTSweepContext tcx, TriangulationPoint point, AdvancingFrontNode node)
		{
			DelaunayTriangle delaunayTriangle = new DelaunayTriangle(point, node.Point, node.Next.Point);
			delaunayTriangle.MarkNeighbor(node.Triangle);
			tcx.Triangles.Add(delaunayTriangle);
			AdvancingFrontNode advancingFrontNode = new AdvancingFrontNode(point);
			advancingFrontNode.Next = node.Next;
			advancingFrontNode.Prev = node;
			node.Next.Prev = advancingFrontNode;
			node.Next = advancingFrontNode;
			tcx.AddNode(advancingFrontNode);
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = advancingFrontNode;
			}
			if (!DTSweep.Legalize(tcx, delaunayTriangle))
			{
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			return advancingFrontNode;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033BC File Offset: 0x000015BC
		private static void EdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			try
			{
				tcx.EdgeEvent.ConstrainedEdge = edge;
				tcx.EdgeEvent.Right = (edge.P.X > edge.Q.X);
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.PrimaryTriangle = node.Triangle;
				}
				if (!DTSweep.IsEdgeSideOfTriangle(node.Triangle, edge.P, edge.Q))
				{
					DTSweep.FillEdgeEvent(tcx, edge, node);
					DTSweep.EdgeEvent(tcx, edge.P, edge.Q, node.Triangle, edge.Q);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003468 File Offset: 0x00001668
		private static void FillEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			if (tcx.EdgeEvent.Right)
			{
				DTSweep.FillRightAboveEdgeEvent(tcx, edge, node);
				return;
			}
			DTSweep.FillLeftAboveEdgeEvent(tcx, edge, node);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003488 File Offset: 0x00001688
		private static void FillRightConcaveEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			DTSweep.Fill(tcx, node.Next);
			if (node.Next.Point != edge.P && TriangulationUtil.Orient2d(edge.Q, node.Next.Point, edge.P) == Orientation.CCW && TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == Orientation.CCW)
			{
				DTSweep.FillRightConcaveEdgeEvent(tcx, edge, node);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003504 File Offset: 0x00001704
		private static void FillRightConvexEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			if (TriangulationUtil.Orient2d(node.Next.Point, node.Next.Next.Point, node.Next.Next.Next.Point) == Orientation.CCW)
			{
				DTSweep.FillRightConcaveEdgeEvent(tcx, edge, node.Next);
				return;
			}
			if (TriangulationUtil.Orient2d(edge.Q, node.Next.Next.Point, edge.P) == Orientation.CCW)
			{
				DTSweep.FillRightConvexEdgeEvent(tcx, edge, node.Next);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003588 File Offset: 0x00001788
		private static void FillRightBelowEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = node;
			}
			if (node.Point.X < edge.P.X)
			{
				if (TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == Orientation.CCW)
				{
					DTSweep.FillRightConcaveEdgeEvent(tcx, edge, node);
					return;
				}
				DTSweep.FillRightConvexEdgeEvent(tcx, edge, node);
				DTSweep.FillRightBelowEdgeEvent(tcx, edge, node);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003604 File Offset: 0x00001804
		private static void FillRightAboveEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			while (node.Next.Point.X < edge.P.X)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = node;
				}
				if (TriangulationUtil.Orient2d(edge.Q, node.Next.Point, edge.P) == Orientation.CCW)
				{
					DTSweep.FillRightBelowEdgeEvent(tcx, edge, node);
				}
				else
				{
					node = node.Next;
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003678 File Offset: 0x00001878
		private static void FillLeftConvexEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			if (TriangulationUtil.Orient2d(node.Prev.Point, node.Prev.Prev.Point, node.Prev.Prev.Prev.Point) == Orientation.CW)
			{
				DTSweep.FillLeftConcaveEdgeEvent(tcx, edge, node.Prev);
				return;
			}
			if (TriangulationUtil.Orient2d(edge.Q, node.Prev.Prev.Point, edge.P) == Orientation.CW)
			{
				DTSweep.FillLeftConvexEdgeEvent(tcx, edge, node.Prev);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000036FC File Offset: 0x000018FC
		private static void FillLeftConcaveEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			DTSweep.Fill(tcx, node.Prev);
			if (node.Prev.Point != edge.P && TriangulationUtil.Orient2d(edge.Q, node.Prev.Point, edge.P) == Orientation.CW && TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point) == Orientation.CW)
			{
				DTSweep.FillLeftConcaveEdgeEvent(tcx, edge, node);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003778 File Offset: 0x00001978
		private static void FillLeftBelowEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = node;
			}
			if (node.Point.X > edge.P.X)
			{
				if (TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point) == Orientation.CW)
				{
					DTSweep.FillLeftConcaveEdgeEvent(tcx, edge, node);
					return;
				}
				DTSweep.FillLeftConvexEdgeEvent(tcx, edge, node);
				DTSweep.FillLeftBelowEdgeEvent(tcx, edge, node);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000037F4 File Offset: 0x000019F4
		private static void FillLeftAboveEdgeEvent(DTSweepContext tcx, DTSweepConstraint edge, AdvancingFrontNode node)
		{
			while (node.Prev.Point.X > edge.P.X)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = node;
				}
				if (TriangulationUtil.Orient2d(edge.Q, node.Prev.Point, edge.P) == Orientation.CW)
				{
					DTSweep.FillLeftBelowEdgeEvent(tcx, edge, node);
				}
				else
				{
					node = node.Prev;
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003864 File Offset: 0x00001A64
		private static bool IsEdgeSideOfTriangle(DelaunayTriangle triangle, TriangulationPoint ep, TriangulationPoint eq)
		{
			int num = triangle.EdgeIndex(ep, eq);
			if (num == -1)
			{
				return false;
			}
			triangle.MarkConstrainedEdge(num);
			triangle = triangle.Neighbors[num];
			if (triangle != null)
			{
				triangle.MarkConstrainedEdge(ep, eq);
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038A4 File Offset: 0x00001AA4
		private static void EdgeEvent(DTSweepContext tcx, TriangulationPoint ep, TriangulationPoint eq, DelaunayTriangle triangle, TriangulationPoint point)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.PrimaryTriangle = triangle;
			}
			if (DTSweep.IsEdgeSideOfTriangle(triangle, ep, eq))
			{
				return;
			}
			TriangulationPoint triangulationPoint = triangle.PointCCWFrom(point);
			Orientation orientation = TriangulationUtil.Orient2d(eq, triangulationPoint, ep);
			if (orientation == Orientation.Collinear)
			{
				throw new PointOnEdgeException("EdgeEvent - Point on constrained edge not supported yet", eq, triangulationPoint, ep);
			}
			TriangulationPoint triangulationPoint2 = triangle.PointCWFrom(point);
			Orientation orientation2 = TriangulationUtil.Orient2d(eq, triangulationPoint2, ep);
			if (orientation2 == Orientation.Collinear)
			{
				throw new PointOnEdgeException("EdgeEvent - Point on constrained edge not supported yet", eq, triangulationPoint2, ep);
			}
			if (orientation == orientation2)
			{
				if (orientation == Orientation.CW)
				{
					triangle = triangle.NeighborCCWFrom(point);
				}
				else
				{
					triangle = triangle.NeighborCWFrom(point);
				}
				DTSweep.EdgeEvent(tcx, ep, eq, triangle, point);
				return;
			}
			DTSweep.FlipEdgeEvent(tcx, ep, eq, triangle, point);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000394C File Offset: 0x00001B4C
		private static void SplitEdge(TriangulationPoint ep, TriangulationPoint eq, TriangulationPoint p)
		{
			eq.Edges.First((DTSweepConstraint e) => e.Q == ep || e.P == ep).P = p;
			new DTSweepConstraint(ep, p);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003990 File Offset: 0x00001B90
		private static void FlipEdgeEvent(DTSweepContext tcx, TriangulationPoint ep, TriangulationPoint eq, DelaunayTriangle t, TriangulationPoint p)
		{
			DelaunayTriangle delaunayTriangle = t.NeighborAcrossFrom(p);
			TriangulationPoint triangulationPoint = delaunayTriangle.OppositePoint(t, p);
			if (delaunayTriangle == null)
			{
				throw new InvalidOperationException("[BUG:FIXME] FLIP failed due to missing triangle");
			}
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.PrimaryTriangle = t;
				tcx.DTDebugContext.SecondaryTriangle = delaunayTriangle;
			}
			if (TriangulationUtil.InScanArea(p, t.PointCCWFrom(p), t.PointCWFrom(p), triangulationPoint))
			{
				DTSweep.RotateTrianglePair(t, p, delaunayTriangle, triangulationPoint);
				tcx.MapTriangleToNodes(t);
				tcx.MapTriangleToNodes(delaunayTriangle);
				if (p != eq || triangulationPoint != ep)
				{
					if (tcx.IsDebugEnabled)
					{
						Console.WriteLine("[FLIP] - flipping and continuing with triangle still crossing edge");
					}
					Orientation o = TriangulationUtil.Orient2d(eq, triangulationPoint, ep);
					t = DTSweep.NextFlipTriangle(tcx, o, t, delaunayTriangle, p, triangulationPoint);
					DTSweep.FlipEdgeEvent(tcx, ep, eq, t, p);
					return;
				}
				if (eq == tcx.EdgeEvent.ConstrainedEdge.Q && ep == tcx.EdgeEvent.ConstrainedEdge.P)
				{
					if (tcx.IsDebugEnabled)
					{
						Console.WriteLine("[FLIP] - constrained edge done");
					}
					t.MarkConstrainedEdge(ep, eq);
					delaunayTriangle.MarkConstrainedEdge(ep, eq);
					DTSweep.Legalize(tcx, t);
					DTSweep.Legalize(tcx, delaunayTriangle);
					return;
				}
				if (tcx.IsDebugEnabled)
				{
					Console.WriteLine("[FLIP] - subedge done");
					return;
				}
			}
			else
			{
				TriangulationPoint p2 = DTSweep.NextFlipPoint(ep, eq, delaunayTriangle, triangulationPoint);
				DTSweep.FlipScanEdgeEvent(tcx, ep, eq, t, delaunayTriangle, p2);
				DTSweep.EdgeEvent(tcx, ep, eq, t, p);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003AE0 File Offset: 0x00001CE0
		private static TriangulationPoint NextFlipPoint(TriangulationPoint ep, TriangulationPoint eq, DelaunayTriangle ot, TriangulationPoint op)
		{
			switch (TriangulationUtil.Orient2d(eq, op, ep))
			{
			case Orientation.CW:
				return ot.PointCCWFrom(op);
			case Orientation.CCW:
				return ot.PointCWFrom(op);
			case Orientation.Collinear:
				throw new PointOnEdgeException("Point on constrained edge not supported yet", eq, op, ep);
			default:
				throw new NotImplementedException("Orientation not handled");
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003B34 File Offset: 0x00001D34
		private static DelaunayTriangle NextFlipTriangle(DTSweepContext tcx, Orientation o, DelaunayTriangle t, DelaunayTriangle ot, TriangulationPoint p, TriangulationPoint op)
		{
			int index;
			if (o == Orientation.CCW)
			{
				index = ot.EdgeIndex(p, op);
				ot.EdgeIsDelaunay[index] = true;
				DTSweep.Legalize(tcx, ot);
				ot.EdgeIsDelaunay.Clear();
				return t;
			}
			index = t.EdgeIndex(p, op);
			t.EdgeIsDelaunay[index] = true;
			DTSweep.Legalize(tcx, t);
			t.EdgeIsDelaunay.Clear();
			return ot;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003BA0 File Offset: 0x00001DA0
		private static void FlipScanEdgeEvent(DTSweepContext tcx, TriangulationPoint ep, TriangulationPoint eq, DelaunayTriangle flipTriangle, DelaunayTriangle t, TriangulationPoint p)
		{
			DelaunayTriangle delaunayTriangle = t.NeighborAcrossFrom(p);
			TriangulationPoint triangulationPoint = delaunayTriangle.OppositePoint(t, p);
			if (delaunayTriangle == null)
			{
				throw new Exception("[BUG:FIXME] FLIP failed due to missing triangle");
			}
			if (tcx.IsDebugEnabled)
			{
				Console.WriteLine("[FLIP:SCAN] - scan next point");
				tcx.DTDebugContext.PrimaryTriangle = t;
				tcx.DTDebugContext.SecondaryTriangle = delaunayTriangle;
			}
			if (TriangulationUtil.InScanArea(eq, flipTriangle.PointCCWFrom(eq), flipTriangle.PointCWFrom(eq), triangulationPoint))
			{
				DTSweep.FlipEdgeEvent(tcx, eq, triangulationPoint, delaunayTriangle, triangulationPoint);
				return;
			}
			TriangulationPoint p2 = DTSweep.NextFlipPoint(ep, eq, delaunayTriangle, triangulationPoint);
			DTSweep.FlipScanEdgeEvent(tcx, ep, eq, flipTriangle, delaunayTriangle, p2);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C34 File Offset: 0x00001E34
		private static void FillAdvancingFront(DTSweepContext tcx, AdvancingFrontNode n)
		{
			AdvancingFrontNode advancingFrontNode = n.Next;
			while (advancingFrontNode.HasNext)
			{
				double num = DTSweep.HoleAngle(advancingFrontNode);
				if (num > 1.5707963267948966 || num < -1.5707963267948966)
				{
					break;
				}
				DTSweep.Fill(tcx, advancingFrontNode);
				advancingFrontNode = advancingFrontNode.Next;
			}
			advancingFrontNode = n.Prev;
			while (advancingFrontNode.HasPrev)
			{
				double num = DTSweep.HoleAngle(advancingFrontNode);
				if (num > 1.5707963267948966 || num < -1.5707963267948966)
				{
					break;
				}
				DTSweep.Fill(tcx, advancingFrontNode);
				advancingFrontNode = advancingFrontNode.Prev;
			}
			if (n.HasNext && n.Next.HasNext)
			{
				double num = DTSweep.BasinAngle(n);
				if (num < 2.356194490192345)
				{
					DTSweep.FillBasin(tcx, n);
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003CEC File Offset: 0x00001EEC
		private static void FillBasin(DTSweepContext tcx, AdvancingFrontNode node)
		{
			if (TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == Orientation.CCW)
			{
				tcx.Basin.leftNode = node;
			}
			else
			{
				tcx.Basin.leftNode = node.Next;
			}
			tcx.Basin.bottomNode = tcx.Basin.leftNode;
			while (tcx.Basin.bottomNode.HasNext && tcx.Basin.bottomNode.Point.Y >= tcx.Basin.bottomNode.Next.Point.Y)
			{
				tcx.Basin.bottomNode = tcx.Basin.bottomNode.Next;
			}
			if (tcx.Basin.bottomNode == tcx.Basin.leftNode)
			{
				return;
			}
			tcx.Basin.rightNode = tcx.Basin.bottomNode;
			while (tcx.Basin.rightNode.HasNext && tcx.Basin.rightNode.Point.Y < tcx.Basin.rightNode.Next.Point.Y)
			{
				tcx.Basin.rightNode = tcx.Basin.rightNode.Next;
			}
			if (tcx.Basin.rightNode == tcx.Basin.bottomNode)
			{
				return;
			}
			tcx.Basin.width = tcx.Basin.rightNode.Point.X - tcx.Basin.leftNode.Point.X;
			tcx.Basin.leftHighest = (tcx.Basin.leftNode.Point.Y > tcx.Basin.rightNode.Point.Y);
			DTSweep.FillBasinReq(tcx, tcx.Basin.bottomNode);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EE0 File Offset: 0x000020E0
		private static void FillBasinReq(DTSweepContext tcx, AdvancingFrontNode node)
		{
			if (DTSweep.IsShallow(tcx, node))
			{
				return;
			}
			DTSweep.Fill(tcx, node);
			if (node.Prev == tcx.Basin.leftNode && node.Next == tcx.Basin.rightNode)
			{
				return;
			}
			if (node.Prev == tcx.Basin.leftNode)
			{
				if (TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == Orientation.CW)
				{
					return;
				}
				node = node.Next;
			}
			else if (node.Next == tcx.Basin.rightNode)
			{
				if (TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point) == Orientation.CCW)
				{
					return;
				}
				node = node.Prev;
			}
			else if (node.Prev.Point.Y < node.Next.Point.Y)
			{
				node = node.Prev;
			}
			else
			{
				node = node.Next;
			}
			DTSweep.FillBasinReq(tcx, node);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003FF0 File Offset: 0x000021F0
		private static bool IsShallow(DTSweepContext tcx, AdvancingFrontNode node)
		{
			double num;
			if (tcx.Basin.leftHighest)
			{
				num = tcx.Basin.leftNode.Point.Y - node.Point.Y;
			}
			else
			{
				num = tcx.Basin.rightNode.Point.Y - node.Point.Y;
			}
			return tcx.Basin.width > num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004064 File Offset: 0x00002264
		private static double HoleAngle(AdvancingFrontNode node)
		{
			double x = node.Point.X;
			double y = node.Point.Y;
			double num = node.Next.Point.X - x;
			double num2 = node.Next.Point.Y - y;
			double num3 = node.Prev.Point.X - x;
			double num4 = node.Prev.Point.Y - y;
			return Math.Atan2(num * num4 - num2 * num3, num * num3 + num2 * num4);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000040F0 File Offset: 0x000022F0
		private static double BasinAngle(AdvancingFrontNode node)
		{
			double x = node.Point.X - node.Next.Next.Point.X;
			return Math.Atan2(node.Point.Y - node.Next.Next.Point.Y, x);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004148 File Offset: 0x00002348
		private static void Fill(DTSweepContext tcx, AdvancingFrontNode node)
		{
			DelaunayTriangle delaunayTriangle = new DelaunayTriangle(node.Prev.Point, node.Point, node.Next.Point);
			delaunayTriangle.MarkNeighbor(node.Prev.Triangle);
			delaunayTriangle.MarkNeighbor(node.Triangle);
			tcx.Triangles.Add(delaunayTriangle);
			node.Prev.Next = node.Next;
			node.Next.Prev = node.Prev;
			tcx.RemoveNode(node);
			if (!DTSweep.Legalize(tcx, delaunayTriangle))
			{
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000041DC File Offset: 0x000023DC
		private static bool Legalize(DTSweepContext tcx, DelaunayTriangle t)
		{
			for (int i = 0; i < 3; i++)
			{
				if (!t.EdgeIsDelaunay[i])
				{
					DelaunayTriangle delaunayTriangle = t.Neighbors[i];
					if (delaunayTriangle != null)
					{
						TriangulationPoint triangulationPoint = t.Points[i];
						TriangulationPoint triangulationPoint2 = delaunayTriangle.OppositePoint(t, triangulationPoint);
						int index = delaunayTriangle.IndexOf(triangulationPoint2);
						if (delaunayTriangle.EdgeIsConstrained[index] || delaunayTriangle.EdgeIsDelaunay[index])
						{
							t.EdgeIsConstrained[i] = delaunayTriangle.EdgeIsConstrained[index];
						}
						else if (TriangulationUtil.SmartIncircle(triangulationPoint, t.PointCCWFrom(triangulationPoint), t.PointCWFrom(triangulationPoint), triangulationPoint2))
						{
							t.EdgeIsDelaunay[i] = true;
							delaunayTriangle.EdgeIsDelaunay[index] = true;
							DTSweep.RotateTrianglePair(t, triangulationPoint, delaunayTriangle, triangulationPoint2);
							if (!DTSweep.Legalize(tcx, t))
							{
								tcx.MapTriangleToNodes(t);
							}
							if (!DTSweep.Legalize(tcx, delaunayTriangle))
							{
								tcx.MapTriangleToNodes(delaunayTriangle);
							}
							t.EdgeIsDelaunay[i] = false;
							delaunayTriangle.EdgeIsDelaunay[index] = false;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000042F0 File Offset: 0x000024F0
		private static void RotateTrianglePair(DelaunayTriangle t, TriangulationPoint p, DelaunayTriangle ot, TriangulationPoint op)
		{
			DelaunayTriangle delaunayTriangle = t.NeighborCCWFrom(p);
			DelaunayTriangle delaunayTriangle2 = t.NeighborCWFrom(p);
			DelaunayTriangle delaunayTriangle3 = ot.NeighborCCWFrom(op);
			DelaunayTriangle delaunayTriangle4 = ot.NeighborCWFrom(op);
			bool constrainedEdgeCCW = t.GetConstrainedEdgeCCW(p);
			bool constrainedEdgeCW = t.GetConstrainedEdgeCW(p);
			bool constrainedEdgeCCW2 = ot.GetConstrainedEdgeCCW(op);
			bool constrainedEdgeCW2 = ot.GetConstrainedEdgeCW(op);
			bool delaunayEdgeCCW = t.GetDelaunayEdgeCCW(p);
			bool delaunayEdgeCW = t.GetDelaunayEdgeCW(p);
			bool delaunayEdgeCCW2 = ot.GetDelaunayEdgeCCW(op);
			bool delaunayEdgeCW2 = ot.GetDelaunayEdgeCW(op);
			t.Legalize(p, op);
			ot.Legalize(op, p);
			ot.SetDelaunayEdgeCCW(p, delaunayEdgeCCW);
			t.SetDelaunayEdgeCW(p, delaunayEdgeCW);
			t.SetDelaunayEdgeCCW(op, delaunayEdgeCCW2);
			ot.SetDelaunayEdgeCW(op, delaunayEdgeCW2);
			ot.SetConstrainedEdgeCCW(p, constrainedEdgeCCW);
			t.SetConstrainedEdgeCW(p, constrainedEdgeCW);
			t.SetConstrainedEdgeCCW(op, constrainedEdgeCCW2);
			ot.SetConstrainedEdgeCW(op, constrainedEdgeCW2);
			t.Neighbors.Clear();
			ot.Neighbors.Clear();
			if (delaunayTriangle != null)
			{
				ot.MarkNeighbor(delaunayTriangle);
			}
			if (delaunayTriangle2 != null)
			{
				t.MarkNeighbor(delaunayTriangle2);
			}
			if (delaunayTriangle3 != null)
			{
				t.MarkNeighbor(delaunayTriangle3);
			}
			if (delaunayTriangle4 != null)
			{
				ot.MarkNeighbor(delaunayTriangle4);
			}
			t.MarkNeighbor(ot);
		}

		// Token: 0x04000017 RID: 23
		private const double PI_div2 = 1.5707963267948966;

		// Token: 0x04000018 RID: 24
		private const double PI_3div4 = 2.356194490192345;

		// Token: 0x02000020 RID: 32
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x060000F3 RID: 243 RVA: 0x00005451 File Offset: 0x00003651
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x00005459 File Offset: 0x00003659
			internal bool <SplitEdge>b__0(DTSweepConstraint e)
			{
				return e.Q == this.ep || e.P == this.ep;
			}

			// Token: 0x04000056 RID: 86
			public TriangulationPoint ep;
		}
	}
}
