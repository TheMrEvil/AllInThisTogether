using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000064 RID: 100
	internal struct NativeParticleData
	{
		// Token: 0x0400018D RID: 397
		internal int count;

		// Token: 0x0400018E RID: 398
		internal NativeParticleData.Array3 positions;

		// Token: 0x0400018F RID: 399
		internal NativeParticleData.Array3 velocities;

		// Token: 0x04000190 RID: 400
		internal NativeParticleData.Array3 axisOfRotations;

		// Token: 0x04000191 RID: 401
		internal NativeParticleData.Array3 rotations;

		// Token: 0x04000192 RID: 402
		internal NativeParticleData.Array3 rotationalSpeeds;

		// Token: 0x04000193 RID: 403
		internal NativeParticleData.Array3 sizes;

		// Token: 0x04000194 RID: 404
		internal unsafe void* startColors;

		// Token: 0x04000195 RID: 405
		internal unsafe void* aliveTimePercent;

		// Token: 0x04000196 RID: 406
		internal unsafe void* inverseStartLifetimes;

		// Token: 0x04000197 RID: 407
		internal unsafe void* randomSeeds;

		// Token: 0x04000198 RID: 408
		internal NativeParticleData.Array4 customData1;

		// Token: 0x04000199 RID: 409
		internal NativeParticleData.Array4 customData2;

		// Token: 0x0400019A RID: 410
		internal unsafe void* meshIndices;

		// Token: 0x02000065 RID: 101
		internal struct Array3
		{
			// Token: 0x0400019B RID: 411
			internal unsafe float* x;

			// Token: 0x0400019C RID: 412
			internal unsafe float* y;

			// Token: 0x0400019D RID: 413
			internal unsafe float* z;
		}

		// Token: 0x02000066 RID: 102
		internal struct Array4
		{
			// Token: 0x0400019E RID: 414
			internal unsafe float* x;

			// Token: 0x0400019F RID: 415
			internal unsafe float* y;

			// Token: 0x040001A0 RID: 416
			internal unsafe float* z;

			// Token: 0x040001A1 RID: 417
			internal unsafe float* w;
		}
	}
}
