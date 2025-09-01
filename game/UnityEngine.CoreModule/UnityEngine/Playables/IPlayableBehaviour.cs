using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000437 RID: 1079
	public interface IPlayableBehaviour
	{
		// Token: 0x06002584 RID: 9604
		[RequiredByNativeCode]
		void OnGraphStart(Playable playable);

		// Token: 0x06002585 RID: 9605
		[RequiredByNativeCode]
		void OnGraphStop(Playable playable);

		// Token: 0x06002586 RID: 9606
		[RequiredByNativeCode]
		void OnPlayableCreate(Playable playable);

		// Token: 0x06002587 RID: 9607
		[RequiredByNativeCode]
		void OnPlayableDestroy(Playable playable);

		// Token: 0x06002588 RID: 9608
		[RequiredByNativeCode]
		void OnBehaviourPlay(Playable playable, FrameData info);

		// Token: 0x06002589 RID: 9609
		[RequiredByNativeCode]
		void OnBehaviourPause(Playable playable, FrameData info);

		// Token: 0x0600258A RID: 9610
		[RequiredByNativeCode]
		void PrepareFrame(Playable playable, FrameData info);

		// Token: 0x0600258B RID: 9611
		[RequiredByNativeCode]
		void ProcessFrame(Playable playable, FrameData info, object playerData);
	}
}
