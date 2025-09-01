using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x0200030F RID: 783
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask : IEquatable<ValueTask>
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00078842 File Offset: 0x00076A42
		internal static Task CompletedTask
		{
			get
			{
				return Task.CompletedTask;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00078849 File Offset: 0x00076A49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0007886A File Offset: 0x00076A6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._continueOnCapturedContext = true;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0007888B File Offset: 0x00076A8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000788A2 File Offset: 0x00076AA2
		public override int GetHashCode()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000788B5 File Offset: 0x00076AB5
		public override bool Equals(object obj)
		{
			return obj is ValueTask && this.Equals((ValueTask)obj);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000788CD File Offset: 0x00076ACD
		public bool Equals(ValueTask other)
		{
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000788ED File Offset: 0x00076AED
		public static bool operator ==(ValueTask left, ValueTask right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000788F7 File Offset: 0x00076AF7
		public static bool operator !=(ValueTask left, ValueTask right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00078904 File Offset: 0x00076B04
		public Task AsTask()
		{
			object obj = this._obj;
			Task result;
			if (obj != null)
			{
				if ((result = (obj as Task)) == null)
				{
					return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource>(obj));
				}
			}
			else
			{
				result = ValueTask.CompletedTask;
			}
			return result;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00078937 File Offset: 0x00076B37
		public ValueTask Preserve()
		{
			if (this._obj != null)
			{
				return new ValueTask(this.AsTask());
			}
			return this;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00078954 File Offset: 0x00076B54
		private Task GetTaskForValueTaskSource(IValueTaskSource t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					t.GetResult(this._token);
					return ValueTask.CompletedTask;
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task<VoidTaskResult> task = new Task<VoidTaskResult>();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					return ValueTask.s_canceledTask;
				}
			}
			return new ValueTask.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x000789DC File Offset: 0x00076BDC
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x00078A1C File Offset: 0x00076C1C
		public bool IsCompletedSuccessfully
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00078A5C File Offset: 0x00076C5C
		public bool IsFaulted
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsFaulted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Faulted;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x00078A9C File Offset: 0x00076C9C
		public bool IsCanceled
		{
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return false;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCanceled;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Canceled;
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00078ADC File Offset: 0x00076CDC
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ThrowIfCompletedUnsuccessfully()
		{
			object obj = this._obj;
			if (obj != null)
			{
				Task task = obj as Task;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return;
				}
				Unsafe.As<IValueTaskSource>(obj).GetResult(this._token);
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00078B15 File Offset: 0x00076D15
		public ValueTaskAwaiter GetAwaiter()
		{
			return new ValueTaskAwaiter(this);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00078B22 File Offset: 0x00076D22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable(new ValueTask(this._obj, this._token, continueOnCapturedContext));
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00078B3B File Offset: 0x00076D3B
		// Note: this type is marked as 'beforefieldinit'.
		static ValueTask()
		{
		}

		// Token: 0x04001BCE RID: 7118
		private static readonly Task s_canceledTask = Task.FromCanceled(new CancellationToken(true));

		// Token: 0x04001BCF RID: 7119
		internal readonly object _obj;

		// Token: 0x04001BD0 RID: 7120
		internal readonly short _token;

		// Token: 0x04001BD1 RID: 7121
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x02000310 RID: 784
		private sealed class ValueTaskSourceAsTask : Task<VoidTaskResult>
		{
			// Token: 0x060021A1 RID: 8609 RVA: 0x00078B4D File Offset: 0x00076D4D
			public ValueTaskSourceAsTask(IValueTaskSource source, short token)
			{
				this._token = token;
				this._source = source;
				source.OnCompleted(ValueTask.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x060021A2 RID: 8610 RVA: 0x00078B71 File Offset: 0x00076D71
			// Note: this type is marked as 'beforefieldinit'.
			static ValueTaskSourceAsTask()
			{
			}

			// Token: 0x04001BD2 RID: 7122
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							source.GetResult(valueTaskSourceAsTask._token);
							valueTaskSourceAsTask.TrySetResult(default(VoidTaskResult));
						}
						catch (Exception ex)
						{
							if (status == ValueTaskSourceStatus.Canceled)
							{
								OperationCanceledException ex2 = ex as OperationCanceledException;
								if (ex2 != null)
								{
									valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
								}
								else
								{
									valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
								}
							}
							else
							{
								valueTaskSourceAsTask.TrySetException(ex);
							}
						}
						return;
					}
				}
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
			};

			// Token: 0x04001BD3 RID: 7123
			private IValueTaskSource _source;

			// Token: 0x04001BD4 RID: 7124
			private readonly short _token;

			// Token: 0x02000311 RID: 785
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x060021A3 RID: 8611 RVA: 0x00078B88 File Offset: 0x00076D88
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x060021A4 RID: 8612 RVA: 0x0000259F File Offset: 0x0000079F
				public <>c()
				{
				}

				// Token: 0x060021A5 RID: 8613 RVA: 0x00078B94 File Offset: 0x00076D94
				internal void <.cctor>b__4_0(object state)
				{
					ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
					if (valueTaskSourceAsTask != null)
					{
						IValueTaskSource source = valueTaskSourceAsTask._source;
						if (source != null)
						{
							valueTaskSourceAsTask._source = null;
							ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
							try
							{
								source.GetResult(valueTaskSourceAsTask._token);
								valueTaskSourceAsTask.TrySetResult(default(VoidTaskResult));
							}
							catch (Exception ex)
							{
								if (status == ValueTaskSourceStatus.Canceled)
								{
									OperationCanceledException ex2 = ex as OperationCanceledException;
									if (ex2 != null)
									{
										valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
									}
									else
									{
										valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
									}
								}
								else
								{
									valueTaskSourceAsTask.TrySetException(ex);
								}
							}
							return;
						}
					}
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				}

				// Token: 0x04001BD5 RID: 7125
				public static readonly ValueTask.ValueTaskSourceAsTask.<>c <>9 = new ValueTask.ValueTaskSourceAsTask.<>c();
			}
		}
	}
}
