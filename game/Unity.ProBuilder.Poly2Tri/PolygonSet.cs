using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000005 RID: 5
	internal class PolygonSet
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025A1 File Offset: 0x000007A1
		public PolygonSet()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025B4 File Offset: 0x000007B4
		public PolygonSet(Polygon poly)
		{
			this._polygons.Add(poly);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025D3 File Offset: 0x000007D3
		public void Add(Polygon p)
		{
			this._polygons.Add(p);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025E1 File Offset: 0x000007E1
		public IEnumerable<Polygon> Polygons
		{
			get
			{
				return this._polygons;
			}
		}

		// Token: 0x04000009 RID: 9
		protected List<Polygon> _polygons = new List<Polygon>();
	}
}
