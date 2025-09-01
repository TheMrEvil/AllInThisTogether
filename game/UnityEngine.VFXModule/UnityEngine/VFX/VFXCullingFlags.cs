using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000005 RID: 5
	[Flags]
	internal enum VFXCullingFlags
	{
		// Token: 0x04000002 RID: 2
		CullNone = 0,
		// Token: 0x04000003 RID: 3
		CullSimulation = 1,
		// Token: 0x04000004 RID: 4
		CullBoundsUpdate = 2,
		// Token: 0x04000005 RID: 5
		CullDefault = 3
	}
}
