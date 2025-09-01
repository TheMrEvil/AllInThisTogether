using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006B8 RID: 1720
	internal class WebCompletionSource<T>
	{
		// Token: 0x0600375B RID: 14171 RVA: 0x000C25C4 File Offset: 0x000C07C4
		public WebCompletionSource(bool runAsync = true)
		{
			this.completion = new TaskCompletionSource<WebCompletionSource<T>.Result>(runAsync ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x000C25DF File Offset: 0x000C07DF
		internal WebCompletionSource<T>.Result CurrentResult
		{
			get
			{
				return this.currentResult;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000C25E7 File Offset: 0x000C07E7
		internal WebCompletionSource<T>.Status CurrentStatus
		{
			get
			{
				WebCompletionSource<T>.Result result = this.currentResult;
				if (result == null)
				{
					return WebCompletionSource<T>.Status.Running;
				}
				return result.Status;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000C25FA File Offset: 0x000C07FA
		internal Task Task
		{
			get
			{
				return this.completion.Task;
			}
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000C2608 File Offset: 0x000C0808
		public bool TrySetCompleted(T argument)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(argument);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000C263C File Offset: 0x000C083C
		public bool TrySetCompleted()
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Completed, null);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000C266E File Offset: 0x000C086E
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(new OperationCanceledException());
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000C267C File Offset: 0x000C087C
		public bool TrySetCanceled(OperationCanceledException error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Canceled, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000C26B4 File Offset: 0x000C08B4
		public bool TrySetException(Exception error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Faulted, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000C26EB File Offset: 0x000C08EB
		public void ThrowOnError()
		{
			if (!this.completion.Task.IsCompleted)
			{
				return;
			}
			ExceptionDispatchInfo error = this.completion.Task.Result.Error;
			if (error == null)
			{
				return;
			}
			error.Throw();
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000C2720 File Offset: 0x000C0920
		public Task<T> WaitForCompletion()
		{
			WebCompletionSource<T>.<WaitForCompletion>d__15 <WaitForCompletion>d__;
			<WaitForCompletion>d__.<>4__this = this;
			<WaitForCompletion>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<WaitForCompletion>d__.<>1__state = -1;
			<WaitForCompletion>d__.<>t__builder.Start<WebCompletionSource<T>.<WaitForCompletion>d__15>(ref <WaitForCompletion>d__);
			return <WaitForCompletion>d__.<>t__builder.Task;
		}

		// Token: 0x0400203D RID: 8253
		private TaskCompletionSource<WebCompletionSource<T>.Result> completion;

		// Token: 0x0400203E RID: 8254
		private WebCompletionSource<T>.Result currentResult;

		// Token: 0x020006B9 RID: 1721
		internal enum Status
		{
			// Token: 0x04002040 RID: 8256
			Running,
			// Token: 0x04002041 RID: 8257
			Completed,
			// Token: 0x04002042 RID: 8258
			Canceled,
			// Token: 0x04002043 RID: 8259
			Faulted
		}

		// Token: 0x020006BA RID: 1722
		internal class Result
		{
			// Token: 0x17000B8E RID: 2958
			// (get) Token: 0x06003766 RID: 14182 RVA: 0x000C2763 File Offset: 0x000C0963
			public WebCompletionSource<T>.Status Status
			{
				[CompilerGenerated]
				get
				{
					return this.<Status>k__BackingField;
				}
			}

			// Token: 0x17000B8F RID: 2959
			// (get) Token: 0x06003767 RID: 14183 RVA: 0x000C276B File Offset: 0x000C096B
			public bool Success
			{
				get
				{
					return this.Status == WebCompletionSource<T>.Status.Completed;
				}
			}

			// Token: 0x17000B90 RID: 2960
			// (get) Token: 0x06003768 RID: 14184 RVA: 0x000C2776 File Offset: 0x000C0976
			public ExceptionDispatchInfo Error
			{
				[CompilerGenerated]
				get
				{
					return this.<Error>k__BackingField;
				}
			}

			// Token: 0x17000B91 RID: 2961
			// (get) Token: 0x06003769 RID: 14185 RVA: 0x000C277E File Offset: 0x000C097E
			public T Argument
			{
				[CompilerGenerated]
				get
				{
					return this.<Argument>k__BackingField;
				}
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x000C2786 File Offset: 0x000C0986
			public Result(T argument)
			{
				this.Status = 1;
				this.Argument = argument;
			}

			// Token: 0x0600376B RID: 14187 RVA: 0x000C279C File Offset: 0x000C099C
			public Result(WebCompletionSource<T>.Status state, ExceptionDispatchInfo error)
			{
				this.Status = state;
				this.Error = error;
			}

			// Token: 0x04002044 RID: 8260
			[CompilerGenerated]
			private readonly WebCompletionSource<T>.Status <Status>k__BackingField;

			// Token: 0x04002045 RID: 8261
			[CompilerGenerated]
			private readonly ExceptionDispatchInfo <Error>k__BackingField;

			// Token: 0x04002046 RID: 8262
			[CompilerGenerated]
			private readonly T <Argument>k__BackingField;
		}

		// Token: 0x020006BB RID: 1723
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForCompletion>d__15 : IAsyncStateMachine
		{
			// Token: 0x0600376C RID: 14188 RVA: 0x000C27B4 File Offset: 0x000C09B4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebCompletionSource<T> webCompletionSource = this.<>4__this;
				T argument;
				try
				{
					ConfiguredTaskAwaitable<WebCompletionSource<T>.Result>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = webCompletionSource.completion.Task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebCompletionSource<T>.Result>.ConfiguredTaskAwaiter, WebCompletionSource<T>.<WaitForCompletion>d__15>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<WebCompletionSource<T>.Result>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					WebCompletionSource<T>.Result result = awaiter.GetResult();
					if (result.Status != WebCompletionSource<T>.Status.Completed)
					{
						result.Error.Throw();
						throw new InvalidOperationException("Should never happen.");
					}
					argument = result.Argument;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(argument);
			}

			// Token: 0x0600376D RID: 14189 RVA: 0x000C28A4 File Offset: 0x000C0AA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002047 RID: 8263
			public int <>1__state;

			// Token: 0x04002048 RID: 8264
			public AsyncTaskMethodBuilder<T> <>t__builder;

			// Token: 0x04002049 RID: 8265
			public WebCompletionSource<T> <>4__this;

			// Token: 0x0400204A RID: 8266
			private ConfiguredTaskAwaitable<WebCompletionSource<T>.Result>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
