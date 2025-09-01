using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B6 RID: 438
	internal sealed class ReverseQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x000277BC File Offset: 0x000259BC
		internal ReverseQueryOperator(IEnumerable<TSource> child) : base(child)
		{
			if (base.Child.OrdinalIndexState == OrdinalIndexState.Indexable)
			{
				base.SetOrdinalIndexState(OrdinalIndexState.Indexable);
				return;
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000277E4 File Offset: 0x000259E4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, new ReverseComparer<TKey>(inputStream.KeyComparer), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ReverseQueryOperator<TSource>.ReverseQueryOperatorEnumerator<TKey>(inputStream[i], settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002783D File Offset: 0x00025A3D
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return ReverseQueryOperator<TSource>.ReverseQueryOperatorResults.NewResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00027854 File Offset: 0x00025A54
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token).Reverse<TSource>();
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x020001B7 RID: 439
		private class ReverseQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06000B4A RID: 2890 RVA: 0x0002786D File Offset: 0x00025A6D
			internal ReverseQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, CancellationToken cancellationToken)
			{
				this._source = source;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x00027884 File Offset: 0x00025A84
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				if (this._buffer == null)
				{
					this._bufferIndex = new Shared<int>(0);
					this._buffer = new List<Pair<TSource, TKey>>();
					TSource first = default(TSource);
					TKey second = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref first, ref second))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						this._buffer.Add(new Pair<TSource, TKey>(first, second));
						this._bufferIndex.Value++;
					}
				}
				Shared<int> bufferIndex = this._bufferIndex;
				int num2 = bufferIndex.Value - 1;
				bufferIndex.Value = num2;
				if (num2 >= 0)
				{
					currentElement = this._buffer[this._bufferIndex.Value].First;
					currentKey = this._buffer[this._bufferIndex.Value].Second;
					return true;
				}
				return false;
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x00027975 File Offset: 0x00025B75
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007D2 RID: 2002
			private readonly QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x040007D3 RID: 2003
			private readonly CancellationToken _cancellationToken;

			// Token: 0x040007D4 RID: 2004
			private List<Pair<TSource, TKey>> _buffer;

			// Token: 0x040007D5 RID: 2005
			private Shared<int> _bufferIndex;
		}

		// Token: 0x020001B8 RID: 440
		private class ReverseQueryOperatorResults : UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults
		{
			// Token: 0x06000B4D RID: 2893 RVA: 0x00027982 File Offset: 0x00025B82
			public static QueryResults<TSource> NewResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new ReverseQueryOperator<TSource>.ReverseQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000B4E RID: 2894 RVA: 0x0002799F File Offset: 0x00025B9F
			private ReverseQueryOperatorResults(QueryResults<TSource> childQueryResults, ReverseQueryOperator<TSource> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this._count = this._childQueryResults.ElementsCount;
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00007E1D File Offset: 0x0000601D
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000279BD File Offset: 0x00025BBD
			internal override int ElementsCount
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x000279C5 File Offset: 0x00025BC5
			internal override TSource GetElement(int index)
			{
				return this._childQueryResults.GetElement(this._count - index - 1);
			}

			// Token: 0x040007D6 RID: 2006
			private int _count;
		}
	}
}
