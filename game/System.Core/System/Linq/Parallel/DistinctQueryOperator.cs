using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000196 RID: 406
	internal sealed class DistinctQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00025E08 File Offset: 0x00024008
		internal DistinctQueryOperator(IEnumerable<TInputOutput> source, IEqualityComparer<TInputOutput> comparer) : base(source)
		{
			this._comparer = comparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00025E1F File Offset: 0x0002401F
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, false);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00025E38 File Offset: 0x00024038
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00025EA8 File Offset: 0x000240A8
		private void WrapPartitionedStreamHelper<TKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> hashStream, IPartitionedStreamRecipient<TInputOutput> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream[i] = new DistinctQueryOperator<TInputOutput>.OrderedDistinctQueryOperatorEnumerator<TKey>(hashStream[i], this._comparer, hashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream[i] = (QueryOperatorEnumerator<TInputOutput, TKey>)new DistinctQueryOperator<TInputOutput>.DistinctQueryOperatorEnumerator<TKey>(hashStream[i], this._comparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00025F26 File Offset: 0x00024126
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Distinct(this._comparer);
		}

		// Token: 0x04000775 RID: 1909
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x02000197 RID: 407
		private class DistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000AD6 RID: 2774 RVA: 0x00025F45 File Offset: 0x00024145
			internal DistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._source = source;
				this._hashLookup = new Set<TInputOutput>(comparer);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x00025F68 File Offset: 0x00024168
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				TKey tkey = default(TKey);
				Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				if (this._outputLoopCount == null)
				{
					this._outputLoopCount = new Shared<int>(0);
				}
				while (this._source.MoveNext(ref pair, ref tkey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._hashLookup.Add(pair.First))
					{
						currentElement = pair.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x00025FF6 File Offset: 0x000241F6
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000776 RID: 1910
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> _source;

			// Token: 0x04000777 RID: 1911
			private Set<TInputOutput> _hashLookup;

			// Token: 0x04000778 RID: 1912
			private CancellationToken _cancellationToken;

			// Token: 0x04000779 RID: 1913
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x02000198 RID: 408
		private class OrderedDistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06000AD9 RID: 2777 RVA: 0x00026003 File Offset: 0x00024203
			internal OrderedDistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, IComparer<TKey> keyComparer, CancellationToken cancellationToken)
			{
				this._source = source;
				this._keyComparer = keyComparer;
				this._hashLookup = new Dictionary<Wrapper<TInputOutput>, TKey>(new WrapperEqualityComparer<TInputOutput>(comparer));
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x00026038 File Offset: 0x00024238
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
			{
				if (this._hashLookupEnumerator == null)
				{
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TKey tkey = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref pair, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						Wrapper<TInputOutput> key = new Wrapper<TInputOutput>(pair.First);
						TKey y;
						if (!this._hashLookup.TryGetValue(key, out y) || this._keyComparer.Compare(tkey, y) < 0)
						{
							this._hashLookup[key] = tkey;
						}
					}
					this._hashLookupEnumerator = this._hashLookup.GetEnumerator();
				}
				if (this._hashLookupEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, TKey> keyValuePair = this._hashLookupEnumerator.Current;
					currentElement = keyValuePair.Key.Value;
					currentKey = keyValuePair.Value;
					return true;
				}
				return false;
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x00026119 File Offset: 0x00024319
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
				if (this._hashLookupEnumerator != null)
				{
					this._hashLookupEnumerator.Dispose();
				}
			}

			// Token: 0x0400077A RID: 1914
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> _source;

			// Token: 0x0400077B RID: 1915
			private Dictionary<Wrapper<TInputOutput>, TKey> _hashLookup;

			// Token: 0x0400077C RID: 1916
			private IComparer<TKey> _keyComparer;

			// Token: 0x0400077D RID: 1917
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, TKey>> _hashLookupEnumerator;

			// Token: 0x0400077E RID: 1918
			private CancellationToken _cancellationToken;
		}
	}
}
