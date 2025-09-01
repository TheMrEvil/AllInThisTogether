using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000044 RID: 68
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationWindowPreview
	{
		// Token: 0x060002A4 RID: 676
		void StartPreview();

		// Token: 0x060002A5 RID: 677
		void StopPreview();

		// Token: 0x060002A6 RID: 678
		void UpdatePreviewGraph(PlayableGraph graph);

		// Token: 0x060002A7 RID: 679
		Playable BuildPreviewGraph(PlayableGraph graph, Playable inputPlayable);
	}
}
