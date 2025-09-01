using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200003E RID: 62
	internal static class TaskToApm
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00009BF4 File Offset: 0x00007DF4
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

		// Token: 0x060001D6 RID: 470 RVA: 0x00009C44 File Offset: 0x00007E44
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

		// Token: 0x060001D7 RID: 471 RVA: 0x00009C84 File Offset: 0x00007E84
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

		// Token: 0x060001D8 RID: 472 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x0200003F RID: 63
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00009D0C File Offset: 0x00007F0C
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060001DA RID: 474 RVA: 0x00009D29 File Offset: 0x00007F29
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060001DB RID: 475 RVA: 0x00009D31 File Offset: 0x00007F31
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001DC RID: 476 RVA: 0x00009D39 File Offset: 0x00007F39
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001DD RID: 477 RVA: 0x00009D46 File Offset: 0x00007F46
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x0400021D RID: 541
			internal readonly Task Task;

			// Token: 0x0400021E RID: 542
			private readonly object _state;

			// Token: 0x0400021F RID: 543
			private readonly bool _completedSynchronously;
		}

		// Token: 0x02000040 RID: 64
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060001DE RID: 478 RVA: 0x0000353B File Offset: 0x0000173B
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060001DF RID: 479 RVA: 0x00009D53 File Offset: 0x00007F53
			internal void <InvokeCallbackWhenTaskCompletes>b__0()
			{
				this.callback(this.asyncResult);
			}

			// Token: 0x04000220 RID: 544
			public AsyncCallback callback;

			// Token: 0x04000221 RID: 545
			public IAsyncResult asyncResult;
		}
	}
}
