using System;
using System.Collections.Generic;

namespace UnityEngine.Playables
{
	// Token: 0x0200043C RID: 1084
	public interface IPlayableAsset
	{
		// Token: 0x06002597 RID: 9623
		Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002598 RID: 9624
		double duration { get; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002599 RID: 9625
		IEnumerable<PlayableBinding> outputs { get; }
	}
}
