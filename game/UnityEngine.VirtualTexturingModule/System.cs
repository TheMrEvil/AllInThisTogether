using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("VirtualTexturing::System", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class System
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		internal static extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000002 RID: 2
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update();

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		[NativeThrows]
		internal static void SetDebugFlag(Guid guid, bool enabled)
		{
			System.SetDebugFlag(guid.ToByteArray(), enabled);
		}

		// Token: 0x06000004 RID: 4
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDebugFlag(byte[] guid, bool enabled);

		// Token: 0x04000001 RID: 1
		public const int AllMips = 2147483647;
	}
}
