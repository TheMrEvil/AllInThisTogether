using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000029 RID: 41
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshGenerationStatus
	{
		// Token: 0x040000EC RID: 236
		Success,
		// Token: 0x040000ED RID: 237
		InvalidMeshId,
		// Token: 0x040000EE RID: 238
		GenerationAlreadyInProgress,
		// Token: 0x040000EF RID: 239
		Canceled,
		// Token: 0x040000F0 RID: 240
		UnknownError
	}
}
