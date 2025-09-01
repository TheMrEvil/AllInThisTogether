using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B3 RID: 435
	internal sealed class LastQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x00027515 File Offset: 0x00025715
		internal LastQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate) : base(child)
		{
			this._predicate = predicate;
			this._prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00026180 File Offset: 0x00024380
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002753C File Offset: 0x0002573C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeNeeded)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00027588 File Offset: 0x00025788
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState = new LastQueryOperator<TSource>.LastQueryOperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new LastQueryOperator<TSource>.LastQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040007C6 RID: 1990
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x040007C7 RID: 1991
		private readonly bool _prematureMergeNeeded;

		// Token: 0x020001B4 RID: 436
		private class LastQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000B41 RID: 2881 RVA: 0x000275FE File Offset: 0x000257FE
			internal LastQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this._source = source;
				this._predicate = predicate;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancelToken;
				this._keyComparer = keyComparer;
				this._partitionId = partitionId;
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x0002763C File Offset: 0x0002583C
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this._alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				bool flag = false;
				try
				{
					int num = 0;
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					while (this._source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._predicate == null || this._predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							flag = true;
						}
						num++;
					}
					if (flag)
					{
						LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState = this._operatorState;
						lock (operatorState)
						{
							if (this._operatorState._partitionId == -1 || this._keyComparer.Compare(tkey, this._operatorState._key) > 0)
							{
								this._operatorState._partitionId = this._partitionId;
								this._operatorState._key = tkey;
							}
						}
					}
				}
				finally
				{
					this._sharedBarrier.Signal();
				}
				this._alreadySearched = true;
				if (this._partitionId == this._operatorState._partitionId)
				{
					this._sharedBarrier.Wait(this._cancellationToken);
					if (this._operatorState._partitionId == this._partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x000277A0 File Offset: 0x000259A0
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007C8 RID: 1992
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x040007C9 RID: 1993
			private Func<TSource, bool> _predicate;

			// Token: 0x040007CA RID: 1994
			private bool _alreadySearched;

			// Token: 0x040007CB RID: 1995
			private int _partitionId;

			// Token: 0x040007CC RID: 1996
			private LastQueryOperator<TSource>.LastQueryOperatorState<TKey> _operatorState;

			// Token: 0x040007CD RID: 1997
			private CountdownEvent _sharedBarrier;

			// Token: 0x040007CE RID: 1998
			private CancellationToken _cancellationToken;

			// Token: 0x040007CF RID: 1999
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x020001B5 RID: 437
		private class LastQueryOperatorState<TKey>
		{
			// Token: 0x06000B44 RID: 2884 RVA: 0x000277AD File Offset: 0x000259AD
			public LastQueryOperatorState()
			{
			}

			// Token: 0x040007D0 RID: 2000
			internal TKey _key;

			// Token: 0x040007D1 RID: 2001
			internal int _partitionId = -1;
		}
	}
}
