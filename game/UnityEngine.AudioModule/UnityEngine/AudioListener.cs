using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[RequireComponent(typeof(Transform))]
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioListener : AudioBehaviour
	{
		// Token: 0x06000054 RID: 84
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper([Out] float[] samples, int channel);

		// Token: 0x06000055 RID: 85
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window);

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		public static extern float volume { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		[NativeProperty("ListenerPause")]
		public static extern bool pause { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		public extern AudioVelocityUpdateMode velocityUpdateMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600005C RID: 92 RVA: 0x0000276C File Offset: 0x0000096C
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000278E File Offset: 0x0000098E
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000279C File Offset: 0x0000099C
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000027BF File Offset: 0x000009BF
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000027CB File Offset: 0x000009CB
		public AudioListener()
		{
		}
	}
}
