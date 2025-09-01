using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045D RID: 1117
	public struct SpotLight
	{
		// Token: 0x04000E79 RID: 3705
		public int instanceID;

		// Token: 0x04000E7A RID: 3706
		public bool shadow;

		// Token: 0x04000E7B RID: 3707
		public LightMode mode;

		// Token: 0x04000E7C RID: 3708
		public Vector3 position;

		// Token: 0x04000E7D RID: 3709
		public Quaternion orientation;

		// Token: 0x04000E7E RID: 3710
		public LinearColor color;

		// Token: 0x04000E7F RID: 3711
		public LinearColor indirectColor;

		// Token: 0x04000E80 RID: 3712
		public float range;

		// Token: 0x04000E81 RID: 3713
		public float sphereRadius;

		// Token: 0x04000E82 RID: 3714
		public float coneAngle;

		// Token: 0x04000E83 RID: 3715
		public float innerConeAngle;

		// Token: 0x04000E84 RID: 3716
		public FalloffType falloff;

		// Token: 0x04000E85 RID: 3717
		public AngularFalloffType angularFalloff;
	}
}
