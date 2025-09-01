using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200030C RID: 780
	internal static class TaskToApm
	{
		// Token: 0x06002182 RID: 8578 RVA: 0x000786D0 File Offset: 0x000768D0
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00078720 File Offset: 0x00076920
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = (asyncResult as Task);
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00078760 File Offset: 0x00076960
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = (taskWrapperAsyncResult.Task as Task<TResult>);
			}
			else
			{
				task = (asyncResult as Task<TResult>);
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000787A4 File Offset: 0x000769A4
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x0200030D RID: 781
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06002186 RID: 8582 RVA: 0x000787E8 File Offset: 0x000769E8
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x06002187 RID: 8583 RVA: 0x00078805 File Offset: 0x00076A05
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007880D File Offset: 0x00076A0D
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06002189 RID: 8585 RVA: 0x00078815 File Offset: 0x00076A15
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x0600218A RID: 8586 RVA: 0x00078822 File Offset: 0x00076A22
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04001BC9 RID: 7113
			internal readonly Task Task;

			// Token: 0x04001BCA RID: 7114
			private readonly object _state;

			// Token: 0x04001BCB RID: 7115
			private readonly bool _completedSynchronously;
		}

		// Token: 0x0200030E RID: 782
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600218B RID: 8587 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600218C RID: 8588 RVA: 0x0007882F File Offset: 0x00076A2F
			internal void <InvokeCallbackWhenTaskCompletes>b__0()
			{
				this.callback(this.asyncResult);
			}

			// Token: 0x04001BCC RID: 7116
			public AsyncCallback callback;

			// Token: 0x04001BCD RID: 7117
			public IAsyncResult asyncResult;
		}
	}
}
