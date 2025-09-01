using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000011 RID: 17
	internal interface Triangulatable
	{
		// Token: 0x0600009F RID: 159
		void Prepare(TriangulationContext tcx);

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160
		IList<TriangulationPoint> Points { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A1 RID: 161
		IList<DelaunayTriangle> Triangles { get; }

		// Token: 0x060000A2 RID: 162
		void AddTriangle(DelaunayTriangle t);

		// Token: 0x060000A3 RID: 163
		void AddTriangles(IEnumerable<DelaunayTriangle> list);

		// Token: 0x060000A4 RID: 164
		void ClearTriangles();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165
		TriangulationMode TriangulationMode { get; }
	}
}
