using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045F RID: 1119
	public struct DiscLight
	{
		// Token: 0x04000E91 RID: 3729
		public int instanceID;

		// Token: 0x04000E92 RID: 3730
		public bool shadow;

		// Token: 0x04000E93 RID: 3731
		public LightMode mode;

		// Token: 0x04000E94 RID: 3732
		public Vector3 position;

		// Token: 0x04000E95 RID: 3733
		public Quaternion orientation;

		// Token: 0x04000E96 RID: 3734
		public LinearColor color;

		// Token: 0x04000E97 RID: 3735
		public LinearColor indirectColor;

		// Token: 0x04000E98 RID: 3736
		public float range;

		// Token: 0x04000E99 RID: 3737
		public float radius;

		// Token: 0x04000E9A RID: 3738
		public FalloffType falloff;
	}
}
