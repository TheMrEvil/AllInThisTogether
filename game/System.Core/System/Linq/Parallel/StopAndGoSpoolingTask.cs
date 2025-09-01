using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E1 RID: 481
	internal class StopAndGoSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x00029E1B File Offset: 0x0002801B
		internal StopAndGoSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, SynchronousChannel<TInputOutput> destination) : base(taskIndex, groupState)
		{
			this._source = source;
			this._destination = destination;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00029E34 File Offset: 0x00028034
		protected override void SpoolingWork()
		{
			TInputOutput item = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source = this._source;
			SynchronousChannel<TInputOutput> destination = this._destination;
			CancellationToken mergedCancellationToken = this._groupState.CancellationState.MergedCancellationToken;
			destination.Init();
			while (source.MoveNext(ref item, ref tignoreKey) && !mergedCancellationToken.IsCancellationRequested)
			{
				destination.Enqueue(item);
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00029E95 File Offset: 0x00028095
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			if (this._destination != null)
			{
				this._destination.SetDone();
			}
			this._source.Dispose();
		}

		// Token: 0x04000871 RID: 2161
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;

		// Token: 0x04000872 RID: 2162
		private SynchronousChannel<TInputOutput> _destination;
	}
}
