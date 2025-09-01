using System;

namespace UnityEngine
{
	// Token: 0x02000057 RID: 87
	[Obsolete("ParticleSystemVertexStreams is deprecated. Please use ParticleSystemVertexStream instead.", false)]
	[Flags]
	public enum ParticleSystemVertexStreams
	{
		// Token: 0x0400015F RID: 351
		Position = 1,
		// Token: 0x04000160 RID: 352
		Normal = 2,
		// Token: 0x04000161 RID: 353
		Tangent = 4,
		// Token: 0x04000162 RID: 354
		Color = 8,
		// Token: 0x04000163 RID: 355
		UV = 16,
		// Token: 0x04000164 RID: 356
		UV2BlendAndFrame = 32,
		// Token: 0x04000165 RID: 357
		CenterAndVertexID = 64,
		// Token: 0x04000166 RID: 358
		Size = 128,
		// Token: 0x04000167 RID: 359
		Rotation = 256,
		// Token: 0x04000168 RID: 360
		Velocity = 512,
		// Token: 0x04000169 RID: 361
		Lifetime = 1024,
		// Token: 0x0400016A RID: 362
		Custom1 = 2048,
		// Token: 0x0400016B RID: 363
		Custom2 = 4096,
		// Token: 0x0400016C RID: 364
		Random = 8192,
		// Token: 0x0400016D RID: 365
		None = 0,
		// Token: 0x0400016E RID: 366
		All = 2147483647
	}
}
