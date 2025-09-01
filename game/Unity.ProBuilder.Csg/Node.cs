using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000003 RID: 3
	internal sealed class Node
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000023BA File Offset: 0x000005BA
		public Node()
		{
			this.front = null;
			this.back = null;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023D0 File Offset: 0x000005D0
		public Node(List<Polygon> list)
		{
			this.Build(list);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023DF File Offset: 0x000005DF
		public Node(List<Polygon> list, Plane plane, Node front, Node back)
		{
			this.polygons = list;
			this.plane = plane;
			this.front = front;
			this.back = back;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002404 File Offset: 0x00000604
		public Node Clone()
		{
			return new Node(this.polygons, this.plane, this.front, this.back);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002423 File Offset: 0x00000623
		public void ClipTo(Node other)
		{
			this.polygons = other.ClipPolygons(this.polygons);
			if (this.front != null)
			{
				this.front.ClipTo(other);
			}
			if (this.back != null)
			{
				this.back.ClipTo(other);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002460 File Offset: 0x00000660
		public void Invert()
		{
			for (int i = 0; i < this.polygons.Count; i++)
			{
				this.polygons[i].Flip();
			}
			this.plane.Flip();
			if (this.front != null)
			{
				this.front.Invert();
			}
			if (this.back != null)
			{
				this.back.Invert();
			}
			Node node = this.front;
			this.front = this.back;
			this.back = node;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E0 File Offset: 0x000006E0
		public void Build(List<Polygon> list)
		{
			if (list.Count < 1)
			{
				return;
			}
			bool flag = this.plane == null || !this.plane.Valid();
			if (flag)
			{
				this.plane = new Plane();
				this.plane.normal = list[0].plane.normal;
				this.plane.w = list[0].plane.w;
			}
			if (this.polygons == null)
			{
				this.polygons = new List<Polygon>();
			}
			List<Polygon> list2 = new List<Polygon>();
			List<Polygon> list3 = new List<Polygon>();
			for (int i = 0; i < list.Count; i++)
			{
				this.plane.SplitPolygon(list[i], this.polygons, this.polygons, list2, list3);
			}
			if (list2.Count > 0)
			{
				if (flag && list.SequenceEqual(list2))
				{
					this.polygons.AddRange(list2);
				}
				else
				{
					Node node;
					if ((node = this.front) == null)
					{
						node = (this.front = new Node());
					}
					node.Build(list2);
				}
			}
			if (list3.Count > 0)
			{
				if (flag && list.SequenceEqual(list3))
				{
					this.polygons.AddRange(list3);
					return;
				}
				Node node2;
				if ((node2 = this.back) == null)
				{
					node2 = (this.back = new Node());
				}
				node2.Build(list3);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000262C File Offset: 0x0000082C
		public List<Polygon> ClipPolygons(List<Polygon> list)
		{
			if (!this.plane.Valid())
			{
				return list;
			}
			List<Polygon> list2 = new List<Polygon>();
			List<Polygon> list3 = new List<Polygon>();
			for (int i = 0; i < list.Count; i++)
			{
				this.plane.SplitPolygon(list[i], list2, list3, list2, list3);
			}
			if (this.front != null)
			{
				list2 = this.front.ClipPolygons(list2);
			}
			if (this.back != null)
			{
				list3 = this.back.ClipPolygons(list3);
			}
			else
			{
				list3.Clear();
			}
			list2.AddRange(list3);
			return list2;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026B8 File Offset: 0x000008B8
		public List<Polygon> AllPolygons()
		{
			List<Polygon> list = this.polygons;
			List<Polygon> collection = new List<Polygon>();
			List<Polygon> collection2 = new List<Polygon>();
			if (this.front != null)
			{
				collection = this.front.AllPolygons();
			}
			if (this.back != null)
			{
				collection2 = this.back.AllPolygons();
			}
			list.AddRange(collection);
			list.AddRange(collection2);
			return list;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002710 File Offset: 0x00000910
		public static Node Union(Node a1, Node b1)
		{
			Node node = a1.Clone();
			Node node2 = b1.Clone();
			node.ClipTo(node2);
			node2.ClipTo(node);
			node2.Invert();
			node2.ClipTo(node);
			node2.Invert();
			node.Build(node2.AllPolygons());
			return new Node(node.AllPolygons());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002764 File Offset: 0x00000964
		public static Node Subtract(Node a1, Node b1)
		{
			Node node = a1.Clone();
			Node node2 = b1.Clone();
			node.Invert();
			node.ClipTo(node2);
			node2.ClipTo(node);
			node2.Invert();
			node2.ClipTo(node);
			node2.Invert();
			node.Build(node2.AllPolygons());
			node.Invert();
			return new Node(node.AllPolygons());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027C4 File Offset: 0x000009C4
		public static Node Intersect(Node a1, Node b1)
		{
			Node node = a1.Clone();
			Node node2 = b1.Clone();
			node.Invert();
			node2.ClipTo(node);
			node2.Invert();
			node.ClipTo(node2);
			node2.ClipTo(node);
			node.Build(node2.AllPolygons());
			node.Invert();
			return new Node(node.AllPolygons());
		}

		// Token: 0x04000004 RID: 4
		public List<Polygon> polygons;

		// Token: 0x04000005 RID: 5
		public Node front;

		// Token: 0x04000006 RID: 6
		public Node back;

		// Token: 0x04000007 RID: 7
		public Plane plane;
	}
}
