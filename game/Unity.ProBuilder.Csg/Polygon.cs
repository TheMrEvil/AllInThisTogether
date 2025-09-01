using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000005 RID: 5
	internal sealed class Polygon
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002B20 File Offset: 0x00000D20
		public Polygon(List<Vertex> list, Material mat)
		{
			this.vertices = list;
			this.plane = new Plane(list[0].position, list[1].position, list[2].position);
			this.material = mat;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B7C File Offset: 0x00000D7C
		public void Flip()
		{
			this.vertices.Reverse();
			for (int i = 0; i < this.vertices.Count; i++)
			{
				this.vertices[i].Flip();
			}
			this.plane.Flip();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BC9 File Offset: 0x00000DC9
		public override string ToString()
		{
			return string.Format("[{0}] {1}", this.vertices.Count, this.plane.normal);
		}

		// Token: 0x0400000A RID: 10
		public List<Vertex> vertices;

		// Token: 0x0400000B RID: 11
		public Plane plane;

		// Token: 0x0400000C RID: 12
		public Material material;
	}
}
