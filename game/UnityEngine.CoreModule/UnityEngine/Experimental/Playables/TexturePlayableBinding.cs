using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200046C RID: 1132
	public static class TexturePlayableBinding
	{
		// Token: 0x0600281B RID: 10267 RVA: 0x00042D68 File Offset: 0x00040F68
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), new PlayableBinding.CreateOutputMethod(TexturePlayableBinding.CreateTextureOutput));
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00042D98 File Offset: 0x00040F98
		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}
