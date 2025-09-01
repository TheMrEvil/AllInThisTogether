using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200009D RID: 157
	[NativeHeader("Runtime/Export/BurstLike/BurstLike.bindings.h")]
	[StaticAccessor("BurstLike", StaticAccessorType.DoubleColon)]
	internal static class BurstLike
	{
		// Token: 0x060002D8 RID: 728
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int NativeFunctionCall_Int_IntPtr_IntPtr(IntPtr function, IntPtr p0, IntPtr p1, out int error);

		// Token: 0x060002D9 RID: 729
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr StaticDataGetOrCreate(int key, int sizeInBytes, out int error);
	}
}
