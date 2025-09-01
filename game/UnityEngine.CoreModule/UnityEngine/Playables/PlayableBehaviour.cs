using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200043E RID: 1086
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x00008CBB File Offset: 0x00006EBB
		public PlayableBehaviour()
		{
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public virtual void OnBehaviourDelay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00004563 File Offset: 0x00002763
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0003F80C File Offset: 0x0003DA0C
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
