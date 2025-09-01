using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000031 RID: 49
	[StaticAccessor("AudioSampleProviderExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSampleProviderExtensions.bindings.h")]
	internal static class AudioSampleProviderExtensionsInternal
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static float GetSpeed(this AudioSampleProvider provider)
		{
			return AudioSampleProviderExtensionsInternal.InternalGetAudioSampleProviderSpeed(provider.id);
		}

		// Token: 0x0600021A RID: 538
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetAudioSampleProviderSpeed(uint providerId);
	}
}
