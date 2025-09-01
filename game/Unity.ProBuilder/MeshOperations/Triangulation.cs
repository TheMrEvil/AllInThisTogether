using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.ProBuilder.Poly2Tri;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000089 RID: 137
	internal static class Triangulation
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00033E1E File Offset: 0x0003201E
		private static TriangulationContext triangulationContext
		{
			get
			{
				if (Triangulation.s_TriangulationContext == null)
				{
					Triangulation.s_TriangulationContext = new DTSweepContext();
				}
				return Triangulation.s_TriangulationContext;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00033E38 File Offset: 0x00032038
		public static bool SortAndTriangulate(IList<Vector2> points, out List<int> indexes, bool convex = false)
		{
			IList<Vector2> list = Projection.Sort(points, SortMethod.CounterClockwise);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(i, points.IndexOf(list[i]));
			}
			if (!Triangulation.Triangulate(list, out indexes, convex))
			{
				return false;
			}
			for (int j = 0; j < indexes.Count; j++)
			{
				indexes[j] = dictionary[indexes[j]];
			}
			return true;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00033EB0 File Offset: 0x000320B0
		public static bool TriangulateVertices(IList<Vertex> vertices, out List<int> triangles, bool unordered = true, bool convex = false)
		{
			Vector3[] array = new Vector3[vertices.Count];
			for (int i = 0; i < vertices.Count; i++)
			{
				array[i] = vertices[i].position;
			}
			return Triangulation.TriangulateVertices(array, out triangles, unordered, convex);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00033EF8 File Offset: 0x000320F8
		public static bool TriangulateVertices(Vector3[] vertices, out List<int> triangles, Vector3[][] holes = null)
		{
			triangles = null;
			if (((vertices == null) ? 0 : vertices.Length) < 3)
			{
				return false;
			}
			Vector3 normal = Projection.FindBestPlane(vertices, null).normal;
			Vector2[] points = Projection.PlanarProject(vertices, null, normal);
			Vector2[][] array = null;
			if (holes != null)
			{
				array = new Vector2[holes.Length][];
				for (int i = 0; i < holes.Length; i++)
				{
					if (holes[i].Length < 3)
					{
						return false;
					}
					array[i] = Projection.PlanarProject(holes[i], null, normal);
				}
			}
			return Triangulation.Triangulate(points, array, out triangles);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00033F74 File Offset: 0x00032174
		public static bool TriangulateVertices(Vector3[] vertices, out List<int> triangles, bool unordered = true, bool convex = false)
		{
			triangles = null;
			int num = (vertices == null) ? 0 : vertices.Length;
			if (num < 3)
			{
				return false;
			}
			if (num == 3)
			{
				triangles = new List<int>
				{
					0,
					1,
					2
				};
				return true;
			}
			Vector2[] points = Projection.PlanarProject(vertices, null);
			if (unordered)
			{
				return Triangulation.SortAndTriangulate(points, out triangles, convex);
			}
			return Triangulation.Triangulate(points, out triangles, convex);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00033FD4 File Offset: 0x000321D4
		public static bool Triangulate(IList<Vector2> points, out List<int> indexes, bool convex = false)
		{
			indexes = new List<int>();
			int index = 0;
			Triangulatable triangulatable2;
			if (!convex)
			{
				Triangulatable triangulatable = new Polygon(points.Select(delegate(Vector2 x)
				{
					double x2 = (double)x.x;
					double y = (double)x.y;
					int index = index;
					index++;
					return new PolygonPoint(x2, y, index);
				}));
				triangulatable2 = triangulatable;
			}
			else
			{
				Triangulatable triangulatable = new PointSet(points.Select(delegate(Vector2 x)
				{
					double x2 = (double)x.x;
					double y = (double)x.y;
					int index = index;
					index++;
					return new TriangulationPoint(x2, y, index);
				}).ToList<TriangulationPoint>());
				triangulatable2 = triangulatable;
			}
			Triangulatable triangulatable3 = triangulatable2;
			try
			{
				Triangulation.triangulationContext.Clear();
				Triangulation.triangulationContext.PrepareTriangulation(triangulatable3);
				DTSweep.Triangulate((DTSweepContext)Triangulation.triangulationContext);
			}
			catch (Exception ex)
			{
				Log.Info("Triangulation failed: " + ex.ToString());
				return false;
			}
			foreach (DelaunayTriangle delaunayTriangle in triangulatable3.Triangles)
			{
				if (delaunayTriangle.Points[0].Index < 0 || delaunayTriangle.Points[1].Index < 0 || delaunayTriangle.Points[2].Index < 0)
				{
					Log.Info("Triangulation failed: Additional vertices were inserted.");
					return false;
				}
				indexes.Add(delaunayTriangle.Points[0].Index);
				indexes.Add(delaunayTriangle.Points[1].Index);
				indexes.Add(delaunayTriangle.Points[2].Index);
			}
			WindingOrder windingOrder = SurfaceTopology.GetWindingOrder(points);
			if (SurfaceTopology.GetWindingOrder(new Vector2[]
			{
				points[indexes[0]],
				points[indexes[1]],
				points[indexes[2]]
			}) != windingOrder)
			{
				indexes.Reverse();
			}
			return true;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000341C4 File Offset: 0x000323C4
		public static bool Triangulate(IList<Vector2> points, IList<IList<Vector2>> holes, out List<int> indexes)
		{
			indexes = new List<int>();
			int index = 0;
			List<Vector2> list = new List<Vector2>(points);
			Polygon polygon = new Polygon(points.Select(delegate(Vector2 x)
			{
				double x2 = (double)x.x;
				double y = (double)x.y;
				int index = index;
				index++;
				return new PolygonPoint(x2, y, index);
			}));
			if (holes != null)
			{
				Func<Vector2, PolygonPoint> <>9__1;
				for (int i = 0; i < holes.Count; i++)
				{
					list.AddRange(holes[i]);
					IEnumerable<Vector2> source = holes[i];
					Func<Vector2, PolygonPoint> selector;
					if ((selector = <>9__1) == null)
					{
						selector = (<>9__1 = delegate(Vector2 x)
						{
							double x2 = (double)x.x;
							double y = (double)x.y;
							int index = index;
							index++;
							return new PolygonPoint(x2, y, index);
						});
					}
					Polygon poly = new Polygon(source.Select(selector));
					polygon.AddHole(poly);
				}
			}
			try
			{
				Triangulation.triangulationContext.Clear();
				Triangulation.triangulationContext.PrepareTriangulation(polygon);
				DTSweep.Triangulate((DTSweepContext)Triangulation.triangulationContext);
			}
			catch (Exception ex)
			{
				Log.Info("Triangulation failed: " + ex.ToString());
				return false;
			}
			foreach (DelaunayTriangle delaunayTriangle in polygon.Triangles)
			{
				if (delaunayTriangle.Points[0].Index < 0 || delaunayTriangle.Points[1].Index < 0 || delaunayTriangle.Points[2].Index < 0)
				{
					Log.Info("Triangulation failed: Additional vertices were inserted.");
					return false;
				}
				indexes.Add(delaunayTriangle.Points[0].Index);
				indexes.Add(delaunayTriangle.Points[1].Index);
				indexes.Add(delaunayTriangle.Points[2].Index);
			}
			WindingOrder windingOrder = SurfaceTopology.GetWindingOrder(points);
			if (SurfaceTopology.GetWindingOrder(new Vector2[]
			{
				list[indexes[0]],
				list[indexes[1]],
				list[indexes[2]]
			}) != windingOrder)
			{
				indexes.Reverse();
			}
			return true;
		}

		// Token: 0x0400026F RID: 623
		private static TriangulationContext s_TriangulationContext;

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060005D9 RID: 1497 RVA: 0x000365D7 File Offset: 0x000347D7
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x000365E0 File Offset: 0x000347E0
			internal TriangulationPoint <Triangulate>b__0(Vector2 x)
			{
				double x2 = (double)x.x;
				double y = (double)x.y;
				int num = this.index;
				this.index = num + 1;
				return new TriangulationPoint(x2, y, num);
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x00036614 File Offset: 0x00034814
			internal PolygonPoint <Triangulate>b__1(Vector2 x)
			{
				double x2 = (double)x.x;
				double y = (double)x.y;
				int num = this.index;
				this.index = num + 1;
				return new PolygonPoint(x2, y, num);
			}

			// Token: 0x04000324 RID: 804
			public int index;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x060005DC RID: 1500 RVA: 0x00036645 File Offset: 0x00034845
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x00036650 File Offset: 0x00034850
			internal PolygonPoint <Triangulate>b__0(Vector2 x)
			{
				double x2 = (double)x.x;
				double y = (double)x.y;
				int num = this.index;
				this.index = num + 1;
				return new PolygonPoint(x2, y, num);
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x00036684 File Offset: 0x00034884
			internal PolygonPoint <Triangulate>b__1(Vector2 x)
			{
				double x2 = (double)x.x;
				double y = (double)x.y;
				int num = this.index;
				this.index = num + 1;
				return new PolygonPoint(x2, y, num);
			}

			// Token: 0x04000325 RID: 805
			public int index;

			// Token: 0x04000326 RID: 806
			public Func<Vector2, PolygonPoint> <>9__1;
		}
	}
}
