using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000199 RID: 409
	internal sealed class ElementAtQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002613C File Offset: 0x0002433C
		internal ElementAtQueryOperator(IEnumerable<TSource> child, int index) : base(child)
		{
			this._index = index;
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00026180 File Offset: 0x00024380
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00026198 File Offset: 0x00024398
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TSource, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TSource, int>)inputStream;
			}
			Shared<bool> resultFoundFlag = new Shared<bool>(false);
			PartitionedStream<TSource, int> partitionedStream2 = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new ElementAtQueryOperator<TSource>.ElementAtQueryOperatorEnumerator(partitionedStream[i], this._index, resultFoundFlag, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002622B File Offset: 0x0002442B
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00026234 File Offset: 0x00024434
		internal bool Aggregate(out TSource result, bool withDefaultValue)
		{
			if (this.LimitsParallelism && base.SpecifiedQuerySettings.WithDefaults().ExecutionMode.Value != ParallelExecutionMode.ForceParallelism)
			{
				CancellationState cancellationState = base.SpecifiedQuerySettings.CancellationState;
				if (withDefaultValue)
				{
					IEnumerable<TSource> source = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken), cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(source, cancellationState).ElementAtOrDefault(this._index);
				}
				else
				{
					IEnumerable<TSource> source2 = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(cancellationState.ExternalCancellationToken), cancellationState.ExternalCancellationToken);
					result = ExceptionAggregator.WrapEnumerable<TSource>(source2, cancellationState).ElementAt(this._index);
				}
				return true;
			}
			using (IEnumerator<TSource> enumerator = base.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					result = tsource;
					return true;
				}
			}
			result = default(TSource);
			return false;
		}

		// Token: 0x0400077F RID: 1919
		private readonly int _index;

		// Token: 0x04000780 RID: 1920
		private readonly bool _prematureMerge;

		// Token: 0x04000781 RID: 1921
		private readonly bool _limitsParallelism;

		// Token: 0x0200019A RID: 410
		private class ElementAtQueryOperatorEnumerator : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000AE2 RID: 2786 RVA: 0x00026348 File Offset: 0x00024548
			internal ElementAtQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, int> source, int index, Shared<bool> resultFoundFlag, CancellationToken cancellationToken)
			{
				this._source = source;
				this._index = index;
				this._resultFoundFlag = resultFoundFlag;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00026370 File Offset: 0x00024570
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				int num = 0;
				while (this._source.MoveNext(ref currentElement, ref currentKey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._resultFoundFlag.Value)
					{
						break;
					}
					if (currentKey == this._index)
					{
						this._resultFoundFlag.Value = true;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x000263CB File Offset: 0x000245CB
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000782 RID: 1922
			private QueryOperatorEnumerator<TSource, int> _source;

			// Token: 0x04000783 RID: 1923
			private int _index;

			// Token: 0x04000784 RID: 1924
			private Shared<bool> _resultFoundFlag;

			// Token: 0x04000785 RID: 1925
			private CancellationToken _cancellationToken;
		}
	}
}
