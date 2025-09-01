using System;

namespace System.Runtime
{
	// Token: 0x02000009 RID: 9
	internal abstract class AsyncEventArgs : IAsyncEventArgs
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021C0 File Offset: 0x000003C0
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021C8 File Offset: 0x000003C8
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021D0 File Offset: 0x000003D0
		private AsyncEventArgs.OperationState State
		{
			set
			{
				if (value != AsyncEventArgs.OperationState.PendingCompletion)
				{
					if (value - AsyncEventArgs.OperationState.CompletedSynchronously <= 1)
					{
						if (this.state != AsyncEventArgs.OperationState.PendingCompletion)
						{
							throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletedTwice(base.GetType())));
						}
					}
				}
				else if (this.state == AsyncEventArgs.OperationState.PendingCompletion)
				{
					throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletionPending(base.GetType())));
				}
				this.state = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002238 File Offset: 0x00000438
		public void Complete(bool completedSynchronously)
		{
			this.Complete(completedSynchronously, null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002242 File Offset: 0x00000442
		public virtual void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			if (completedSynchronously)
			{
				this.State = AsyncEventArgs.OperationState.CompletedSynchronously;
				return;
			}
			this.State = AsyncEventArgs.OperationState.CompletedAsynchronously;
			this.callback(this);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002269 File Offset: 0x00000469
		protected void SetAsyncState(AsyncEventArgsCallback callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			this.State = AsyncEventArgs.OperationState.PendingCompletion;
			this.asyncState = state;
			this.callback = callback;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002293 File Offset: 0x00000493
		protected AsyncEventArgs()
		{
		}

		// Token: 0x0400003E RID: 62
		private AsyncEventArgs.OperationState state;

		// Token: 0x0400003F RID: 63
		private object asyncState;

		// Token: 0x04000040 RID: 64
		private AsyncEventArgsCallback callback;

		// Token: 0x04000041 RID: 65
		private Exception exception;

		// Token: 0x0200005B RID: 91
		private enum OperationState
		{
			// Token: 0x04000212 RID: 530
			Created,
			// Token: 0x04000213 RID: 531
			PendingCompletion,
			// Token: 0x04000214 RID: 532
			CompletedSynchronously,
			// Token: 0x04000215 RID: 533
			CompletedAsynchronously
		}
	}
}
