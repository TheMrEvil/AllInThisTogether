using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x0200000B RID: 11
	[NativeType(Header = "Modules/DSPGraph/Public/ExecuteContext.bindings.h")]
	internal struct ExecuteContextInternal
	{
		// Token: 0x06000049 RID: 73
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_PostEvent(void* dspNodePtr, long eventTypeHashCode, void* eventPtr, int eventSize);
	}
}
