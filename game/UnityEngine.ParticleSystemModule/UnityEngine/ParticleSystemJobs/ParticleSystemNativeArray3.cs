using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000061 RID: 97
	public struct ParticleSystemNativeArray3
	{
		// Token: 0x170001E8 RID: 488
		public Vector3 this[int index]
		{
			get
			{
				return new Vector3(this.x[index], this.y[index], this.z[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
			}
		}

		// Token: 0x04000178 RID: 376
		public NativeArray<float> x;

		// Token: 0x04000179 RID: 377
		public NativeArray<float> y;

		// Token: 0x0400017A RID: 378
		public NativeArray<float> z;
	}
}
