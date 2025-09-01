using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Audio/Public/AudioMixerGroup.h")]
	public class AudioMixerGroup : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00003481 File Offset: 0x00001681
		internal AudioMixerGroup()
		{
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B4 RID: 436
		[NativeProperty]
		public extern AudioMixer audioMixer { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
