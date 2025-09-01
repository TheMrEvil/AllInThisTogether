using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Runtime/Export/GI/GIDebugVisualisation.bindings.h")]
	public static class GIDebugVisualisation
	{
		// Token: 0x06000008 RID: 8
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetRuntimeInputTextures();

		// Token: 0x06000009 RID: 9
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PlayCycleMode();

		// Token: 0x0600000A RID: 10
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PauseCycleMode();

		// Token: 0x0600000B RID: 11
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCycleMode();

		// Token: 0x0600000C RID: 12
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipSystems(int skip);

		// Token: 0x0600000D RID: 13
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipInstances(int skip);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14
		public static extern bool cycleMode { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15
		public static extern bool pauseCycleMode { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		public static extern GITextureType texType { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
