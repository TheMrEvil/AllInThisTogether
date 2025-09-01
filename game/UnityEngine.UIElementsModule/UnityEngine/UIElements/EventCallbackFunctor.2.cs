using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D8 RID: 472
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003E711 File Offset: 0x0003C911
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0003E719 File Offset: 0x0003C919
		internal TCallbackArgs userArgs
		{
			[CompilerGenerated]
			get
			{
				return this.<userArgs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<userArgs>k__BackingField = value;
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003E722 File Offset: 0x0003C922
		public EventCallbackFunctor(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, CallbackPhase phase, InvokePolicy invokePolicy) : base(phase, invokePolicy)
		{
			this.userArgs = userArgs;
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003E74C File Offset: 0x0003C94C
		public override void Invoke(EventBase evt, PropagationPhase propagationPhase)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			bool flag2 = evt.eventTypeId != this.m_EventTypeId;
			if (!flag2)
			{
				bool flag3 = base.PhaseMatches(propagationPhase);
				if (flag3)
				{
					using (new EventDebuggerLogCall(this.m_Callback, evt))
					{
						this.m_Callback(evt as TEventType, this.userArgs);
					}
				}
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003E7E0 File Offset: 0x0003C9E0
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x040006F4 RID: 1780
		private readonly EventCallback<TEventType, TCallbackArgs> m_Callback;

		// Token: 0x040006F5 RID: 1781
		private readonly long m_EventTypeId;

		// Token: 0x040006F6 RID: 1782
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TCallbackArgs <userArgs>k__BackingField;
	}
}
