using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Pipes
{
	// Token: 0x0200034A RID: 842
	internal abstract class PipeCompletionSource<TResult> : TaskCompletionSource<TResult>
	{
		// Token: 0x0600197F RID: 6527 RVA: 0x00055718 File Offset: 0x00053918
		protected unsafe PipeCompletionSource(ThreadPoolBoundHandle handle, ReadOnlyMemory<byte> bufferToPin) : base(TaskCreationOptions.RunContinuationsAsynchronously)
		{
			this._threadPoolBinding = handle;
			this._state = 0;
			this._pinnedMemory = bufferToPin.Pin();
			this._overlapped = this._threadPoolBinding.AllocateNativeOverlapped(delegate(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
			{
				((PipeCompletionSource<TResult>)ThreadPoolBoundHandle.GetNativeOverlappedState(pOverlapped)).AsyncCallback(errorCode, numBytes);
			}, this, null);
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005577A File Offset: 0x0005397A
		internal unsafe NativeOverlapped* Overlapped
		{
			get
			{
				return this._overlapped;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00055784 File Offset: 0x00053984
		internal void RegisterForCancellation(CancellationToken cancellationToken)
		{
			if (cancellationToken.CanBeCanceled && this.Overlapped != null)
			{
				int num = Interlocked.CompareExchange(ref this._state, 4, 0);
				if (num == 0)
				{
					this._cancellationRegistration = cancellationToken.Register(delegate(object thisRef)
					{
						((PipeCompletionSource<TResult>)thisRef).Cancel();
					}, this);
					num = Interlocked.Exchange(ref this._state, 0);
				}
				else if (num != 8)
				{
					num = Interlocked.Exchange(ref this._state, 0);
				}
				if ((num & 3) != 0)
				{
					this.CompleteCallback(num);
				}
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005580E File Offset: 0x00053A0E
		internal void ReleaseResources()
		{
			this._cancellationRegistration.Dispose();
			if (this._overlapped != null)
			{
				this._threadPoolBinding.FreeNativeOverlapped(this.Overlapped);
				this._overlapped = null;
			}
			this._pinnedMemory.Dispose();
		}

		// Token: 0x06001983 RID: 6531
		internal abstract void SetCompletedSynchronously();

		// Token: 0x06001984 RID: 6532 RVA: 0x0005584C File Offset: 0x00053A4C
		protected virtual void AsyncCallback(uint errorCode, uint numBytes)
		{
			int num;
			if (errorCode == 0U)
			{
				num = 1;
			}
			else
			{
				num = 2;
				this._errorCode = (int)errorCode;
			}
			if (Interlocked.Exchange(ref this._state, num) == 0 && Interlocked.Exchange(ref this._state, 8) != 0)
			{
				this.CompleteCallback(num);
			}
		}

		// Token: 0x06001985 RID: 6533
		protected abstract void HandleError(int errorCode);

		// Token: 0x06001986 RID: 6534 RVA: 0x0005588C File Offset: 0x00053A8C
		private unsafe void Cancel()
		{
			SafeHandle handle = this._threadPoolBinding.Handle;
			NativeOverlapped* overlapped = this.Overlapped;
			if (!handle.IsInvalid && !Interop.Kernel32.CancelIoEx(handle, overlapped))
			{
				Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000558C3 File Offset: 0x00053AC3
		protected virtual void HandleUnexpectedCancellation()
		{
			base.TrySetCanceled();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000558CC File Offset: 0x00053ACC
		private void CompleteCallback(int resultState)
		{
			CancellationToken token = this._cancellationRegistration.Token;
			this.ReleaseResources();
			if (resultState != 2)
			{
				this.SetCompletedSynchronously();
				return;
			}
			if (this._errorCode != 995)
			{
				this.HandleError(this._errorCode);
				return;
			}
			if (token.CanBeCanceled && !token.IsCancellationRequested)
			{
				this.HandleUnexpectedCancellation();
				return;
			}
			base.TrySetCanceled(token);
		}

		// Token: 0x04000C39 RID: 3129
		private const int NoResult = 0;

		// Token: 0x04000C3A RID: 3130
		private const int ResultSuccess = 1;

		// Token: 0x04000C3B RID: 3131
		private const int ResultError = 2;

		// Token: 0x04000C3C RID: 3132
		private const int RegisteringCancellation = 4;

		// Token: 0x04000C3D RID: 3133
		private const int CompletedCallback = 8;

		// Token: 0x04000C3E RID: 3134
		private readonly ThreadPoolBoundHandle _threadPoolBinding;

		// Token: 0x04000C3F RID: 3135
		private CancellationTokenRegistration _cancellationRegistration;

		// Token: 0x04000C40 RID: 3136
		private int _errorCode;

		// Token: 0x04000C41 RID: 3137
		private unsafe NativeOverlapped* _overlapped;

		// Token: 0x04000C42 RID: 3138
		private MemoryHandle _pinnedMemory;

		// Token: 0x04000C43 RID: 3139
		private int _state;

		// Token: 0x0200034B RID: 843
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001989 RID: 6537 RVA: 0x00055931 File Offset: 0x00053B31
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600198A RID: 6538 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x0005593D File Offset: 0x00053B3D
			internal unsafe void <.ctor>b__11_0(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
			{
				((PipeCompletionSource<TResult>)ThreadPoolBoundHandle.GetNativeOverlappedState(pOverlapped)).AsyncCallback(errorCode, numBytes);
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x00055951 File Offset: 0x00053B51
			internal void <RegisterForCancellation>b__14_0(object thisRef)
			{
				((PipeCompletionSource<TResult>)thisRef).Cancel();
			}

			// Token: 0x04000C44 RID: 3140
			public static readonly PipeCompletionSource<TResult>.<>c <>9 = new PipeCompletionSource<TResult>.<>c();

			// Token: 0x04000C45 RID: 3141
			public static IOCompletionCallback <>9__11_0;

			// Token: 0x04000C46 RID: 3142
			public static Action<object> <>9__14_0;
		}
	}
}
