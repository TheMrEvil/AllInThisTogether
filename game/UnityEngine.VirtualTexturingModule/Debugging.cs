using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000005 RID: 5
	[StaticAccessor("VirtualTexturing::Debugging", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class Debugging
	{
		// Token: 0x06000009 RID: 9
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNumHandles();

		// Token: 0x0600000A RID: 10
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GrabHandleInfo(out Debugging.Handle debugHandle, int index);

		// Token: 0x0600000B RID: 11
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetInfoDump();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12
		// (set) Token: 0x0600000D RID: 13
		[NativeThrows]
		public static extern bool debugTilesEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		[NativeThrows]
		public static extern bool resolvingEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		[NativeThrows]
		public static extern bool flushEveryTickEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x02000006 RID: 6
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingDebugHandle.h")]
		public struct Handle
		{
			// Token: 0x04000004 RID: 4
			public long handle;

			// Token: 0x04000005 RID: 5
			public string group;

			// Token: 0x04000006 RID: 6
			public string name;

			// Token: 0x04000007 RID: 7
			public int numLayers;

			// Token: 0x04000008 RID: 8
			public Material material;
		}
	}
}
