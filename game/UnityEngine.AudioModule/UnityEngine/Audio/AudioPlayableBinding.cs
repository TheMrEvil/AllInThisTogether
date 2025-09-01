using System;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	// Token: 0x02000029 RID: 41
	public static class AudioPlayableBinding
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00003640 File Offset: 0x00001840
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), new PlayableBinding.CreateOutputMethod(AudioPlayableBinding.CreateAudioOutput));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003670 File Offset: 0x00001870
		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
}
