using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045B RID: 1115
	public struct DirectionalLight
	{
		// Token: 0x04000E66 RID: 3686
		public int instanceID;

		// Token: 0x04000E67 RID: 3687
		public bool shadow;

		// Token: 0x04000E68 RID: 3688
		public LightMode mode;

		// Token: 0x04000E69 RID: 3689
		public Vector3 position;

		// Token: 0x04000E6A RID: 3690
		public Quaternion orientation;

		// Token: 0x04000E6B RID: 3691
		public LinearColor color;

		// Token: 0x04000E6C RID: 3692
		public LinearColor indirectColor;

		// Token: 0x04000E6D RID: 3693
		public float penumbraWidthRadian;

		// Token: 0x04000E6E RID: 3694
		[Obsolete("Directional lights support cookies now. In order to position the cookie projection in the world, a position and full orientation are necessary. Use the position and orientation members instead of the direction parameter.", true)]
		public Vector3 direction;
	}
}
