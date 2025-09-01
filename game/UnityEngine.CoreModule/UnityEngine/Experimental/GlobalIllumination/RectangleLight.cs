using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045E RID: 1118
	public struct RectangleLight
	{
		// Token: 0x04000E86 RID: 3718
		public int instanceID;

		// Token: 0x04000E87 RID: 3719
		public bool shadow;

		// Token: 0x04000E88 RID: 3720
		public LightMode mode;

		// Token: 0x04000E89 RID: 3721
		public Vector3 position;

		// Token: 0x04000E8A RID: 3722
		public Quaternion orientation;

		// Token: 0x04000E8B RID: 3723
		public LinearColor color;

		// Token: 0x04000E8C RID: 3724
		public LinearColor indirectColor;

		// Token: 0x04000E8D RID: 3725
		public float range;

		// Token: 0x04000E8E RID: 3726
		public float width;

		// Token: 0x04000E8F RID: 3727
		public float height;

		// Token: 0x04000E90 RID: 3728
		public FalloffType falloff;
	}
}
