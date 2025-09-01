using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E2 RID: 482
	internal class PipelineSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x00029EBB File Offset: 0x000280BB
		internal PipelineSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, AsynchronousChannel<TInputOutput> destination) : base(taskIndex, groupState)
		{
			this._source = source;
			this._destination = destination;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00029ED4 File Offset: 0x000280D4
		protected override void SpoolingWork()
		{
			TInputOutput item = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this._source;
			AsynchronousChannel<TInputOutput> destination = this._destination;
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			while (source.MoveNext(ref item, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(item);
			}
			destination.FlushBuffers();
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00029F35 File Offset: 0x00028135
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this._destination != null)
			{
				this._destination.SetDone();
			}
			this._source.Dispose();
		}

		// Token: 0x04000873 RID: 2163
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;

		// Token: 0x04000874 RID: 2164
		private AsynchronousChannel<TInputOutput> _destination;
	}
}
