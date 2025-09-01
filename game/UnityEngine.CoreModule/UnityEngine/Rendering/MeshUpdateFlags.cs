using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A3 RID: 931
	[Flags]
	public enum MeshUpdateFlags
	{
		// Token: 0x04000A46 RID: 2630
		Default = 0,
		// Token: 0x04000A47 RID: 2631
		DontValidateIndices = 1,
		// Token: 0x04000A48 RID: 2632
		DontResetBoneBounds = 2,
		// Token: 0x04000A49 RID: 2633
		DontNotifyMeshUsers = 4,
		// Token: 0x04000A4A RID: 2634
		DontRecalculateBounds = 8
	}
}
