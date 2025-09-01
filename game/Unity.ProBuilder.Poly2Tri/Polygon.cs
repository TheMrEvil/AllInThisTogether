using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000003 RID: 3
	internal class Polygon : Triangulatable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public Polygon(IList<PolygonPoint> points)
		{
			if (points.Count < 3)
			{
				throw new ArgumentException("List has fewer than 3 points", "points");
			}
			if (points[0].Equals(points[points.Count - 1]))
			{
				points.RemoveAt(points.Count - 1);
			}
			this._points.AddRange(points.Cast<TriangulationPoint>());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002182 File Offset: 0x00000382
		public Polygon(IEnumerable<PolygonPoint> points) : this((points as IList<PolygonPoint>) ?? points.ToArray<PolygonPoint>())
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public Polygon(params PolygonPoint[] points) : this(points)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A3 File Offset: 0x000003A3
		public TriangulationMode TriangulationMode
		{
			get
			{
				return TriangulationMode.Polygon;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A6 File Offset: 0x000003A6
		public void AddSteinerPoint(TriangulationPoint point)
		{
			if (this._steinerPoints == null)
			{
				this._steinerPoints = new List<TriangulationPoint>();
			}
			this._steinerPoints.Add(point);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C7 File Offset: 0x000003C7
		public void AddSteinerPoints(List<TriangulationPoint> points)
		{
			if (this._steinerPoints == null)
			{
				this._steinerPoints = new List<TriangulationPoint>();
			}
			this._steinerPoints.AddRange(points);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E8 File Offset: 0x000003E8
		public void ClearSteinerPoints()
		{
			if (this._steinerPoints != null)
			{
				this._steinerPoints.Clear();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FD File Offset: 0x000003FD
		public void AddHole(Polygon poly)
		{
			if (this._holes == null)
			{
				this._holes = new List<Polygon>();
			}
			this._holes.Add(poly);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002220 File Offset: 0x00000420
		public void InsertPointAfter(PolygonPoint point, PolygonPoint newPoint)
		{
			int num = this._points.IndexOf(point);
			if (num == -1)
			{
				throw new ArgumentException("Tried to insert a point into a Polygon after a point not belonging to the Polygon", "point");
			}
			newPoint.Next = point.Next;
			newPoint.Previous = point;
			point.Next.Previous = newPoint;
			point.Next = newPoint;
			this._points.Insert(num + 1, newPoint);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002284 File Offset: 0x00000484
		public void AddPoints(IEnumerable<PolygonPoint> list)
		{
			foreach (PolygonPoint polygonPoint in list)
			{
				polygonPoint.Previous = this._last;
				if (this._last != null)
				{
					polygonPoint.Next = this._last.Next;
					this._last.Next = polygonPoint;
				}
				this._last = polygonPoint;
				this._points.Add(polygonPoint);
			}
			PolygonPoint polygonPoint2 = (PolygonPoint)this._points[0];
			this._last.Next = polygonPoint2;
			polygonPoint2.Previous = this._last;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002334 File Offset: 0x00000534
		public void AddPoint(PolygonPoint p)
		{
			p.Previous = this._last;
			p.Next = this._last.Next;
			this._last.Next = p;
			this._points.Add(p);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
		public void RemovePoint(PolygonPoint p)
		{
			PolygonPoint next = p.Next;
			PolygonPoint previous = p.Previous;
			previous.Next = next;
			next.Previous = previous;
			this._points.Remove(p);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023A2 File Offset: 0x000005A2
		public IList<TriangulationPoint> Points
		{
			get
			{
				return this._points;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023AA File Offset: 0x000005AA
		public IList<DelaunayTriangle> Triangles
		{
			get
			{
				return this._triangles;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023B2 File Offset: 0x000005B2
		public IList<Polygon> Holes
		{
			get
			{
				return this._holes;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023BA File Offset: 0x000005BA
		public void AddTriangle(DelaunayTriangle t)
		{
			this._triangles.Add(t);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C8 File Offset: 0x000005C8
		public void AddTriangles(IEnumerable<DelaunayTriangle> list)
		{
			this._triangles.AddRange(list);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023D6 File Offset: 0x000005D6
		public void ClearTriangles()
		{
			if (this._triangles != null)
			{
				this._triangles.Clear();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023EC File Offset: 0x000005EC
		public void Prepare(TriangulationContext tcx)
		{
			if (this._triangles == null)
			{
				this._triangles = new List<DelaunayTriangle>(this._points.Count);
			}
			else
			{
				this._triangles.Clear();
			}
			for (int i = 0; i < this._points.Count - 1; i++)
			{
				tcx.NewConstraint(this._points[i], this._points[i + 1]);
			}
			tcx.NewConstraint(this._points[0], this._points[this._points.Count - 1]);
			tcx.Points.AddRange(this._points);
			if (this._holes != null)
			{
				foreach (Polygon polygon in this._holes)
				{
					for (int j = 0; j < polygon._points.Count - 1; j++)
					{
						tcx.NewConstraint(polygon._points[j], polygon._points[j + 1]);
					}
					tcx.NewConstraint(polygon._points[0], polygon._points[polygon._points.Count - 1]);
					tcx.Points.AddRange(polygon._points);
				}
			}
			if (this._steinerPoints != null)
			{
				tcx.Points.AddRange(this._steinerPoints);
			}
		}

		// Token: 0x04000002 RID: 2
		protected List<TriangulationPoint> _points = new List<TriangulationPoint>();

		// Token: 0x04000003 RID: 3
		protected List<TriangulationPoint> _steinerPoints;

		// Token: 0x04000004 RID: 4
		protected List<Polygon> _holes;

		// Token: 0x04000005 RID: 5
		protected List<DelaunayTriangle> _triangles;

		// Token: 0x04000006 RID: 6
		protected PolygonPoint _last;
	}
}
