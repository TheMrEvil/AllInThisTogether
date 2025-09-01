using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200034B RID: 843
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x0007E800 File Offset: 0x0007CA00
		// Note: this type is marked as 'beforefieldinit'.
		static GenericDelegateCache()
		{
		}

		// Token: 0x04001CA6 RID: 7334
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> arg = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(arg);
		};

		// Token: 0x04001CA7 RID: 7335
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> obj = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(obj);
			return default(TResult);
		};

		// Token: 0x04001CA8 RID: 7336
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			return ((Func<Task<TAntecedentResult>[], TResult>)state)(wrappedAntecedents.Result);
		};

		// Token: 0x04001CA9 RID: 7337
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			((Action<Task<TAntecedentResult>[]>)state)(wrappedAntecedents.Result);
			return default(TResult);
		};

		// Token: 0x0200034C RID: 844
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002353 RID: 9043 RVA: 0x0007E861 File Offset: 0x0007CA61
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002354 RID: 9044 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002355 RID: 9045 RVA: 0x0007E870 File Offset: 0x0007CA70
			internal TResult <.cctor>b__4_0(Task<Task> wrappedWinner, object state)
			{
				Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
				Task<TAntecedentResult> arg = (Task<TAntecedentResult>)wrappedWinner.Result;
				return func(arg);
			}

			// Token: 0x06002356 RID: 9046 RVA: 0x0007E898 File Offset: 0x0007CA98
			internal TResult <.cctor>b__4_1(Task<Task> wrappedWinner, object state)
			{
				Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
				Task<TAntecedentResult> obj = (Task<TAntecedentResult>)wrappedWinner.Result;
				action(obj);
				return default(TResult);
			}

			// Token: 0x06002357 RID: 9047 RVA: 0x0007E8C6 File Offset: 0x0007CAC6
			internal TResult <.cctor>b__4_2(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task<TAntecedentResult>[], TResult>)state)(wrappedAntecedents.Result);
			}

			// Token: 0x06002358 RID: 9048 RVA: 0x0007E8E0 File Offset: 0x0007CAE0
			internal TResult <.cctor>b__4_3(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task<TAntecedentResult>[]>)state)(wrappedAntecedents.Result);
				return default(TResult);
			}

			// Token: 0x04001CAA RID: 7338
			public static readonly GenericDelegateCache<TAntecedentResult, TResult>.<>c <>9 = new GenericDelegateCache<TAntecedentResult, TResult>.<>c();
		}
	}
}
