using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000006 RID: 6
	internal abstract class ActionItem
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020BC File Offset: 0x000002BC
		protected ActionItem()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CC File Offset: 0x000002CC
		public bool LowPriority
		{
			get
			{
				return this.lowPriority;
			}
			protected set
			{
				this.lowPriority = value;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020D5 File Offset: 0x000002D5
		public static void Schedule(Action<object> callback, object state)
		{
			ActionItem.Schedule(callback, state, false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020DF File Offset: 0x000002DF
		[SecuritySafeCritical]
		public static void Schedule(Action<object> callback, object state, bool lowPriority)
		{
			if (PartialTrustHelpers.ShouldFlowSecurityContext || WaitCallbackActionItem.ShouldUseActivity || Fx.Trace.IsEnd2EndActivityTracingEnabled)
			{
				new ActionItem.DefaultActionItem(callback, state, lowPriority).Schedule();
				return;
			}
			ActionItem.ScheduleCallback(callback, state, lowPriority);
		}

		// Token: 0x06000010 RID: 16
		[SecurityCritical]
		protected abstract void Invoke();

		// Token: 0x06000011 RID: 17 RVA: 0x00002111 File Offset: 0x00000311
		[SecurityCritical]
		protected void Schedule()
		{
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Action Item Is Already Scheduled"));
			}
			this.isScheduled = true;
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithoutContextCallback);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002142 File Offset: 0x00000342
		[SecurityCritical]
		protected void ScheduleWithoutContext()
		{
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Action Item Is Already Scheduled"));
			}
			this.isScheduled = true;
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithoutContextCallback);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002173 File Offset: 0x00000373
		[SecurityCritical]
		private static void ScheduleCallback(Action<object> callback, object state, bool lowPriority)
		{
			if (lowPriority)
			{
				IOThreadScheduler.ScheduleCallbackLowPriNoFlow(callback, state);
				return;
			}
			IOThreadScheduler.ScheduleCallbackNoFlow(callback, state);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002187 File Offset: 0x00000387
		[SecurityCritical]
		private void ScheduleCallback(Action<object> callback)
		{
			ActionItem.ScheduleCallback(callback, this, this.lowPriority);
		}

		// Token: 0x04000039 RID: 57
		private bool isScheduled;

		// Token: 0x0400003A RID: 58
		private bool lowPriority;

		// Token: 0x02000059 RID: 89
		[SecurityCritical]
		private static class CallbackHelper
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600034B RID: 843 RVA: 0x00010CF0 File Offset: 0x0000EEF0
			public static Action<object> InvokeWithoutContextCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.invokeWithoutContextCallback == null)
					{
						ActionItem.CallbackHelper.invokeWithoutContextCallback = new Action<object>(ActionItem.CallbackHelper.InvokeWithoutContext);
					}
					return ActionItem.CallbackHelper.invokeWithoutContextCallback;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x0600034C RID: 844 RVA: 0x00010D0F File Offset: 0x0000EF0F
			public static ContextCallback OnContextAppliedCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.onContextAppliedCallback == null)
					{
						ActionItem.CallbackHelper.onContextAppliedCallback = new ContextCallback(ActionItem.CallbackHelper.OnContextApplied);
					}
					return ActionItem.CallbackHelper.onContextAppliedCallback;
				}
			}

			// Token: 0x0600034D RID: 845 RVA: 0x00010D2E File Offset: 0x0000EF2E
			private static void InvokeWithoutContext(object state)
			{
				((ActionItem)state).Invoke();
				((ActionItem)state).isScheduled = false;
			}

			// Token: 0x0600034E RID: 846 RVA: 0x00010D47 File Offset: 0x0000EF47
			private static void OnContextApplied(object o)
			{
				((ActionItem)o).Invoke();
				((ActionItem)o).isScheduled = false;
			}

			// Token: 0x0400020A RID: 522
			private static Action<object> invokeWithoutContextCallback;

			// Token: 0x0400020B RID: 523
			private static ContextCallback onContextAppliedCallback;
		}

		// Token: 0x0200005A RID: 90
		private class DefaultActionItem : ActionItem
		{
			// Token: 0x0600034F RID: 847 RVA: 0x00010D60 File Offset: 0x0000EF60
			[SecuritySafeCritical]
			public DefaultActionItem(Action<object> callback, object state, bool isLowPriority)
			{
				base.LowPriority = isLowPriority;
				this.callback = callback;
				this.state = state;
				if (WaitCallbackActionItem.ShouldUseActivity)
				{
					this.flowLegacyActivityId = true;
					this.activityId = DiagnosticTraceBase.ActivityId;
				}
				if (Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
					if (TraceCore.ActionItemScheduledIsEnabled(Fx.Trace))
					{
						TraceCore.ActionItemScheduled(Fx.Trace, this.eventTraceActivity);
					}
				}
			}

			// Token: 0x06000350 RID: 848 RVA: 0x00010DD5 File Offset: 0x0000EFD5
			[SecurityCritical]
			protected override void Invoke()
			{
				if (this.flowLegacyActivityId || Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.TraceAndInvoke();
					return;
				}
				this.callback(this.state);
			}

			// Token: 0x06000351 RID: 849 RVA: 0x00010E04 File Offset: 0x0000F004
			[SecurityCritical]
			private void TraceAndInvoke()
			{
				if (this.flowLegacyActivityId)
				{
					Guid guid = DiagnosticTraceBase.ActivityId;
					try
					{
						DiagnosticTraceBase.ActivityId = this.activityId;
						this.callback(this.state);
						return;
					}
					finally
					{
						DiagnosticTraceBase.ActivityId = guid;
					}
				}
				Guid empty = Guid.Empty;
				bool flag = false;
				try
				{
					if (this.eventTraceActivity != null)
					{
						empty = Trace.CorrelationManager.ActivityId;
						flag = true;
						Trace.CorrelationManager.ActivityId = this.eventTraceActivity.ActivityId;
						if (TraceCore.ActionItemCallbackInvokedIsEnabled(Fx.Trace))
						{
							TraceCore.ActionItemCallbackInvoked(Fx.Trace, this.eventTraceActivity);
						}
					}
					this.callback(this.state);
				}
				finally
				{
					if (flag)
					{
						Trace.CorrelationManager.ActivityId = empty;
					}
				}
			}

			// Token: 0x0400020C RID: 524
			[SecurityCritical]
			private Action<object> callback;

			// Token: 0x0400020D RID: 525
			[SecurityCritical]
			private object state;

			// Token: 0x0400020E RID: 526
			private bool flowLegacyActivityId;

			// Token: 0x0400020F RID: 527
			private Guid activityId;

			// Token: 0x04000210 RID: 528
			private EventTraceActivity eventTraceActivity;
		}
	}
}
