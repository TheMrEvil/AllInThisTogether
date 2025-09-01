using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000008 RID: 8
	internal enum VFXTaskType
	{
		// Token: 0x040000AE RID: 174
		None,
		// Token: 0x040000AF RID: 175
		Spawner = 268435456,
		// Token: 0x040000B0 RID: 176
		Initialize = 536870912,
		// Token: 0x040000B1 RID: 177
		Update = 805306368,
		// Token: 0x040000B2 RID: 178
		Output = 1073741824,
		// Token: 0x040000B3 RID: 179
		CameraSort = 805306369,
		// Token: 0x040000B4 RID: 180
		PerCameraUpdate,
		// Token: 0x040000B5 RID: 181
		PerCameraSort,
		// Token: 0x040000B6 RID: 182
		ParticlePointOutput = 1073741824,
		// Token: 0x040000B7 RID: 183
		ParticleLineOutput,
		// Token: 0x040000B8 RID: 184
		ParticleQuadOutput,
		// Token: 0x040000B9 RID: 185
		ParticleHexahedronOutput,
		// Token: 0x040000BA RID: 186
		ParticleMeshOutput,
		// Token: 0x040000BB RID: 187
		ParticleTriangleOutput,
		// Token: 0x040000BC RID: 188
		ParticleOctagonOutput,
		// Token: 0x040000BD RID: 189
		ConstantRateSpawner = 268435456,
		// Token: 0x040000BE RID: 190
		BurstSpawner,
		// Token: 0x040000BF RID: 191
		PeriodicBurstSpawner,
		// Token: 0x040000C0 RID: 192
		VariableRateSpawner,
		// Token: 0x040000C1 RID: 193
		CustomCallbackSpawner,
		// Token: 0x040000C2 RID: 194
		SetAttributeSpawner,
		// Token: 0x040000C3 RID: 195
		EvaluateExpressionsSpawner
	}
}
