using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000040 RID: 64
	public static class AnimationPlayableBinding
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00004594 File Offset: 0x00002794
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000045C4 File Offset: 0x000027C4
		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}
