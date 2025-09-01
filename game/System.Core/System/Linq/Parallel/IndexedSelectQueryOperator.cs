using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AE RID: 430
	internal sealed class IndexedSelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00027185 File Offset: 0x00025385
		internal IndexedSelectQueryOperator(IEnumerable<TInput> child, Func<TInput, int, TOutput> selector) : base(child)
		{
			this._selector = selector;
			this._outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000271A4 File Offset: 0x000253A4
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = ordinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
				ordinalIndexState2 = OrdinalIndexState.Correct;
			}
			base.SetOrdinalIndexState(ordinalIndexState2);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000271E5 File Offset: 0x000253E5
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults.NewResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000271FC File Offset: 0x000253FC
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TInput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInput, int>)inputStream;
			}
			PartitionedStream<TOutput, int> partitionedStream2 = new PartitionedStream<TOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorEnumerator(partitionedStream[i], this._selector);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002727A File Offset: 0x0002547A
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00027282 File Offset: 0x00025482
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this._selector);
		}

		// Token: 0x040007B8 RID: 1976
		private readonly Func<TInput, int, TOutput> _selector;

		// Token: 0x040007B9 RID: 1977
		private bool _prematureMerge;

		// Token: 0x040007BA RID: 1978
		private bool _limitsParallelism;

		// Token: 0x020001AF RID: 431
		private class IndexedSelectQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, int>
		{
			// Token: 0x06000B2A RID: 2858 RVA: 0x0002729B File Offset: 0x0002549B
			internal IndexedSelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, int> source, Func<TInput, int, TOutput> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x000272B4 File Offset: 0x000254B4
			internal override bool MoveNext(ref TOutput currentElement, ref int currentKey)
			{
				TInput arg = default(TInput);
				if (this._source.MoveNext(ref arg, ref currentKey))
				{
					currentElement = this._selector(arg, currentKey);
					return true;
				}
				return false;
			}

			// Token: 0x06000B2C RID: 2860 RVA: 0x000272F0 File Offset: 0x000254F0
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007BB RID: 1979
			private readonly QueryOperatorEnumerator<TInput, int> _source;

			// Token: 0x040007BC RID: 1980
			private readonly Func<TInput, int, TOutput> _selector;
		}

		// Token: 0x020001B0 RID: 432
		private class IndexedSelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06000B2D RID: 2861 RVA: 0x000272FD File Offset: 0x000254FD
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new IndexedSelectQueryOperator<TInput, TOutput>.IndexedSelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x0002731A File Offset: 0x0002551A
			private IndexedSelectQueryOperatorResults(QueryResults<TInput> childQueryResults, IndexedSelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this._selectOp = op;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002733F File Offset: 0x0002553F
			internal override int ElementsCount
			{
				get
				{
					return this._childQueryResults.ElementsCount;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00007E1D File Offset: 0x0000601D
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x0002734C File Offset: 0x0002554C
			internal override TOutput GetElement(int index)
			{
				return this._selectOp._selector(this._childQueryResults.GetElement(index), index);
			}

			// Token: 0x040007BD RID: 1981
			private IndexedSelectQueryOperator<TInput, TOutput> _selectOp;

			// Token: 0x040007BE RID: 1982
			private int _childCount;
		}
	}
}
