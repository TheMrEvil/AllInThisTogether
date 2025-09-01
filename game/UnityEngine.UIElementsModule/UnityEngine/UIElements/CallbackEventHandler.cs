using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E0 RID: 480
	public abstract class CallbackEventHandler : IEventHandler
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0003F028 File Offset: 0x0003D228
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, InvokePolicy.Default);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003F060 File Offset: 0x0003D260
		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown, InvokePolicy.Default);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003F098 File Offset: 0x0003D298
		internal void RegisterCallback<TEventType>(EventCallback<TEventType> callback, InvokePolicy invokePolicy, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry == null;
			if (flag)
			{
				this.m_CallbackRegistry = new EventCallbackRegistry();
			}
			this.m_CallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, invokePolicy);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003F0D0 File Offset: 0x0003D2D0
		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003F0FC File Offset: 0x0003D2FC
		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = this.m_CallbackRegistry != null;
			if (flag)
			{
				this.m_CallbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003F128 File Offset: 0x0003D328
		internal bool TryGetUserArgs<TEventType, TCallbackArgs>(EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown, out TCallbackArgs userData) where TEventType : EventBase<TEventType>, new()
		{
			userData = default(TCallbackArgs);
			bool flag = this.m_CallbackRegistry != null;
			return flag && this.m_CallbackRegistry.TryGetUserArgs<TEventType, TCallbackArgs>(callback, useTrickleDown, out userData);
		}

		// Token: 0x06000F45 RID: 3909
		public abstract void SendEvent(EventBase e);

		// Token: 0x06000F46 RID: 3910
		internal abstract void SendEvent(EventBase e, DispatchMode dispatchMode);

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003F161 File Offset: 0x0003D361
		internal void HandleEventAtTargetPhase(EventBase evt)
		{
			evt.currentTarget = evt.target;
			evt.propagationPhase = PropagationPhase.AtTarget;
			this.HandleEvent(evt);
			evt.propagationPhase = PropagationPhase.DefaultActionAtTarget;
			this.HandleEvent(evt);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003F194 File Offset: 0x0003D394
		public virtual void HandleEvent(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				switch (evt.propagationPhase)
				{
				case PropagationPhase.TrickleDown:
				case PropagationPhase.BubbleUp:
				{
					bool flag2 = !evt.isPropagationStopped;
					if (flag2)
					{
						EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
						if (callbackRegistry != null)
						{
							callbackRegistry.InvokeCallbacks(evt, evt.propagationPhase);
						}
					}
					break;
				}
				case PropagationPhase.AtTarget:
				{
					bool flag3 = !evt.isPropagationStopped;
					if (flag3)
					{
						EventCallbackRegistry callbackRegistry2 = this.m_CallbackRegistry;
						if (callbackRegistry2 != null)
						{
							callbackRegistry2.InvokeCallbacks(evt, PropagationPhase.TrickleDown);
						}
					}
					bool flag4 = !evt.isPropagationStopped;
					if (flag4)
					{
						EventCallbackRegistry callbackRegistry3 = this.m_CallbackRegistry;
						if (callbackRegistry3 != null)
						{
							callbackRegistry3.InvokeCallbacks(evt, PropagationPhase.BubbleUp);
						}
					}
					break;
				}
				case PropagationPhase.DefaultAction:
				{
					bool flag5 = !evt.isDefaultPrevented;
					if (flag5)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							bool flag6;
							if (evt.skipDisabledElements)
							{
								VisualElement visualElement = this as VisualElement;
								if (visualElement != null)
								{
									flag6 = !visualElement.enabledInHierarchy;
									goto IL_15A;
								}
							}
							flag6 = false;
							IL_15A:
							bool flag7 = flag6;
							if (flag7)
							{
								this.ExecuteDefaultActionDisabled(evt);
							}
							else
							{
								this.ExecuteDefaultAction(evt);
							}
						}
					}
					break;
				}
				case PropagationPhase.DefaultActionAtTarget:
				{
					bool flag8 = !evt.isDefaultPrevented;
					if (flag8)
					{
						using (new EventDebuggerLogExecuteDefaultAction(evt))
						{
							bool flag9;
							if (evt.skipDisabledElements)
							{
								VisualElement visualElement2 = this as VisualElement;
								if (visualElement2 != null)
								{
									flag9 = !visualElement2.enabledInHierarchy;
									goto IL_F2;
								}
							}
							flag9 = false;
							IL_F2:
							bool flag10 = flag9;
							if (flag10)
							{
								this.ExecuteDefaultActionDisabledAtTarget(evt);
							}
							else
							{
								this.ExecuteDefaultActionAtTarget(evt);
							}
						}
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003F344 File Offset: 0x0003D544
		public bool HasTrickleDownHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasTrickleDownHandlers();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003F36C File Offset: 0x0003D56C
		public bool HasBubbleUpHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasBubbleHandlers();
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00002166 File Offset: 0x00000366
		internal virtual void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00002166 File Offset: 0x00000366
		internal virtual void ExecuteDefaultActionDisabled(EventBase evt)
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000020C2 File Offset: 0x000002C2
		protected CallbackEventHandler()
		{
		}

		// Token: 0x04000708 RID: 1800
		private EventCallbackRegistry m_CallbackRegistry;
	}
}
