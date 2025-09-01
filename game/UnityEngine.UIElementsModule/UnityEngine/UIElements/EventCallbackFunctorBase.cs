using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D6 RID: 470
	internal abstract class EventCallbackFunctorBase
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0003E5AB File Offset: 0x0003C7AB
		public CallbackPhase phase
		{
			[CompilerGenerated]
			get
			{
				return this.<phase>k__BackingField;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003E5B3 File Offset: 0x0003C7B3
		public InvokePolicy invokePolicy
		{
			[CompilerGenerated]
			get
			{
				return this.<invokePolicy>k__BackingField;
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003E5BB File Offset: 0x0003C7BB
		protected EventCallbackFunctorBase(CallbackPhase phase, InvokePolicy invokePolicy)
		{
			this.phase = phase;
			this.invokePolicy = invokePolicy;
		}

		// Token: 0x06000F0E RID: 3854
		public abstract void Invoke(EventBase evt, PropagationPhase propagationPhase);

		// Token: 0x06000F0F RID: 3855
		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase);

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		protected bool PhaseMatches(PropagationPhase propagationPhase)
		{
			CallbackPhase phase = this.phase;
			CallbackPhase callbackPhase = phase;
			if (callbackPhase != CallbackPhase.TargetAndBubbleUp)
			{
				if (callbackPhase == CallbackPhase.TrickleDownAndTarget)
				{
					bool flag = propagationPhase != PropagationPhase.TrickleDown && propagationPhase != PropagationPhase.AtTarget;
					if (flag)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag2 = propagationPhase != PropagationPhase.AtTarget && propagationPhase != PropagationPhase.BubbleUp;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040006F0 RID: 1776
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly CallbackPhase <phase>k__BackingField;

		// Token: 0x040006F1 RID: 1777
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly InvokePolicy <invokePolicy>k__BackingField;
	}
}
