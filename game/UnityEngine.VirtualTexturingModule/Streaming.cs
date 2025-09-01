using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Streaming", StaticAccessorType.DoubleColon)]
	public static class Streaming
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002243 File Offset: 0x00000443
		[NativeThrows]
		public static void RequestRegion([NotNull("ArgumentNullException")] Material mat, int stackNameId, Rect r, int mipMap, int numMips)
		{
			Streaming.RequestRegion_Injected(mat, stackNameId, ref r, mipMap, numMips);
		}

		// Token: 0x06000022 RID: 34
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetTextureStackSize([NotNull("ArgumentNullException")] Material mat, int stackNameId, out int width, out int height);

		// Token: 0x06000023 RID: 35
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCPUCacheSize(int sizeInMegabytes);

		// Token: 0x06000024 RID: 36
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCPUCacheSize();

		// Token: 0x06000025 RID: 37
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGPUCacheSettings(GPUCacheSetting[] cacheSettings);

		// Token: 0x06000026 RID: 38
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GPUCacheSetting[] GetGPUCacheSettings();

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestRegion_Injected(Material mat, int stackNameId, ref Rect r, int mipMap, int numMips);
	}
}
