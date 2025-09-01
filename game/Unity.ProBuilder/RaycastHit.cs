using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000048 RID: 72
	internal sealed class RaycastHit
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0001A273 File Offset: 0x00018473
		public RaycastHit(float distance, Vector3 point, Vector3 normal, int face)
		{
			this.distance = distance;
			this.point = point;
			this.normal = normal;
			this.face = face;
		}

		// Token: 0x040001AD RID: 429
		public float distance;

		// Token: 0x040001AE RID: 430
		public Vector3 point;

		// Token: 0x040001AF RID: 431
		public Vector3 normal;

		// Token: 0x040001B0 RID: 432
		public int face;
	}
}
