using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000062 RID: 98
	public struct ParticleSystemNativeArray4
	{
		// Token: 0x170001E9 RID: 489
		public Vector4 this[int index]
		{
			get
			{
				return new Vector4(this.x[index], this.y[index], this.z[index], this.w[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
				this.w[index] = value.w;
			}
		}

		// Token: 0x0400017B RID: 379
		public NativeArray<float> x;

		// Token: 0x0400017C RID: 380
		public NativeArray<float> y;

		// Token: 0x0400017D RID: 381
		public NativeArray<float> z;

		// Token: 0x0400017E RID: 382
		public NativeArray<float> w;
	}
}
