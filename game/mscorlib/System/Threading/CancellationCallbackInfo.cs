using System;

namespace System.Threading
{
	// Token: 0x020002AE RID: 686
	internal class CancellationCallbackInfo
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x000701B8 File Offset: 0x0006E3B8
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000701E0 File Offset: 0x0006E3E0
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00070228 File Offset: 0x0006E428
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04001A90 RID: 6800
		internal readonly Action<object> Callback;

		// Token: 0x04001A91 RID: 6801
		internal readonly object StateForCallback;

		// Token: 0x04001A92 RID: 6802
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04001A93 RID: 6803
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04001A94 RID: 6804
		private static ContextCallback s_executionContextCallback;

		// Token: 0x020002AF RID: 687
		internal sealed class WithSyncContext : CancellationCallbackInfo
		{
			// Token: 0x06001E42 RID: 7746 RVA: 0x0007024D File Offset: 0x0006E44D
			internal WithSyncContext(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource, SynchronizationContext targetSyncContext) : base(callback, stateForCallback, targetExecutionContext, cancellationTokenSource)
			{
				this.TargetSyncContext = targetSyncContext;
			}

			// Token: 0x04001A95 RID: 6805
			internal readonly SynchronizationContext TargetSyncContext;
		}
	}
}
