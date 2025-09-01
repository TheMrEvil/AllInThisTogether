using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000D RID: 13
	internal abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000022DE File Offset: 0x000004DE
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.thisLock = new object();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022FF File Offset: 0x000004FF
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002308 File Offset: 0x00000508
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.manualResetEvent != null)
				{
					return this.manualResetEvent;
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.manualResetEvent == null)
					{
						this.manualResetEvent = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.manualResetEvent;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002370 File Offset: 0x00000570
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002378 File Offset: 0x00000578
		public bool HasCallback
		{
			get
			{
				return this.callback != null;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002383 File Offset: 0x00000583
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002393 File Offset: 0x00000593
		protected Action<AsyncResult, Exception> OnCompleting
		{
			[CompilerGenerated]
			get
			{
				return this.<OnCompleting>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OnCompleting>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000239C File Offset: 0x0000059C
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000023A4 File Offset: 0x000005A4
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000023AC File Offset: 0x000005AC
		protected Action<AsyncCallback, IAsyncResult> VirtualCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<VirtualCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<VirtualCallback>k__BackingField = value;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000023B8 File Offset: 0x000005B8
		protected void Complete(bool completedSynchronously)
		{
			if (this.isCompleted)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncResultCompletedTwice(base.GetType())));
			}
			this.completedSynchronously = completedSynchronously;
			if (this.OnCompleting != null)
			{
				try
				{
					this.OnCompleting(this, this.exception);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.exception = ex;
				}
			}
			if (completedSynchronously)
			{
				this.isCompleted = true;
			}
			else
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.isCompleted = true;
					if (this.manualResetEvent != null)
					{
						this.manualResetEvent.Set();
					}
				}
			}
			if (this.callback != null)
			{
				try
				{
					if (this.VirtualCallback != null)
					{
						this.VirtualCallback(this.callback, this);
					}
					else
					{
						this.callback(this);
					}
				}
				catch (Exception innerException)
				{
					if (Fx.IsFatal(innerException))
					{
						throw;
					}
					throw Fx.Exception.AsError(new CallbackException("Async Callback Threw Exception", innerException));
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024E0 File Offset: 0x000006E0
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024F0 File Offset: 0x000006F0
		private static void AsyncCompletionWrapperCallback(IAsyncResult result)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Invalid Null Async Result"));
			}
			if (result.CompletedSynchronously)
			{
				return;
			}
			AsyncResult asyncResult = (AsyncResult)result.AsyncState;
			if (!asyncResult.OnContinueAsyncCompletion(result))
			{
				return;
			}
			AsyncResult.AsyncCompletion nextCompletion = asyncResult.GetNextCompletion();
			if (nextCompletion == null)
			{
				AsyncResult.ThrowInvalidAsyncResult(result);
			}
			bool flag = false;
			Exception ex = null;
			try
			{
				flag = nextCompletion(result);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				flag = true;
				ex = ex2;
			}
			if (flag)
			{
				asyncResult.Complete(false, ex);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000257C File Offset: 0x0000077C
		protected virtual bool OnContinueAsyncCompletion(IAsyncResult result)
		{
			return true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000257F File Offset: 0x0000077F
		protected void SetBeforePrepareAsyncCompletionAction(Action beforePrepareAsyncCompletionAction)
		{
			this.beforePrepareAsyncCompletionAction = beforePrepareAsyncCompletionAction;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002588 File Offset: 0x00000788
		protected void SetCheckSyncValidationFunc(Func<IAsyncResult, bool> checkSyncValidationFunc)
		{
			this.checkSyncValidationFunc = checkSyncValidationFunc;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002591 File Offset: 0x00000791
		protected AsyncCallback PrepareAsyncCompletion(AsyncResult.AsyncCompletion callback)
		{
			if (this.beforePrepareAsyncCompletionAction != null)
			{
				this.beforePrepareAsyncCompletionAction();
			}
			this.nextAsyncCompletion = callback;
			if (AsyncResult.asyncCompletionWrapperCallback == null)
			{
				AsyncResult.asyncCompletionWrapperCallback = Fx.ThunkCallback(new AsyncCallback(AsyncResult.AsyncCompletionWrapperCallback));
			}
			return AsyncResult.asyncCompletionWrapperCallback;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025D0 File Offset: 0x000007D0
		protected bool CheckSyncContinue(IAsyncResult result)
		{
			AsyncResult.AsyncCompletion asyncCompletion;
			return this.TryContinueHelper(result, out asyncCompletion);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025E8 File Offset: 0x000007E8
		protected bool SyncContinue(IAsyncResult result)
		{
			AsyncResult.AsyncCompletion asyncCompletion;
			return this.TryContinueHelper(result, out asyncCompletion) && asyncCompletion(result);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000260C File Offset: 0x0000080C
		private bool TryContinueHelper(IAsyncResult result, out AsyncResult.AsyncCompletion callback)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Invalid Null Async Result"));
			}
			callback = null;
			if (this.checkSyncValidationFunc != null)
			{
				if (!this.checkSyncValidationFunc(result))
				{
					return false;
				}
			}
			else if (!result.CompletedSynchronously)
			{
				return false;
			}
			callback = this.GetNextCompletion();
			if (callback == null)
			{
				AsyncResult.ThrowInvalidAsyncResult("Only call Check/SyncContinue once per async operation (once per PrepareAsyncCompletion).");
			}
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000266D File Offset: 0x0000086D
		private AsyncResult.AsyncCompletion GetNextCompletion()
		{
			AsyncResult.AsyncCompletion result = this.nextAsyncCompletion;
			this.nextAsyncCompletion = null;
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000267C File Offset: 0x0000087C
		protected static void ThrowInvalidAsyncResult(IAsyncResult result)
		{
			throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidAsyncResultImplementation(result.GetType())));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002698 File Offset: 0x00000898
		protected static void ThrowInvalidAsyncResult(string debugText)
		{
			string message = "Invalid Async Result Implementation Generic";
			throw Fx.Exception.AsError(new InvalidOperationException(message));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026C0 File Offset: 0x000008C0
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
		{
			if (result == null)
			{
				throw Fx.Exception.ArgumentNull("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw Fx.Exception.Argument("result", "Invalid Async Result");
			}
			if (tasyncResult.endCalled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Async Result Already Ended"));
			}
			tasyncResult.endCalled = true;
			if (!tasyncResult.isCompleted)
			{
				tasyncResult.AsyncWaitHandle.WaitOne();
			}
			if (tasyncResult.manualResetEvent != null)
			{
				tasyncResult.manualResetEvent.Close();
			}
			if (tasyncResult.exception != null)
			{
				throw Fx.Exception.AsError(tasyncResult.exception);
			}
			return tasyncResult;
		}

		// Token: 0x04000044 RID: 68
		private static AsyncCallback asyncCompletionWrapperCallback;

		// Token: 0x04000045 RID: 69
		private AsyncCallback callback;

		// Token: 0x04000046 RID: 70
		private bool completedSynchronously;

		// Token: 0x04000047 RID: 71
		private bool endCalled;

		// Token: 0x04000048 RID: 72
		private Exception exception;

		// Token: 0x04000049 RID: 73
		private bool isCompleted;

		// Token: 0x0400004A RID: 74
		private AsyncResult.AsyncCompletion nextAsyncCompletion;

		// Token: 0x0400004B RID: 75
		private object state;

		// Token: 0x0400004C RID: 76
		private Action beforePrepareAsyncCompletionAction;

		// Token: 0x0400004D RID: 77
		private Func<IAsyncResult, bool> checkSyncValidationFunc;

		// Token: 0x0400004E RID: 78
		private ManualResetEvent manualResetEvent;

		// Token: 0x0400004F RID: 79
		private object thisLock;

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private Action<AsyncResult, Exception> <OnCompleting>k__BackingField;

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private Action<AsyncCallback, IAsyncResult> <VirtualCallback>k__BackingField;

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x06000353 RID: 851
		protected delegate bool AsyncCompletion(IAsyncResult result);
	}
}
