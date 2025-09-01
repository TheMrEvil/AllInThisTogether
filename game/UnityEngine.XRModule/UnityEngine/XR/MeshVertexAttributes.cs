using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	[Flags]
	public enum MeshVertexAttributes
	{
		// Token: 0x040000FC RID: 252
		None = 0,
		// Token: 0x040000FD RID: 253
		Normals = 1,
		// Token: 0x040000FE RID: 254
		Tangents = 2,
		// Token: 0x040000FF RID: 255
		UVs = 4,
		// Token: 0x04000100 RID: 256
		Colors = 8
	}
}
