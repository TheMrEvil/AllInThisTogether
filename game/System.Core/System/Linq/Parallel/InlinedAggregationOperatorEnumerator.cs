using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000150 RID: 336
	internal abstract class InlinedAggregationOperatorEnumerator<TIntermediate> : QueryOperatorEnumerator<TIntermediate, int>
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x0002271F File Offset: 0x0002091F
		internal InlinedAggregationOperatorEnumerator(int partitionIndex, CancellationToken cancellationToken)
		{
			this._partitionIndex = partitionIndex;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00022735 File Offset: 0x00020935
		internal sealed override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
		{
			if (!this._done && this.MoveNextCore(ref currentElement))
			{
				currentKey = this._partitionIndex;
				this._done = true;
				return true;
			}
			return false;
		}

		// Token: 0x060009AF RID: 2479
		protected abstract bool MoveNextCore(ref TIntermediate currentElement);

		// Token: 0x040006FF RID: 1791
		private int _partitionIndex;

		// Token: 0x04000700 RID: 1792
		private bool _done;

		// Token: 0x04000701 RID: 1793
		protected CancellationToken _cancellationToken;
	}
}
