using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200080C RID: 2060
	public readonly struct ValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600462B RID: 17963 RVA: 0x000E5998 File Offset: 0x000E3B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x000E59A1 File Offset: 0x000E3BA1
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000E59AE File Offset: 0x000E3BAE
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			this._value.ThrowIfCompletedUnsuccessfully();
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000E59BC File Offset: 0x000E3BBC
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000E5A24 File Offset: 0x000E3C24
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000E5A8B File Offset: 0x000E3C8B
		// Note: this type is marked as 'beforefieldinit'.
		static ValueTaskAwaiter()
		{
		}

		// Token: 0x04002D45 RID: 11589
		internal static readonly Action<object> s_invokeActionDelegate = delegate(object state)
		{
			Action action = state as Action;
			if (action == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			action();
		};

		// Token: 0x04002D46 RID: 11590
		private readonly ValueTask _value;

		// Token: 0x0200080D RID: 2061
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004631 RID: 17969 RVA: 0x000E5AA2 File Offset: 0x000E3CA2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004632 RID: 17970 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06004633 RID: 17971 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
			internal void <.cctor>b__9_0(object state)
			{
				Action action = state as Action;
				if (action == null)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
					return;
				}
				action();
			}

			// Token: 0x04002D47 RID: 11591
			public static readonly ValueTaskAwaiter.<>c <>9 = new ValueTaskAwaiter.<>c();
		}
	}
}
