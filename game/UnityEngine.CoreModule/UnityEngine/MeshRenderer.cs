using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	[NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
	public class MeshRenderer : Renderer
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00004563 File Offset: 0x00002763
		[RequiredByNativeCode]
		private void DontStripMeshRenderer()
		{
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000EE8 RID: 3816
		// (set) Token: 0x06000EE9 RID: 3817
		public extern Mesh additionalVertexStreams { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000EEA RID: 3818
		// (set) Token: 0x06000EEB RID: 3819
		public extern Mesh enlightenVertexStream { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EEC RID: 3820
		public extern int subMeshStartIndex { [NativeName("GetSubMeshStartIndex")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000EED RID: 3821 RVA: 0x0000BF29 File Offset: 0x0000A129
		public MeshRenderer()
		{
		}
	}
}
