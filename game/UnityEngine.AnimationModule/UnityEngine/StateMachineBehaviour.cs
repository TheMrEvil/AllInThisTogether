using System;
using UnityEngine.Animations;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[RequiredByNativeCode]
	public abstract class StateMachineBehaviour : ScriptableObject
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002059 File Offset: 0x00000259
		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000205C File Offset: 0x0000025C
		protected StateMachineBehaviour()
		{
		}
	}
}
