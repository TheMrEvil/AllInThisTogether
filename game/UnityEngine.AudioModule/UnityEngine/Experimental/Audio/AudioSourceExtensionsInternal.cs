using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSourceExtensions.bindings.h")]
	internal static class AudioSourceExtensionsInternal
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00003D11 File Offset: 0x00001F11
		public static void RegisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_RegisterSampleProviderWithAudioSource(source, provider.id);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00003D21 File Offset: 0x00001F21
		public static void UnregisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_UnregisterSampleProviderFromAudioSource(source, provider.id);
		}

		// Token: 0x0600021D RID: 541
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RegisterSampleProviderWithAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);

		// Token: 0x0600021E RID: 542
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_UnregisterSampleProviderFromAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);
	}
}
