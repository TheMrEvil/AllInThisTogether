using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000002 RID: 2
	internal static class P2T
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void Triangulate(PolygonSet ps)
		{
			TriangulationContext triangulationContext = P2T.CreateContext(P2T._defaultAlgorithm);
			foreach (Polygon t in ps.Polygons)
			{
				triangulationContext.PrepareTriangulation(t);
				P2T.Triangulate(triangulationContext);
				triangulationContext.Clear();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B4 File Offset: 0x000002B4
		public static void Triangulate(Polygon p)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, p);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C1 File Offset: 0x000002C1
		public static void Triangulate(ConstrainedPointSet cps)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, cps);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CE File Offset: 0x000002CE
		public static void Triangulate(PointSet ps)
		{
			P2T.Triangulate(P2T._defaultAlgorithm, ps);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DB File Offset: 0x000002DB
		public static TriangulationContext CreateContext(TriangulationAlgorithm algorithm)
		{
			return new DTSweepContext();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
		public static void Triangulate(TriangulationAlgorithm algorithm, Triangulatable t)
		{
			TriangulationContext triangulationContext = P2T.CreateContext(algorithm);
			triangulationContext.PrepareTriangulation(t);
			P2T.Triangulate(triangulationContext);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public static void Triangulate(TriangulationContext tcx)
		{
			TriangulationAlgorithm algorithm = tcx.Algorithm;
			DTSweep.Triangulate((DTSweepContext)tcx);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		public static void Warmup()
		{
		}

		// Token: 0x04000001 RID: 1
		private static TriangulationAlgorithm _defaultAlgorithm;
	}
}
