using System;

namespace System.IO.Pipes
{
	// Token: 0x0200033F RID: 831
	internal sealed class ConnectionCompletionSource : PipeCompletionSource<VoidResult>
	{
		// Token: 0x06001933 RID: 6451 RVA: 0x00054760 File Offset: 0x00052960
		internal ConnectionCompletionSource(NamedPipeServerStream server) : base(server._threadPoolBinding, ReadOnlyMemory<byte>.Empty)
		{
			this._serverStream = server;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005477C File Offset: 0x0005297C
		internal override void SetCompletedSynchronously()
		{
			this._serverStream.State = PipeState.Connected;
			base.TrySetResult(default(VoidResult));
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000547A5 File Offset: 0x000529A5
		protected override void AsyncCallback(uint errorCode, uint numBytes)
		{
			if (errorCode == 535U)
			{
				errorCode = 0U;
			}
			base.AsyncCallback(errorCode, numBytes);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000547BA File Offset: 0x000529BA
		protected override void HandleError(int errorCode)
		{
			base.TrySetException(Win32Marshal.GetExceptionForWin32Error(errorCode, ""));
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000547CE File Offset: 0x000529CE
		protected override void HandleUnexpectedCancellation()
		{
			base.TrySetException(Error.GetOperationAborted());
		}

		// Token: 0x04000C11 RID: 3089
		private readonly NamedPipeServerStream _serverStream;
	}
}
