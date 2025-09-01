using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000004 RID: 4
	[NativeType(Header = "Modules/DSPGraph/Public/AudioMemoryManager.bindings.h")]
	internal struct AudioMemoryManager
	{
		// Token: 0x0600000C RID: 12
		[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Internal_AllocateAudioMemory(int size, int alignment);

		// Token: 0x0600000D RID: 13
		[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_FreeAudioMemory(void* memory);
	}
}
