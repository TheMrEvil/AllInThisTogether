using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000009 RID: 9
	[NativeType(Header = "Modules/DSPGraph/Public/DSPNodeUpdateRequest.bindings.h")]
	internal struct DSPNodeUpdateRequestHandleInternal
	{
		// Token: 0x0600003A RID: 58
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Internal_GetUpdateJobData(ref Handle graph, ref Handle requestHandle);

		// Token: 0x0600003B RID: 59
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_HasError(ref Handle graph, ref Handle requestHandle);

		// Token: 0x0600003C RID: 60
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetDSPNode(ref Handle graph, ref Handle requestHandle, ref Handle node);

		// Token: 0x0600003D RID: 61
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetFence(ref Handle graph, ref Handle requestHandle, ref JobHandle fence);

		// Token: 0x0600003E RID: 62
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Dispose(ref Handle graph, ref Handle requestHandle);
	}
}
