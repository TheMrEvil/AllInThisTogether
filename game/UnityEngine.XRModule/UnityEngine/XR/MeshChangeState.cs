using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002E RID: 46
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshChangeState
	{
		// Token: 0x04000105 RID: 261
		Added,
		// Token: 0x04000106 RID: 262
		Updated,
		// Token: 0x04000107 RID: 263
		Removed,
		// Token: 0x04000108 RID: 264
		Unchanged
	}
}
