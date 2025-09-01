using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x02000455 RID: 1109
	[RequiredByNativeCode]
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class SpriteRendererGroup
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x00041868 File Offset: 0x0003FA68
		public static void AddRenderers(NativeArray<SpriteIntermediateRendererInfo> renderers)
		{
			SpriteRendererGroup.AddRenderers(renderers.GetUnsafeReadOnlyPtr<SpriteIntermediateRendererInfo>(), renderers.Length);
		}

		// Token: 0x060027BE RID: 10174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void AddRenderers(void* renderers, int count);

		// Token: 0x060027BF RID: 10175
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear();

		// Token: 0x060027C0 RID: 10176 RVA: 0x00002072 File Offset: 0x00000272
		public SpriteRendererGroup()
		{
		}
	}
}
