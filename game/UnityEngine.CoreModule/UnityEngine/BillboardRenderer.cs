using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000121 RID: 289
	[NativeHeader("Runtime/Graphics/Billboard/BillboardRenderer.h")]
	public sealed class BillboardRenderer : Renderer
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007F8 RID: 2040
		// (set) Token: 0x060007F9 RID: 2041
		public extern BillboardAsset billboard { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060007FA RID: 2042 RVA: 0x0000BF29 File Offset: 0x0000A129
		public BillboardRenderer()
		{
		}
	}
}
