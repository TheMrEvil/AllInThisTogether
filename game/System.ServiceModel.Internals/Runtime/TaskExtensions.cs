using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Runtime
{
	// Token: 0x0200002D RID: 45
	internal static class TaskExtensions
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static IAsyncResult AsAsyncResult<T>(this Task<T> task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw Fx.Exception.ArgumentNull("task");
			}
			if (task.Status == TaskStatus.Created)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("SFx Task Not Started"));
			}
			TaskCompletionSource<T> tcs = new TaskCompletionSource<T>(state);
			task.ContinueWith(delegate(Task<T> t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(t.Result);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005E78 File Offset: 0x00004078
		public static IAsyncResult AsAsyncResult(this Task task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw Fx.Exception.ArgumentNull("task");
			}
			if (task.Status == TaskStatus.Created)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("SFx Task Not Started"));
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(state);
			task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(null);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005EF1 File Offset: 0x000040F1
		public static ConfiguredTaskAwaitable SuppressContextFlow(this Task task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005EFA File Offset: 0x000040FA
		public static ConfiguredTaskAwaitable<T> SuppressContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005F03 File Offset: 0x00004103
		public static ConfiguredTaskAwaitable ContinueOnCapturedContextFlow(this Task task)
		{
			return task.ConfigureAwait(true);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005F0C File Offset: 0x0000410C
		public static ConfiguredTaskAwaitable<T> ContinueOnCapturedContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(true);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005F18 File Offset: 0x00004118
		public static void Wait<TException>(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005F4C File Offset: 0x0000414C
		public static bool Wait<TException>(this Task task, int millisecondsTimeout)
		{
			bool result;
			try
			{
				result = task.Wait(millisecondsTimeout);
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005F84 File Offset: 0x00004184
		public static bool Wait<TException>(this Task task, TimeSpan timeout)
		{
			bool result;
			try
			{
				if (timeout == TimeSpan.MaxValue)
				{
					result = task.Wait(-1);
				}
				else
				{
					result = task.Wait(timeout);
				}
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005FD0 File Offset: 0x000041D0
		public static void Wait(this Task task, TimeSpan timeout, Action<Exception, TimeSpan, string> exceptionConverter, string operationType)
		{
			bool flag = false;
			try
			{
				if (timeout > TimeoutHelper.MaxWait)
				{
					task.Wait();
				}
				else
				{
					flag = !task.Wait(timeout);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex) || exceptionConverter == null)
				{
					throw;
				}
				exceptionConverter(ex, timeout, operationType);
			}
			if (flag)
			{
				throw Fx.Exception.AsError(new TimeoutException(InternalSR.TaskTimedOutError(timeout)));
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006048 File Offset: 0x00004248
		public static Task<TBase> Upcast<TDerived, TBase>(this Task<TDerived> task) where TDerived : TBase
		{
			if (task.Status != TaskStatus.RanToCompletion)
			{
				return task.UpcastPrivate<TDerived, TBase>();
			}
			return Task.FromResult<TBase>((TBase)((object)task.Result));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006070 File Offset: 0x00004270
		private static Task<TBase> UpcastPrivate<TDerived, TBase>(this Task<TDerived> task) where TDerived : TBase
		{
			TaskExtensions.<UpcastPrivate>d__11<TDerived, TBase> <UpcastPrivate>d__;
			<UpcastPrivate>d__.task = task;
			<UpcastPrivate>d__.<>t__builder = AsyncTaskMethodBuilder<TBase>.Create();
			<UpcastPrivate>d__.<>1__state = -1;
			<UpcastPrivate>d__.<>t__builder.Start<TaskExtensions.<UpcastPrivate>d__11<TDerived, TBase>>(ref <UpcastPrivate>d__);
			return <UpcastPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0<T>
		{
			// Token: 0x060003E9 RID: 1001 RVA: 0x00012858 File Offset: 0x00010A58
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00012860 File Offset: 0x00010A60
			internal void <AsAsyncResult>b__0(Task<T> t)
			{
				if (t.IsFaulted)
				{
					this.tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					this.tcs.TrySetCanceled();
				}
				else
				{
					this.tcs.TrySetResult(t.Result);
				}
				if (this.callback != null)
				{
					this.callback(this.tcs.Task);
				}
			}

			// Token: 0x040002C2 RID: 706
			public TaskCompletionSource<T> tcs;

			// Token: 0x040002C3 RID: 707
			public AsyncCallback callback;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x060003EB RID: 1003 RVA: 0x000128D4 File Offset: 0x00010AD4
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x000128DC File Offset: 0x00010ADC
			internal void <AsAsyncResult>b__0(Task t)
			{
				if (t.IsFaulted)
				{
					this.tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					this.tcs.TrySetCanceled();
				}
				else
				{
					this.tcs.TrySetResult(null);
				}
				if (this.callback != null)
				{
					this.callback(this.tcs.Task);
				}
			}

			// Token: 0x040002C4 RID: 708
			public TaskCompletionSource<object> tcs;

			// Token: 0x040002C5 RID: 709
			public AsyncCallback callback;
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <UpcastPrivate>d__11<TDerived, TBase> : IAsyncStateMachine where TDerived : TBase
		{
			// Token: 0x060003ED RID: 1005 RVA: 0x0001294C File Offset: 0x00010B4C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				TBase result;
				try
				{
					ConfiguredTaskAwaitable<TDerived>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<TDerived>.ConfiguredTaskAwaiter, TaskExtensions.<UpcastPrivate>d__11<TDerived, TBase>>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<TDerived>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					result = (TBase)((object)awaiter.GetResult());
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x00012A10 File Offset: 0x00010C10
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002C6 RID: 710
			public int <>1__state;

			// Token: 0x040002C7 RID: 711
			public AsyncTaskMethodBuilder<TBase> <>t__builder;

			// Token: 0x040002C8 RID: 712
			public Task<TDerived> task;

			// Token: 0x040002C9 RID: 713
			private ConfiguredTaskAwaitable<TDerived>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
