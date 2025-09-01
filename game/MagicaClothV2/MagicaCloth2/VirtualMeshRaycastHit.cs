using System;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000135 RID: 309
	public struct VirtualMeshRaycastHit : IComparable<VirtualMeshRaycastHit>, IValid
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0002B5EE File Offset: 0x000297EE
		public int CompareTo(VirtualMeshRaycastHit other)
		{
			if (this.distance == other.distance)
			{
				return 0;
			}
			if (this.distance >= other.distance)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002B611 File Offset: 0x00029811
		public bool IsValid()
		{
			return this.type > VirtualMeshPrimitive.None;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002B61C File Offset: 0x0002981C
		public override string ToString()
		{
			return string.Format("{0} [{1}] pos:{2}, dist:{3}, nor:{4}", new object[]
			{
				this.type,
				this.index,
				this.position,
				this.distance,
				this.normal
			});
		}

		// Token: 0x040007E8 RID: 2024
		public VirtualMeshPrimitive type;

		// Token: 0x040007E9 RID: 2025
		public int index;

		// Token: 0x040007EA RID: 2026
		public float3 position;

		// Token: 0x040007EB RID: 2027
		public float3 normal;

		// Token: 0x040007EC RID: 2028
		public float distance;
	}
}
