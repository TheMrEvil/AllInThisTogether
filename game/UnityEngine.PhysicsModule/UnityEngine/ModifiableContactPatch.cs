using System;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	internal struct ModifiableContactPatch
	{
		// Token: 0x0400009D RID: 157
		public ModifiableMassProperties massProperties;

		// Token: 0x0400009E RID: 158
		public Vector3 normal;

		// Token: 0x0400009F RID: 159
		public float restitution;

		// Token: 0x040000A0 RID: 160
		public float dynamicFriction;

		// Token: 0x040000A1 RID: 161
		public float staticFriction;

		// Token: 0x040000A2 RID: 162
		public byte startContactIndex;

		// Token: 0x040000A3 RID: 163
		public byte contactCount;

		// Token: 0x040000A4 RID: 164
		public byte materialFlags;

		// Token: 0x040000A5 RID: 165
		public byte internalFlags;

		// Token: 0x040000A6 RID: 166
		public ushort materialIndex;

		// Token: 0x040000A7 RID: 167
		public ushort otherMaterialIndex;

		// Token: 0x02000024 RID: 36
		public enum Flags
		{
			// Token: 0x040000A9 RID: 169
			HasFaceIndices = 1,
			// Token: 0x040000AA RID: 170
			HasModifiedMassRatios = 8,
			// Token: 0x040000AB RID: 171
			HasTargetVelocity = 16,
			// Token: 0x040000AC RID: 172
			HasMaxImpulse = 32,
			// Token: 0x040000AD RID: 173
			RegeneratePatches = 64
		}
	}
}
