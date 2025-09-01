using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000081 RID: 129
	internal static class TaskToApm
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00017F34 File Offset: 0x00016134
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

		// Token: 0x06000626 RID: 1574 RVA: 0x00017F84 File Offset: 0x00016184
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

		// Token: 0x06000627 RID: 1575 RVA: 0x00017FC4 File Offset: 0x000161C4
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

		// Token: 0x06000628 RID: 1576 RVA: 0x00018008 File Offset: 0x00016208
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000082 RID: 130
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06000629 RID: 1577 RVA: 0x0001804C File Offset: 0x0001624C
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x00018069 File Offset: 0x00016269
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600062B RID: 1579 RVA: 0x00018071 File Offset: 0x00016271
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600062C RID: 1580 RVA: 0x00018079 File Offset: 0x00016279
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600062D RID: 1581 RVA: 0x00018086 File Offset: 0x00016286
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x0400066B RID: 1643
			internal readonly Task Task;

			// Token: 0x0400066C RID: 1644
			private readonly object _state;

			// Token: 0x0400066D RID: 1645
			private readonly bool _completedSynchronously;
		}

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600062E RID: 1582 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x00018093 File Offset: 0x00016293
			internal void <InvokeCallbackWhenTaskCompletes>b__0()
			{
				this.callback(this.asyncResult);
			}

			// Token: 0x0400066E RID: 1646
			public AsyncCallback callback;

			// Token: 0x0400066F RID: 1647
			public IAsyncResult asyncResult;
		}
	}
}
