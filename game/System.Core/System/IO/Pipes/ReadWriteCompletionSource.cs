using System;

namespace System.IO.Pipes
{
	// Token: 0x02000352 RID: 850
	internal sealed class ReadWriteCompletionSource : PipeCompletionSource<int>
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x00056B12 File Offset: 0x00054D12
		internal ReadWriteCompletionSource(PipeStream stream, ReadOnlyMemory<byte> bufferToPin, bool isWrite) : base(stream._threadPoolBinding, bufferToPin)
		{
			this._pipeStream = stream;
			this._isWrite = isWrite;
			this._isMessageComplete = true;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00056B36 File Offset: 0x00054D36
		internal override void SetCompletedSynchronously()
		{
			if (!this._isWrite)
			{
				this._pipeStream.UpdateMessageCompletion(this._isMessageComplete);
			}
			base.TrySetResult(this._numBytes);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00056B60 File Offset: 0x00054D60
		protected override void AsyncCallback(uint errorCode, uint numBytes)
		{
			this._numBytes = (int)numBytes;
			if (!this._isWrite && (errorCode == 109U || errorCode - 232U <= 1U))
			{
				errorCode = 0U;
			}
			if (errorCode == 234U)
			{
				errorCode = 0U;
				this._isMessageComplete = false;
			}
			else
			{
				this._isMessageComplete = true;
			}
			base.AsyncCallback(errorCode, numBytes);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00056BB1 File Offset: 0x00054DB1
		protected override void HandleError(int errorCode)
		{
			base.TrySetException(this._pipeStream.WinIOError(errorCode));
		}

		// Token: 0x04000C6A RID: 3178
		private readonly bool _isWrite;

		// Token: 0x04000C6B RID: 3179
		private readonly PipeStream _pipeStream;

		// Token: 0x04000C6C RID: 3180
		private bool _isMessageComplete;

		// Token: 0x04000C6D RID: 3181
		private int _numBytes;
	}
}
