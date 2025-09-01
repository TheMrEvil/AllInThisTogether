using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200019E RID: 414
	[NativeHeader("Runtime/Graphics/Mesh/MeshCombiner.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	internal struct StaticBatchingHelper
	{
		// Token: 0x06001090 RID: 4240
		[FreeFunction("MeshScripting::CombineMeshVerticesForStaticBatching")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x06001091 RID: 4241
		[FreeFunction("MeshScripting::CombineMeshIndicesForStaticBatching")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, Mesh combinedMesh);

		// Token: 0x06001092 RID: 4242
		[FreeFunction("IsMeshBatchable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsMeshBatchable(Mesh mesh);
	}
}
