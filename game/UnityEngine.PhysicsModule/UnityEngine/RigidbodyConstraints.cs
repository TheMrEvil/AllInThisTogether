using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public enum RigidbodyConstraints
	{
		// Token: 0x04000002 RID: 2
		None,
		// Token: 0x04000003 RID: 3
		FreezePositionX = 2,
		// Token: 0x04000004 RID: 4
		FreezePositionY = 4,
		// Token: 0x04000005 RID: 5
		FreezePositionZ = 8,
		// Token: 0x04000006 RID: 6
		FreezeRotationX = 16,
		// Token: 0x04000007 RID: 7
		FreezeRotationY = 32,
		// Token: 0x04000008 RID: 8
		FreezeRotationZ = 64,
		// Token: 0x04000009 RID: 9
		FreezePosition = 14,
		// Token: 0x0400000A RID: 10
		FreezeRotation = 112,
		// Token: 0x0400000B RID: 11
		FreezeAll = 126
	}
}
