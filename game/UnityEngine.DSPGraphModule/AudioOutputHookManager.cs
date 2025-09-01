using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000005 RID: 5
	[NativeType(Header = "Modules/DSPGraph/Public/AudioOutputHookManager.bindings.h")]
	internal struct AudioOutputHookManager
	{
		// Token: 0x0600000E RID: 14
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateAudioOutputHook(out Handle outputHook, void* jobReflectionData, void* jobData);

		// Token: 0x0600000F RID: 15
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisposeAudioOutputHook(ref Handle outputHook);
	}
}
