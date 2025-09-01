using System;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	public enum ParticleSystemShapeType
	{
		// Token: 0x040000A1 RID: 161
		Sphere,
		// Token: 0x040000A2 RID: 162
		[Obsolete("SphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		SphereShell,
		// Token: 0x040000A3 RID: 163
		Hemisphere,
		// Token: 0x040000A4 RID: 164
		[Obsolete("HemisphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		HemisphereShell,
		// Token: 0x040000A5 RID: 165
		Cone,
		// Token: 0x040000A6 RID: 166
		Box,
		// Token: 0x040000A7 RID: 167
		Mesh,
		// Token: 0x040000A8 RID: 168
		[Obsolete("ConeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeShell,
		// Token: 0x040000A9 RID: 169
		ConeVolume,
		// Token: 0x040000AA RID: 170
		[Obsolete("ConeVolumeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeVolumeShell,
		// Token: 0x040000AB RID: 171
		Circle,
		// Token: 0x040000AC RID: 172
		[Obsolete("CircleEdge is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		CircleEdge,
		// Token: 0x040000AD RID: 173
		SingleSidedEdge,
		// Token: 0x040000AE RID: 174
		MeshRenderer,
		// Token: 0x040000AF RID: 175
		SkinnedMeshRenderer,
		// Token: 0x040000B0 RID: 176
		BoxShell,
		// Token: 0x040000B1 RID: 177
		BoxEdge,
		// Token: 0x040000B2 RID: 178
		Donut,
		// Token: 0x040000B3 RID: 179
		Rectangle,
		// Token: 0x040000B4 RID: 180
		Sprite,
		// Token: 0x040000B5 RID: 181
		SpriteRenderer
	}
}
