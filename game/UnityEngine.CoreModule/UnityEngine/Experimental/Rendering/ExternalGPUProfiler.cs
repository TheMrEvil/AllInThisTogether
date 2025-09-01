using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000474 RID: 1140
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class ExternalGPUProfiler
	{
		// Token: 0x0600283E RID: 10302
		[FreeFunction("ExternalGPUProfilerBindings::BeginGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginGPUCapture();

		// Token: 0x0600283F RID: 10303
		[FreeFunction("ExternalGPUProfilerBindings::EndGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndGPUCapture();

		// Token: 0x06002840 RID: 10304
		[FreeFunction("ExternalGPUProfilerBindings::IsAttached")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAttached();
	}
}
