using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x02000018 RID: 24
	internal abstract class TriangulationDebugContext
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004BD6 File Offset: 0x00002DD6
		public TriangulationDebugContext(TriangulationContext tcx)
		{
			this._tcx = tcx;
		}

		// Token: 0x060000C9 RID: 201
		public abstract void Clear();

		// Token: 0x04000041 RID: 65
		protected TriangulationContext _tcx;
	}
}
