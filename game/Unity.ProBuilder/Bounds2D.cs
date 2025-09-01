using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000009 RID: 9
	internal sealed class Bounds2D
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F3A File Offset: 0x0000113A
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002F44 File Offset: 0x00001144
		public Vector2 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
				this.m_Extents.x = this.m_Size.x * 0.5f;
				this.m_Extents.y = this.m_Size.y * 0.5f;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F90 File Offset: 0x00001190
		public Vector2 extents
		{
			get
			{
				return this.m_Extents;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F98 File Offset: 0x00001198
		public Vector2[] corners
		{
			get
			{
				return new Vector2[]
				{
					new Vector2(this.center.x - this.extents.x, this.center.y + this.extents.y),
					new Vector2(this.center.x + this.extents.x, this.center.y + this.extents.y),
					new Vector2(this.center.x - this.extents.x, this.center.y - this.extents.y),
					new Vector2(this.center.x + this.extents.x, this.center.y - this.extents.y)
				};
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003093 File Offset: 0x00001293
		public Bounds2D()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030BC File Offset: 0x000012BC
		public Bounds2D(Vector2 center, Vector2 size)
		{
			this.center = center;
			this.size = size;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030F3 File Offset: 0x000012F3
		public Bounds2D(IList<Vector2> points)
		{
			this.SetWithPoints(points);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003123 File Offset: 0x00001323
		public Bounds2D(IList<Vector2> points, IList<int> indexes)
		{
			this.SetWithPoints(points, indexes);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003154 File Offset: 0x00001354
		internal Bounds2D(Vector3[] points, Edge[] edges)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (points.Length != 0 && edges.Length != 0)
			{
				num = points[edges[0].a].x;
				num3 = points[edges[0].a].y;
				num2 = num;
				num4 = num3;
				for (int i = 0; i < edges.Length; i++)
				{
					num = Mathf.Min(num, points[edges[i].a].x);
					num = Mathf.Min(num, points[edges[i].b].x);
					num3 = Mathf.Min(num3, points[edges[i].a].y);
					num3 = Mathf.Min(num3, points[edges[i].b].y);
					num2 = Mathf.Max(num2, points[edges[i].a].x);
					num2 = Mathf.Max(num2, points[edges[i].b].x);
					num4 = Mathf.Max(num4, points[edges[i].a].y);
					num4 = Mathf.Max(num4, points[edges[i].b].y);
				}
			}
			this.center = new Vector2((num + num2) / 2f, (num3 + num4) / 2f);
			this.size = new Vector3(num2 - num, num4 - num3);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003328 File Offset: 0x00001528
		public Bounds2D(Vector2[] points, int length)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (points.Length != 0)
			{
				num = points[0].x;
				num3 = points[0].y;
				num2 = num;
				num4 = num3;
				for (int i = 1; i < length; i++)
				{
					num = Mathf.Min(num, points[i].x);
					num3 = Mathf.Min(num3, points[i].y);
					num2 = Mathf.Max(num2, points[i].x);
					num4 = Mathf.Max(num4, points[i].y);
				}
			}
			this.center = new Vector2((num + num2) / 2f, (num3 + num4) / 2f);
			this.size = new Vector3(num2 - num, num4 - num3);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000342C File Offset: 0x0000162C
		public bool ContainsPoint(Vector2 point)
		{
			return point.x <= this.center.x + this.extents.x && point.x >= this.center.x - this.extents.x && point.y <= this.center.y + this.extents.y && point.y >= this.center.y - this.extents.y;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034BC File Offset: 0x000016BC
		public bool IntersectsLineSegment(Vector2 lineStart, Vector2 lineEnd)
		{
			if (this.ContainsPoint(lineStart) || this.ContainsPoint(lineEnd))
			{
				return true;
			}
			Vector2[] corners = this.corners;
			return Math.GetLineSegmentIntersect(corners[0], corners[1], lineStart, lineEnd) || Math.GetLineSegmentIntersect(corners[1], corners[3], lineStart, lineEnd) || Math.GetLineSegmentIntersect(corners[3], corners[2], lineStart, lineEnd) || Math.GetLineSegmentIntersect(corners[2], corners[0], lineStart, lineEnd);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003540 File Offset: 0x00001740
		public bool Intersects(Bounds2D bounds)
		{
			Vector2 vector = this.center - bounds.center;
			Vector2 vector2 = this.size + bounds.size;
			return Mathf.Abs(vector.x) * 2f < vector2.x && Mathf.Abs(vector.y) * 2f < vector2.y;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035A8 File Offset: 0x000017A8
		public bool Intersects(Rect rect)
		{
			Vector2 vector = this.center - rect.center;
			Vector2 vector2 = this.size + rect.size;
			return Mathf.Abs(vector.x) * 2f < vector2.x && Mathf.Abs(vector.y) * 2f < vector2.y;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003610 File Offset: 0x00001810
		public void SetWithPoints(IList<Vector2> points)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int count = points.Count;
			if (count > 0)
			{
				num = points[0].x;
				num3 = points[0].y;
				num2 = num;
				num4 = num3;
				for (int i = 1; i < count; i++)
				{
					float x = points[i].x;
					float y = points[i].y;
					if (x < num)
					{
						num = x;
					}
					if (x > num2)
					{
						num2 = x;
					}
					if (y < num3)
					{
						num3 = y;
					}
					if (y > num4)
					{
						num4 = y;
					}
				}
			}
			this.center.x = (num + num2) / 2f;
			this.center.y = (num3 + num4) / 2f;
			this.m_Size.x = num2 - num;
			this.m_Size.y = num4 - num3;
			this.m_Extents.x = this.m_Size.x * 0.5f;
			this.m_Extents.y = this.m_Size.y * 0.5f;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000372C File Offset: 0x0000192C
		public void SetWithPoints(IList<Vector2> points, IList<int> indexes)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (points.Count > 0 && indexes.Count > 0)
			{
				num = points[indexes[0]].x;
				num3 = points[indexes[0]].y;
				num2 = num;
				num4 = num3;
				for (int i = 1; i < indexes.Count; i++)
				{
					float x = points[indexes[i]].x;
					float y = points[indexes[i]].y;
					if (x < num)
					{
						num = x;
					}
					if (x > num2)
					{
						num2 = x;
					}
					if (y < num3)
					{
						num3 = y;
					}
					if (y > num4)
					{
						num4 = y;
					}
				}
			}
			this.center.x = (num + num2) / 2f;
			this.center.y = (num3 + num4) / 2f;
			this.m_Size.x = num2 - num;
			this.m_Size.y = num4 - num3;
			this.m_Extents.x = this.m_Size.x * 0.5f;
			this.m_Extents.y = this.m_Size.y * 0.5f;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003870 File Offset: 0x00001A70
		public static Vector2 Center(IList<Vector2> points)
		{
			int count = points.Count;
			float num = points[0].x;
			float num2 = points[0].y;
			float num3 = num;
			float num4 = num2;
			for (int i = 1; i < count; i++)
			{
				float x = points[i].x;
				float y = points[i].y;
				if (x < num)
				{
					num = x;
				}
				if (x > num3)
				{
					num3 = x;
				}
				if (y < num2)
				{
					num2 = y;
				}
				if (y > num4)
				{
					num4 = y;
				}
			}
			return new Vector2((num + num3) / 2f, (num2 + num4) / 2f);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003924 File Offset: 0x00001B24
		public static Vector2 Center(IList<Vector2> points, IList<int> indexes)
		{
			int count = indexes.Count;
			float num = points[indexes[0]].x;
			float num2 = points[indexes[0]].y;
			float num3 = num;
			float num4 = num2;
			for (int i = 1; i < count; i++)
			{
				float x = points[indexes[i]].x;
				float y = points[indexes[i]].y;
				if (x < num)
				{
					num = x;
				}
				if (x > num3)
				{
					num3 = x;
				}
				if (y < num2)
				{
					num2 = y;
				}
				if (y > num4)
				{
					num4 = y;
				}
			}
			return new Vector2((num + num3) / 2f, (num2 + num4) / 2f);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000039F0 File Offset: 0x00001BF0
		public static Vector2 Size(IList<Vector2> points, IList<int> indexes)
		{
			int count = indexes.Count;
			float num = points[indexes[0]].x;
			float num2 = points[indexes[0]].y;
			float num3 = num;
			float num4 = num2;
			for (int i = 1; i < count; i++)
			{
				float x = points[indexes[i]].x;
				float y = points[indexes[i]].y;
				if (x < num)
				{
					num = x;
				}
				if (x > num3)
				{
					num3 = x;
				}
				if (y < num2)
				{
					num2 = y;
				}
				if (y > num4)
				{
					num4 = y;
				}
			}
			return new Vector2(num3 - num, num4 - num2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003AB0 File Offset: 0x00001CB0
		internal static Vector2 Center(IList<Vector4> points, IEnumerable<int> indexes)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (indexes.Any<int>())
			{
				int index = indexes.First<int>();
				num = points[index].x;
				num3 = points[index].y;
				num2 = num;
				num4 = num3;
				foreach (int index2 in indexes)
				{
					float x = points[index2].x;
					float y = points[index2].y;
					if (x < num)
					{
						num = x;
					}
					if (x > num2)
					{
						num2 = x;
					}
					if (y < num3)
					{
						num3 = y;
					}
					if (y > num4)
					{
						num4 = y;
					}
				}
			}
			return new Vector2((num + num2) / 2f, (num3 + num4) / 2f);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003B98 File Offset: 0x00001D98
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "[cen: ";
			int num = 1;
			Vector2 vector = this.center;
			array[num] = vector.ToString();
			array[2] = " size: ";
			array[3] = this.size.ToString();
			array[4] = "]";
			return string.Concat(array);
		}

		// Token: 0x0400001F RID: 31
		public Vector2 center = Vector2.zero;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private Vector2 m_Size = Vector2.zero;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private Vector2 m_Extents = Vector2.zero;
	}
}
