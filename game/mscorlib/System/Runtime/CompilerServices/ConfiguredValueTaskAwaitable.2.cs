using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E9 RID: 2025
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable<TResult>
	{
		// Token: 0x060045EA RID: 17898 RVA: 0x000E55D1 File Offset: 0x000E37D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000E55DA File Offset: 0x000E37DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter(this._value);
		}

		// Token: 0x04002D34 RID: 11572
		private readonly ValueTask<TResult> _value;

		// Token: 0x020007EA RID: 2026
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060045EC RID: 17900 RVA: 0x000E55E7 File Offset: 0x000E37E7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(ValueTask<TResult> value)
			{
				this._value = value;
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x060045ED RID: 17901 RVA: 0x000E55F0 File Offset: 0x000E37F0
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x060045EE RID: 17902 RVA: 0x000E55FD File Offset: 0x000E37FD
			[StackTraceHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TResult GetResult()
			{
				return this._value.Result;
			}

			// Token: 0x060045EF RID: 17903 RVA: 0x000E560C File Offset: 0x000E380C
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x060045F0 RID: 17904 RVA: 0x000E56B0 File Offset: 0x000E38B0
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x04002D35 RID: 11573
			private readonly ValueTask<TResult> _value;
		}
	}
}
