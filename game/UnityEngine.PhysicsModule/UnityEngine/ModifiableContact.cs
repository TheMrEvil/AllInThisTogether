using System;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	internal struct ModifiableContact
	{
		// Token: 0x04000092 RID: 146
		public Vector3 contact;

		// Token: 0x04000093 RID: 147
		public float separation;

		// Token: 0x04000094 RID: 148
		public Vector3 targetVelocity;

		// Token: 0x04000095 RID: 149
		public float maxImpulse;

		// Token: 0x04000096 RID: 150
		public Vector3 normal;

		// Token: 0x04000097 RID: 151
		public float restitution;

		// Token: 0x04000098 RID: 152
		public uint materialFlags;

		// Token: 0x04000099 RID: 153
		public ushort materialIndex;

		// Token: 0x0400009A RID: 154
		public ushort otherMaterialIndex;

		// Token: 0x0400009B RID: 155
		public float staticFriction;

		// Token: 0x0400009C RID: 156
		public float dynamicFriction;
	}
}
