using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200047E RID: 1150
	[NativeHeader("Runtime/Export/Graphics/RayTracingAccelerationStructure.bindings.h")]
	[Flags]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	public enum RayTracingSubMeshFlags
	{
		// Token: 0x04000F89 RID: 3977
		Disabled = 0,
		// Token: 0x04000F8A RID: 3978
		Enabled = 1,
		// Token: 0x04000F8B RID: 3979
		ClosestHitOnly = 2,
		// Token: 0x04000F8C RID: 3980
		UniqueAnyHitCalls = 4
	}
}
