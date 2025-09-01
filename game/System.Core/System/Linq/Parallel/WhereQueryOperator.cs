using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CE RID: 462
	internal sealed class WhereQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x000290E1 File Offset: 0x000272E1
		internal WhereQueryOperator(IEnumerable<TInputOutput> child, Func<TInputOutput, bool> predicate) : base(child)
		{
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Increasing));
			this._predicate = predicate;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00029108 File Offset: 0x00027308
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new WhereQueryOperator<TInputOutput>.WhereQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00025C6F File Offset: 0x00023E6F
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002916A File Offset: 0x0002736A
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Where(this._predicate);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000828 RID: 2088
		private Func<TInputOutput, bool> _predicate;

		// Token: 0x020001CF RID: 463
		private class WhereQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06000BA6 RID: 2982 RVA: 0x00029189 File Offset: 0x00027389
			internal WhereQueryOperatorEnumerator(QueryOperatorEnumerator<TInputOutput, TKey> source, Func<TInputOutput, bool> predicate, CancellationToken cancellationToken)
			{
				this._source = source;
				this._predicate = predicate;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x000291A8 File Offset: 0x000273A8
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
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
					if (this._predicate(currentElement))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x00029216 File Offset: 0x00027416
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000829 RID: 2089
			private readonly QueryOperatorEnumerator<TInputOutput, TKey> _source;

			// Token: 0x0400082A RID: 2090
			private readonly Func<TInputOutput, bool> _predicate;

			// Token: 0x0400082B RID: 2091
			private CancellationToken _cancellationToken;

			// Token: 0x0400082C RID: 2092
			private Shared<int> _outputLoopCount;
		}
	}
}
