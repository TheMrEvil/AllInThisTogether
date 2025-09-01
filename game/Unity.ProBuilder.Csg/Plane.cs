using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000004 RID: 4
	internal sealed class Plane
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000281D File Offset: 0x00000A1D
		public Plane()
		{
			this.normal = Vector3.zero;
			this.w = 0f;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000283B File Offset: 0x00000A3B
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.normal = Vector3.Cross(b - a, c - a);
			this.w = Vector3.Dot(this.normal, a);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000286E File Offset: 0x00000A6E
		public override string ToString()
		{
			return string.Format("{0} {1}", this.normal, this.w);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002890 File Offset: 0x00000A90
		public bool Valid()
		{
			return this.normal.magnitude > 0f;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void Flip()
		{
			this.normal *= -1f;
			this.w *= -1f;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void SplitPolygon(Polygon polygon, List<Polygon> coplanarFront, List<Polygon> coplanarBack, List<Polygon> front, List<Polygon> back)
		{
			Plane.EPolygonType epolygonType = Plane.EPolygonType.Coplanar;
			List<Plane.EPolygonType> list = new List<Plane.EPolygonType>();
			for (int i = 0; i < polygon.vertices.Count; i++)
			{
				float num = Vector3.Dot(this.normal, polygon.vertices[i].position) - this.w;
				Plane.EPolygonType epolygonType2 = (num < -CSG.epsilon) ? Plane.EPolygonType.Back : ((num > CSG.epsilon) ? Plane.EPolygonType.Front : Plane.EPolygonType.Coplanar);
				epolygonType |= epolygonType2;
				list.Add(epolygonType2);
			}
			switch (epolygonType)
			{
			case Plane.EPolygonType.Coplanar:
				if (Vector3.Dot(this.normal, polygon.plane.normal) > 0f)
				{
					coplanarFront.Add(polygon);
					return;
				}
				coplanarBack.Add(polygon);
				return;
			case Plane.EPolygonType.Front:
				front.Add(polygon);
				return;
			case Plane.EPolygonType.Back:
				back.Add(polygon);
				return;
			case Plane.EPolygonType.Spanning:
			{
				List<Vertex> list2 = new List<Vertex>();
				List<Vertex> list3 = new List<Vertex>();
				for (int j = 0; j < polygon.vertices.Count; j++)
				{
					int index = (j + 1) % polygon.vertices.Count;
					Plane.EPolygonType epolygonType3 = list[j];
					Plane.EPolygonType epolygonType4 = list[index];
					Vertex vertex = polygon.vertices[j];
					Vertex y = polygon.vertices[index];
					if (epolygonType3 != Plane.EPolygonType.Back)
					{
						list2.Add(vertex);
					}
					if (epolygonType3 != Plane.EPolygonType.Front)
					{
						list3.Add(vertex);
					}
					if ((epolygonType3 | epolygonType4) == Plane.EPolygonType.Spanning)
					{
						float weight = (this.w - Vector3.Dot(this.normal, vertex.position)) / Vector3.Dot(this.normal, y.position - vertex.position);
						Vertex item = vertex.Mix(y, weight);
						list2.Add(item);
						list3.Add(item);
					}
				}
				if (list2.Count >= 3)
				{
					if (list2.SequenceEqual(polygon.vertices))
					{
						front.Add(polygon);
					}
					else
					{
						Polygon polygon2 = new Polygon(list2, polygon.material);
						if (polygon2.plane.Valid())
						{
							front.Add(polygon2);
						}
					}
				}
				if (list3.Count >= 3)
				{
					if (list3.SequenceEqual(polygon.vertices))
					{
						back.Add(polygon);
						return;
					}
					Polygon polygon3 = new Polygon(list3, polygon.material);
					if (polygon3.plane.Valid())
					{
						back.Add(polygon3);
					}
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04000008 RID: 8
		public Vector3 normal;

		// Token: 0x04000009 RID: 9
		public float w;

		// Token: 0x0200000B RID: 11
		[Flags]
		private enum EPolygonType
		{
			// Token: 0x04000025 RID: 37
			Coplanar = 0,
			// Token: 0x04000026 RID: 38
			Front = 1,
			// Token: 0x04000027 RID: 39
			Back = 2,
			// Token: 0x04000028 RID: 40
			Spanning = 3
		}
	}
}
