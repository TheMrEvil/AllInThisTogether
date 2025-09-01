using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000194 RID: 404
	internal sealed class DefaultIfEmptyQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00025C48 File Offset: 0x00023E48
		internal DefaultIfEmptyQueryOperator(IEnumerable<TSource> child, TSource defaultValue) : base(child)
		{
			this._defaultValue = defaultValue;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Correct));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00025C6F File Offset: 0x00023E6F
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00025C88 File Offset: 0x00023E88
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			Shared<int> sharedEmptyCount = new Shared<int>(0);
			CountdownEvent sharedLatch = new CountdownEvent(partitionCount - 1);
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new DefaultIfEmptyQueryOperator<TSource>.DefaultIfEmptyQueryOperatorEnumerator<TKey>(inputStream[i], this._defaultValue, i, partitionCount, sharedEmptyCount, sharedLatch, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00025D02 File Offset: 0x00023F02
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).DefaultIfEmpty(this._defaultValue);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400076C RID: 1900
		private readonly TSource _defaultValue;

		// Token: 0x02000195 RID: 405
		private class DefaultIfEmptyQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06000ACD RID: 2765 RVA: 0x00025D1B File Offset: 0x00023F1B
			internal DefaultIfEmptyQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, TSource defaultValue, int partitionIndex, int partitionCount, Shared<int> sharedEmptyCount, CountdownEvent sharedLatch, CancellationToken cancelToken)
			{
				this._source = source;
				this._defaultValue = defaultValue;
				this._partitionIndex = partitionIndex;
				this._partitionCount = partitionCount;
				this._sharedEmptyCount = sharedEmptyCount;
				this._sharedLatch = sharedLatch;
				this._cancelToken = cancelToken;
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x00025D58 File Offset: 0x00023F58
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				bool flag = this._source.MoveNext(ref currentElement, ref currentKey);
				if (!this._lookedForEmpty)
				{
					this._lookedForEmpty = true;
					if (!flag)
					{
						if (this._partitionIndex == 0)
						{
							this._sharedLatch.Wait(this._cancelToken);
							this._sharedLatch.Dispose();
							if (this._sharedEmptyCount.Value == this._partitionCount - 1)
							{
								currentElement = this._defaultValue;
								currentKey = default(TKey);
								return true;
							}
							return false;
						}
						else
						{
							Interlocked.Increment(ref this._sharedEmptyCount.Value);
						}
					}
					if (this._partitionIndex != 0)
					{
						this._sharedLatch.Signal();
					}
				}
				return flag;
			}

			// Token: 0x06000ACF RID: 2767 RVA: 0x00025DFB File Offset: 0x00023FFB
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x0400076D RID: 1901
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x0400076E RID: 1902
			private bool _lookedForEmpty;

			// Token: 0x0400076F RID: 1903
			private int _partitionIndex;

			// Token: 0x04000770 RID: 1904
			private int _partitionCount;

			// Token: 0x04000771 RID: 1905
			private TSource _defaultValue;

			// Token: 0x04000772 RID: 1906
			private Shared<int> _sharedEmptyCount;

			// Token: 0x04000773 RID: 1907
			private CountdownEvent _sharedLatch;

			// Token: 0x04000774 RID: 1908
			private CancellationToken _cancelToken;
		}
	}
}
