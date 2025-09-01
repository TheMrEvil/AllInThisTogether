using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200036F RID: 879
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06002483 RID: 9347 RVA: 0x00082C4B File Offset: 0x00080E4B
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext) : base(action, flowExecutionContext)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00082C5C File Offset: 0x00080E5C
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.Current)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00082C98 File Offset: 0x00080E98
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00082CC4 File Offset: 0x00080EC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00082CEE File Offset: 0x00080EEE
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextAwaitTaskContinuation()
		{
		}

		// Token: 0x04001D35 RID: 7477
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001D36 RID: 7478
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001D37 RID: 7479
		private readonly SynchronizationContext m_syncContext;

		// Token: 0x02000370 RID: 880
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002488 RID: 9352 RVA: 0x00082D05 File Offset: 0x00080F05
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002489 RID: 9353 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x0600248A RID: 9354 RVA: 0x0006ED00 File Offset: 0x0006CF00
			internal void <.cctor>b__7_0(object state)
			{
				((Action)state)();
			}

			// Token: 0x04001D38 RID: 7480
			public static readonly SynchronizationContextAwaitTaskContinuation.<>c <>9 = new SynchronizationContextAwaitTaskContinuation.<>c();
		}
	}
}
