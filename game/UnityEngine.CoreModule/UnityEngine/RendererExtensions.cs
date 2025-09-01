using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013D RID: 317
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class RendererExtensions
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0000F32E File Offset: 0x0000D52E
		public static void UpdateGIMaterials(this Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer(renderer);
		}

		// Token: 0x06000A70 RID: 2672
		[FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateGIMaterialsForRenderer(Renderer renderer);
	}
}
