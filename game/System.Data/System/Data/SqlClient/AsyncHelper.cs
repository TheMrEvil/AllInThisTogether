using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000233 RID: 563
	internal static class AsyncHelper
	{
		// Token: 0x06001B0D RID: 6925 RVA: 0x0007C634 File Offset: 0x0007A834
		internal static Task CreateContinuationTask(Task task, Action onSuccess, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			if (task == null)
			{
				onSuccess();
				return null;
			}
			TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
			AsyncHelper.ContinueTask(task, completion, delegate
			{
				onSuccess();
				completion.SetResult(null);
			}, connectionToDoom, onFailure, null, null, null);
			return completion.Task;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0007C694 File Offset: 0x0007A894
		internal static Task CreateContinuationTask<T1, T2>(Task task, Action<T1, T2> onSuccess, T1 arg1, T2 arg2, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			return AsyncHelper.CreateContinuationTask(task, delegate()
			{
				onSuccess(arg1, arg2);
			}, connectionToDoom, onFailure);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0007C6D4 File Offset: 0x0007A8D4
		internal static void ContinueTask(Task task, TaskCompletionSource<object> completion, Action onSuccess, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null, Action onCancellation = null, Func<Exception, Exception> exceptionConverter = null, SqlConnection connectionToAbort = null)
		{
			task.ContinueWith(delegate(Task tsk)
			{
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					if (exceptionConverter != null)
					{
						ex = exceptionConverter(ex);
					}
					try
					{
						if (onFailure != null)
						{
							onFailure(ex);
						}
						return;
					}
					finally
					{
						completion.TrySetException(ex);
					}
				}
				if (tsk.IsCanceled)
				{
					try
					{
						if (onCancellation != null)
						{
							onCancellation();
						}
						return;
					}
					finally
					{
						completion.TrySetCanceled();
					}
				}
				try
				{
					onSuccess();
				}
				catch (Exception exception)
				{
					completion.SetException(exception);
				}
			}, TaskScheduler.Default);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0007C728 File Offset: 0x0007A928
		internal static void WaitForCompletion(Task task, int timeout, Action onTimeout = null, bool rethrowExceptions = true)
		{
			try
			{
				task.Wait((timeout > 0) ? (1000 * timeout) : -1);
			}
			catch (AggregateException ex)
			{
				if (rethrowExceptions)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
			}
			if (!task.IsCompleted && onTimeout != null)
			{
				onTimeout();
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0007C784 File Offset: 0x0007A984
		internal static void SetTimeoutException(TaskCompletionSource<object> completion, int timeout, Func<Exception> exc, CancellationToken ctoken)
		{
			if (timeout > 0)
			{
				Task.Delay(timeout * 1000, ctoken).ContinueWith(delegate(Task tsk)
				{
					if (!tsk.IsCanceled && !completion.Task.IsCompleted)
					{
						completion.TrySetException(exc());
					}
				});
			}
		}

		// Token: 0x02000234 RID: 564
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06001B12 RID: 6930 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x0007C7C8 File Offset: 0x0007A9C8
			internal void <CreateContinuationTask>b__0()
			{
				this.onSuccess();
				this.completion.SetResult(null);
			}

			// Token: 0x0400114E RID: 4430
			public Action onSuccess;

			// Token: 0x0400114F RID: 4431
			public TaskCompletionSource<object> completion;
		}

		// Token: 0x02000235 RID: 565
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0<T1, T2>
		{
			// Token: 0x06001B14 RID: 6932 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06001B15 RID: 6933 RVA: 0x0007C7E1 File Offset: 0x0007A9E1
			internal void <CreateContinuationTask>b__0()
			{
				this.onSuccess(this.arg1, this.arg2);
			}

			// Token: 0x04001150 RID: 4432
			public Action<T1, T2> onSuccess;

			// Token: 0x04001151 RID: 4433
			public T1 arg1;

			// Token: 0x04001152 RID: 4434
			public T2 arg2;
		}

		// Token: 0x02000236 RID: 566
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06001B16 RID: 6934 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06001B17 RID: 6935 RVA: 0x0007C7FC File Offset: 0x0007A9FC
			internal void <ContinueTask>b__0(Task tsk)
			{
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					if (this.exceptionConverter != null)
					{
						ex = this.exceptionConverter(ex);
					}
					try
					{
						if (this.onFailure != null)
						{
							this.onFailure(ex);
						}
						return;
					}
					finally
					{
						this.completion.TrySetException(ex);
					}
				}
				if (tsk.IsCanceled)
				{
					try
					{
						if (this.onCancellation != null)
						{
							this.onCancellation();
						}
						return;
					}
					finally
					{
						this.completion.TrySetCanceled();
					}
				}
				try
				{
					this.onSuccess();
				}
				catch (Exception exception)
				{
					this.completion.SetException(exception);
				}
			}

			// Token: 0x04001153 RID: 4435
			public Func<Exception, Exception> exceptionConverter;

			// Token: 0x04001154 RID: 4436
			public Action<Exception> onFailure;

			// Token: 0x04001155 RID: 4437
			public TaskCompletionSource<object> completion;

			// Token: 0x04001156 RID: 4438
			public Action onCancellation;

			// Token: 0x04001157 RID: 4439
			public Action onSuccess;
		}

		// Token: 0x02000237 RID: 567
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06001B18 RID: 6936 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06001B19 RID: 6937 RVA: 0x0007C8C8 File Offset: 0x0007AAC8
			internal void <SetTimeoutException>b__0(Task tsk)
			{
				if (!tsk.IsCanceled && !this.completion.Task.IsCompleted)
				{
					this.completion.TrySetException(this.exc());
				}
			}

			// Token: 0x04001158 RID: 4440
			public TaskCompletionSource<object> completion;

			// Token: 0x04001159 RID: 4441
			public Func<Exception> exc;
		}
	}
}
