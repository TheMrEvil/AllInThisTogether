using System;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	public struct BoundingSphere
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00007FAA File Offset: 0x000061AA
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00007FBB File Offset: 0x000061BB
		public BoundingSphere(Vector4 packedSphere)
		{
			this.position = new Vector3(packedSphere.x, packedSphere.y, packedSphere.z);
			this.radius = packedSphere.w;
		}

		// Token: 0x04000366 RID: 870
		public Vector3 position;

		// Token: 0x04000367 RID: 871
		public float radius;
	}
}
