using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DE RID: 2014
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder<TResult>
	{
		// Token: 0x060045CC RID: 17868 RVA: 0x000E529C File Offset: 0x000E349C
		public static AsyncValueTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncValueTaskMethodBuilder<TResult>);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x000E52B2 File Offset: 0x000E34B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x000E52C0 File Offset: 0x000E34C0
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x000E52CE File Offset: 0x000E34CE
		public void SetResult(TResult result)
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult(result);
				return;
			}
			this._result = result;
			this._haveResult = true;
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x000E52F3 File Offset: 0x000E34F3
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000E5301 File Offset: 0x000E3501
		public ValueTask<TResult> Task
		{
			get
			{
				if (this._haveResult)
				{
					return new ValueTask<TResult>(this._result);
				}
				this._useBuilder = true;
				return new ValueTask<TResult>(this._methodBuilder.Task);
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x000E532E File Offset: 0x000E352E
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000E5344 File Offset: 0x000E3544
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04002D27 RID: 11559
		private AsyncTaskMethodBuilder<TResult> _methodBuilder;

		// Token: 0x04002D28 RID: 11560
		private TResult _result;

		// Token: 0x04002D29 RID: 11561
		private bool _haveResult;

		// Token: 0x04002D2A RID: 11562
		private bool _useBuilder;
	}
}
