using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B1 RID: 433
	internal sealed class IndexedWhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002736B File Offset: 0x0002556B
		internal IndexedWhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, int, bool> predicate) : base(child)
		{
			this._predicate = predicate;
			this._outputOrdered = true;
			this.InitOrdinalIndexState();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00027388 File Offset: 0x00025588
		private void InitOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct))
			{
				this._prematureMerge = true;
				this._limitsParallelism = (ordinalIndexState != OrdinalIndexState.Shuffled);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00025C6F File Offset: 0x00023E6F
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000273C8 File Offset: 0x000255C8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInputOutput, int> partitionedStream;
			if (this._prematureMerge)
			{
				partitionedStream = QueryOperator<TInputOutput>.ExecuteAndCollectResults<TKey>(inputStream, partitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
			}
			else
			{
				partitionedStream = (PartitionedStream<TInputOutput, int>)inputStream;
			}
			PartitionedStream<TInputOutput, int> partitionedStream2 = new PartitionedStream<TInputOutput, int>(partitionCount, Util.GetDefaultComparer<int>(), this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new IndexedWhereQueryOperator<TInputOutput>.IndexedWhereQueryOperatorEnumerator(partitionedStream[i], this._predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream2);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00027452 File Offset: 0x00025652
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Where(this._predicate);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00027471 File Offset: 0x00025671
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x040007BF RID: 1983
		private Func<TInputOutput, int, bool> _predicate;

		// Token: 0x040007C0 RID: 1984
		private bool _prematureMerge;

		// Token: 0x040007C1 RID: 1985
		private bool _limitsParallelism;

		// Token: 0x020001B2 RID: 434
		private class IndexedWhereQueryOperatorEnumerator : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000B38 RID: 2872 RVA: 0x00027479 File Offset: 0x00025679
			internal IndexedWhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, int> source, Func<TInputOutput, int, bool> predicate, CancellationToken cancellationToken)
			{
				this._source = source;
				this._predicate = predicate;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x00027498 File Offset: 0x00025698
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this._outputLoopCount == null)
				{
					this._outputLoopCount = new Shared<int>(0);
				}
				while (this._source.MoveNext(ref currentElement, ref currentKey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._predicate(currentElement, currentKey))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000B3A RID: 2874 RVA: 0x00027508 File Offset: 0x00025708
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007C2 RID: 1986
			private readonly QueryOperatorEnumerator<TInputOutput, int> _source;

			// Token: 0x040007C3 RID: 1987
			private readonly Func<TInputOutput, int, bool> _predicate;

			// Token: 0x040007C4 RID: 1988
			private CancellationToken _cancellationToken;

			// Token: 0x040007C5 RID: 1989
			private Shared<int> _outputLoopCount;
		}
	}
}
