using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000158 RID: 344
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class MeshFilter : Component
	{
		// Token: 0x06000EA5 RID: 3749 RVA: 0x00004563 File Offset: 0x00002763
		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EA6 RID: 3750
		// (set) Token: 0x06000EA7 RID: 3751
		public extern Mesh sharedMesh { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EA8 RID: 3752
		// (set) Token: 0x06000EA9 RID: 3753
		public extern Mesh mesh { [NativeName("GetInstantiatedMeshFromScript")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetInstantiatedMesh")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EAA RID: 3754 RVA: 0x00010727 File Offset: 0x0000E927
		public MeshFilter()
		{
		}
	}
}
