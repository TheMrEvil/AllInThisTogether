using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200080E RID: 2062
	public readonly struct ValueTaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06004634 RID: 17972 RVA: 0x000E5AD5 File Offset: 0x000E3CD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x000E5ADE File Offset: 0x000E3CDE
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x000E5AEB File Offset: 0x000E3CEB
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TResult GetResult()
		{
			return this._value.Result;
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x000E5AF8 File Offset: 0x000E3CF8
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000E5B60 File Offset: 0x000E3D60
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x04002D48 RID: 11592
		private readonly ValueTask<TResult> _value;
	}
}
