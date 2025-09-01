using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D9 RID: 2009
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x000E5160 File Offset: 0x000E3360
		public static AsyncIteratorMethodBuilder Create()
		{
			return default(AsyncIteratorMethodBuilder);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000E5176 File Offset: 0x000E3376
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000E517E File Offset: 0x000E337E
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x000E518D File Offset: 0x000E338D
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x000E519C File Offset: 0x000E339C
		public void Complete()
		{
			this._methodBuilder.SetResult();
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000E51A9 File Offset: 0x000E33A9
		internal object ObjectIdForDebugger
		{
			get
			{
				return this._methodBuilder.ObjectIdForDebugger;
			}
		}

		// Token: 0x04002D22 RID: 11554
		private AsyncTaskMethodBuilder _methodBuilder;
	}
}
