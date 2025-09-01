using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000122 RID: 290
	internal sealed class ConcatQueryOperator<TSource> : BinaryQueryOperator<TSource, TSource, TSource>
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0001F8CC File Offset: 0x0001DACC
		internal ConcatQueryOperator(ParallelQuery<TSource> firstChild, ParallelQuery<TSource> secondChild) : base(firstChild, secondChild)
		{
			this._outputOrdered = (base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered);
			this._prematureMergeLeft = base.LeftChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			this._prematureMergeRight = base.RightChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			if (base.LeftChild.OrdinalIndexState == OrdinalIndexState.Indexable && base.RightChild.OrdinalIndexState == OrdinalIndexState.Indexable)
			{
				base.SetOrdinalIndex(OrdinalIndexState.Indexable);
				return;
			}
			base.SetOrdinalIndex(OrdinalIndexState.Increasing.Worse(base.LeftChild.OrdinalIndexState.Worse(base.RightChild.OrdinalIndexState)));
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001F97C File Offset: 0x0001DB7C
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> leftChildQueryResults = base.LeftChild.Open(settings, preferStriping);
			QueryResults<TSource> rightChildQueryResults = base.RightChild.Open(settings, preferStriping);
			return ConcatQueryOperator<TSource>.ConcatQueryOperatorResults.NewResults(leftChildQueryResults, rightChildQueryResults, this, settings, preferStriping);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStream, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeLeft)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TLeftKey>(leftStream, leftStream.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int, TRightKey>(partitionedStream, rightStream, outputRecipient, settings, preferStriping);
				return;
			}
			this.WrapHelper<TLeftKey, TRightKey>(leftStream, rightStream, outputRecipient, settings, preferStriping);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001FA04 File Offset: 0x0001DC04
		private void WrapHelper<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, QuerySettings settings, bool preferStriping)
		{
			if (this._prematureMergeRight)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TRightKey>(rightStream, leftStreamInc.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper2<TLeftKey, int>(leftStreamInc, partitionedStream, outputRecipient);
				return;
			}
			this.WrapHelper2<TLeftKey, TRightKey>(leftStreamInc, rightStream, outputRecipient);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001FA50 File Offset: 0x0001DC50
		private void WrapHelper2<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStreamInc, IPartitionedStreamRecipient<TSource> outputRecipient)
		{
			int partitionCount = leftStreamInc.PartitionCount;
			IComparer<ConcatKey<TLeftKey, TRightKey>> keyComparer = ConcatKey<TLeftKey, TRightKey>.MakeComparer(leftStreamInc.KeyComparer, rightStreamInc.KeyComparer);
			PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>> partitionedStream = new PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>>(partitionCount, keyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ConcatQueryOperator<TSource>.ConcatQueryOperatorEnumerator<TLeftKey, TRightKey>(leftStreamInc[i], rightStreamInc[i]);
			}
			outputRecipient.Receive<ConcatKey<TLeftKey, TRightKey>>(partitionedStream);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001FAB1 File Offset: 0x0001DCB1
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.LeftChild.AsSequentialQuery(token).Concat(base.RightChild.AsSequentialQuery(token));
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000680 RID: 1664
		private readonly bool _prematureMergeLeft;

		// Token: 0x04000681 RID: 1665
		private readonly bool _prematureMergeRight;

		// Token: 0x02000123 RID: 291
		private sealed class ConcatQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TSource, ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x06000904 RID: 2308 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
			internal ConcatQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TLeftKey> firstSource, QueryOperatorEnumerator<TSource, TRightKey> secondSource)
			{
				this._firstSource = firstSource;
				this._secondSource = secondSource;
			}

			// Token: 0x06000905 RID: 2309 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
			internal override bool MoveNext(ref TSource currentElement, ref ConcatKey<TLeftKey, TRightKey> currentKey)
			{
				if (!this._begunSecond)
				{
					TLeftKey leftKey = default(TLeftKey);
					if (this._firstSource.MoveNext(ref currentElement, ref leftKey))
					{
						currentKey = ConcatKey<TLeftKey, TRightKey>.MakeLeft(leftKey);
						return true;
					}
					this._begunSecond = true;
				}
				TRightKey rightKey = default(TRightKey);
				if (this._secondSource.MoveNext(ref currentElement, ref rightKey))
				{
					currentKey = ConcatKey<TLeftKey, TRightKey>.MakeRight(rightKey);
					return true;
				}
				return false;
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x0001FB51 File Offset: 0x0001DD51
			protected override void Dispose(bool disposing)
			{
				this._firstSource.Dispose();
				this._secondSource.Dispose();
			}

			// Token: 0x04000682 RID: 1666
			private QueryOperatorEnumerator<TSource, TLeftKey> _firstSource;

			// Token: 0x04000683 RID: 1667
			private QueryOperatorEnumerator<TSource, TRightKey> _secondSource;

			// Token: 0x04000684 RID: 1668
			private bool _begunSecond;
		}

		// Token: 0x02000124 RID: 292
		private class ConcatQueryOperatorResults : BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults
		{
			// Token: 0x06000907 RID: 2311 RVA: 0x0001FB69 File Offset: 0x0001DD69
			public static QueryResults<TSource> NewResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (leftChildQueryResults.IsIndexible && rightChildQueryResults.IsIndexible)
				{
					return new ConcatQueryOperator<TSource>.ConcatQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
				}
				return new BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x0001FB92 File Offset: 0x0001DD92
			private ConcatQueryOperatorResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> concatOp, QuerySettings settings, bool preferStriping) : base(leftChildQueryResults, rightChildQueryResults, concatOp, settings, preferStriping)
			{
				this._leftChildCount = leftChildQueryResults.ElementsCount;
				this._rightChildCount = rightChildQueryResults.ElementsCount;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x00007E1D File Offset: 0x0000601D
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001FBB9 File Offset: 0x0001DDB9
			internal override int ElementsCount
			{
				get
				{
					return this._leftChildCount + this._rightChildCount;
				}
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
			internal override TSource GetElement(int index)
			{
				if (index < this._leftChildCount)
				{
					return this._leftChildQueryResults.GetElement(index);
				}
				return this._rightChildQueryResults.GetElement(index - this._leftChildCount);
			}

			// Token: 0x04000685 RID: 1669
			private int _leftChildCount;

			// Token: 0x04000686 RID: 1670
			private int _rightChildCount;
		}
	}
}
