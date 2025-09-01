using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045C RID: 1116
	public struct PointLight
	{
		// Token: 0x04000E6F RID: 3695
		public int instanceID;

		// Token: 0x04000E70 RID: 3696
		public bool shadow;

		// Token: 0x04000E71 RID: 3697
		public LightMode mode;

		// Token: 0x04000E72 RID: 3698
		public Vector3 position;

		// Token: 0x04000E73 RID: 3699
		public Quaternion orientation;

		// Token: 0x04000E74 RID: 3700
		public LinearColor color;

		// Token: 0x04000E75 RID: 3701
		public LinearColor indirectColor;

		// Token: 0x04000E76 RID: 3702
		public float range;

		// Token: 0x04000E77 RID: 3703
		public float sphereRadius;

		// Token: 0x04000E78 RID: 3704
		public FalloffType falloff;
	}
}
