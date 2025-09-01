using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D7 RID: 471
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x0003E62E File Offset: 0x0003C82E
		public EventCallbackFunctor(EventCallback<TEventType> callback, CallbackPhase phase, InvokePolicy invokePolicy = InvokePolicy.Default) : base(phase, invokePolicy)
		{
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003E64C File Offset: 0x0003C84C
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
						this.m_Callback(evt as TEventType);
					}
				}
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x040006F2 RID: 1778
		private readonly EventCallback<TEventType> m_Callback;

		// Token: 0x040006F3 RID: 1779
		private readonly long m_EventTypeId;
	}
}
