using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DE RID: 478
	internal class EventCallbackRegistry
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003EBBD File Offset: 0x0003CDBD
		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003EBCC File Offset: 0x0003CDCC
		public EventCallbackRegistry()
		{
			this.m_IsInvoking = 0;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003EBE0 File Offset: 0x0003CDE0
		private EventCallbackList GetCallbackListForWriting()
		{
			bool flag = this.m_IsInvoking > 0;
			EventCallbackList result;
			if (flag)
			{
				bool flag2 = this.m_TemporaryCallbacks == null;
				if (flag2)
				{
					bool flag3 = this.m_Callbacks != null;
					if (flag3)
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks);
					}
					else
					{
						this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(null);
					}
				}
				result = this.m_TemporaryCallbacks;
			}
			else
			{
				bool flag4 = this.m_Callbacks == null;
				if (flag4)
				{
					this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null);
				}
				result = this.m_Callbacks;
			}
			return result;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003EC6C File Offset: 0x0003CE6C
		private EventCallbackList GetCallbackListForReading()
		{
			bool flag = this.m_TemporaryCallbacks != null;
			EventCallbackList result;
			if (flag)
			{
				result = this.m_TemporaryCallbacks;
			}
			else
			{
				result = this.m_Callbacks;
			}
			return result;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003EC9C File Offset: 0x0003CE9C
		private bool ShouldRegisterCallback(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				bool flag2 = callbackListForReading != null;
				result = (!flag2 || !callbackListForReading.Contains(eventTypeId, callback, phase));
			}
			return result;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003ECDC File Offset: 0x0003CEDC
		private bool UnregisterCallback(long eventTypeId, Delegate callback, TrickleDown useTrickleDown)
		{
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForWriting = this.GetCallbackListForWriting();
				CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
				result = callbackListForWriting.Remove(eventTypeId, callback, phase);
			}
			return result;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003ED14 File Offset: 0x0003CF14
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long eventTypeId = EventBase<TEventType>.TypeId();
			CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList == null || !eventCallbackList.Contains(eventTypeId, callback, phase);
			if (flag2)
			{
				eventCallbackList = this.GetCallbackListForWriting();
				eventCallbackList.Add(new EventCallbackFunctor<TEventType>(callback, phase, invokePolicy));
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public void RegisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			long eventTypeId = EventBase<TEventType>.TypeId();
			CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
			EventCallbackList eventCallbackList = this.GetCallbackListForReading();
			bool flag2 = eventCallbackList != null;
			if (flag2)
			{
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = eventCallbackList.Find(eventTypeId, callback, phase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag3 = eventCallbackFunctor != null;
				if (flag3)
				{
					eventCallbackFunctor.userArgs = userArgs;
					return;
				}
			}
			eventCallbackList = this.GetCallbackListForWriting();
			eventCallbackList.Add(new EventCallbackFunctor<TEventType, TCallbackArgs>(callback, userArgs, phase, invokePolicy));
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003EE00 File Offset: 0x0003D000
		public bool UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(eventTypeId, callback, useTrickleDown);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003EE24 File Offset: 0x0003D024
		public bool UnregisterCallback<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			long eventTypeId = EventBase<TEventType>.TypeId();
			return this.UnregisterCallback(eventTypeId, callback, useTrickleDown);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003EE48 File Offset: 0x0003D048
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userArgs) where TEventType : EventBase<TEventType>, new()
		{
			userArgs = default(TCallbackArgs);
			bool flag = callback == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				EventCallbackList callbackListForReading = this.GetCallbackListForReading();
				long eventTypeId = EventBase<TEventType>.TypeId();
				CallbackPhase phase = (useTrickleDown == TrickleDown.TrickleDown) ? CallbackPhase.TrickleDownAndTarget : CallbackPhase.TargetAndBubbleUp;
				EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = callbackListForReading.Find(eventTypeId, callback, phase) as EventCallbackFunctor<TEventType, TCallbackArgs>;
				bool flag2 = eventCallbackFunctor == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					userArgs = eventCallbackFunctor.userArgs;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
		public void InvokeCallbacks(EventBase evt, PropagationPhase propagationPhase)
		{
			bool flag = this.m_Callbacks == null;
			if (!flag)
			{
				this.m_IsInvoking++;
				int i = 0;
				while (i < this.m_Callbacks.Count)
				{
					bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
					if (isImmediatePropagationStopped)
					{
						break;
					}
					if (!evt.skipDisabledElements)
					{
						goto IL_6B;
					}
					VisualElement visualElement = evt.currentTarget as VisualElement;
					if (visualElement == null || visualElement.enabledInHierarchy)
					{
						goto IL_6B;
					}
					bool flag2 = this.m_Callbacks[i].invokePolicy != InvokePolicy.IncludeDisabled;
					IL_6C:
					bool flag3 = flag2;
					if (!flag3)
					{
						this.m_Callbacks[i].Invoke(evt, propagationPhase);
					}
					i++;
					continue;
					IL_6B:
					flag2 = false;
					goto IL_6C;
				}
				this.m_IsInvoking--;
				bool flag4 = this.m_IsInvoking == 0;
				if (flag4)
				{
					bool flag5 = this.m_TemporaryCallbacks != null;
					if (flag5)
					{
						EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
					}
				}
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
		public bool HasTrickleDownHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.trickleDownCallbackCount > 0;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003EFF0 File Offset: 0x0003D1F0
		public bool HasBubbleHandlers()
		{
			return this.m_Callbacks != null && this.m_Callbacks.bubbleUpCallbackCount > 0;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003F01B File Offset: 0x0003D21B
		// Note: this type is marked as 'beforefieldinit'.
		static EventCallbackRegistry()
		{
		}

		// Token: 0x04000704 RID: 1796
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		// Token: 0x04000705 RID: 1797
		private EventCallbackList m_Callbacks;

		// Token: 0x04000706 RID: 1798
		private EventCallbackList m_TemporaryCallbacks;

		// Token: 0x04000707 RID: 1799
		private int m_IsInvoking;
	}
}
