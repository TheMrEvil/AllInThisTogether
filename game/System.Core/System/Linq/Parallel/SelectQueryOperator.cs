using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BE RID: 446
	internal sealed class SelectQueryOperator<TInput, TOutput> : UnaryQueryOperator<TInput, TOutput>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00028012 File Offset: 0x00026212
		internal SelectQueryOperator(IEnumerable<TInput> child, Func<TInput, TOutput> selector) : base(child)
		{
			this._selector = selector;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00028034 File Offset: 0x00026234
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			PartitionedStream<TOutput, TKey> partitionedStream = new PartitionedStream<TOutput, TKey>(inputStream.PartitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorEnumerator<TKey>(inputStream[i], this._selector);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002808A File Offset: 0x0002628A
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults.NewResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000280A1 File Offset: 0x000262A1
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).Select(this._selector);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040007F0 RID: 2032
		private Func<TInput, TOutput> _selector;

		// Token: 0x020001BF RID: 447
		private class SelectQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TOutput, TKey>
		{
			// Token: 0x06000B67 RID: 2919 RVA: 0x000280BA File Offset: 0x000262BA
			internal SelectQueryOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Func<TInput, TOutput> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x000280D0 File Offset: 0x000262D0
			internal override bool MoveNext(ref TOutput currentElement, ref TKey currentKey)
			{
				TInput arg = default(TInput);
				if (this._source.MoveNext(ref arg, ref currentKey))
				{
					currentElement = this._selector(arg);
					return true;
				}
				return false;
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x0002810A File Offset: 0x0002630A
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040007F1 RID: 2033
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x040007F2 RID: 2034
			private readonly Func<TInput, TOutput> _selector;
		}

		// Token: 0x020001C0 RID: 448
		private class SelectQueryOperatorResults : UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults
		{
			// Token: 0x06000B6A RID: 2922 RVA: 0x00028117 File Offset: 0x00026317
			public static QueryResults<TOutput> NewResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new SelectQueryOperator<TInput, TOutput>.SelectQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x00028134 File Offset: 0x00026334
			private SelectQueryOperatorResults(QueryResults<TInput> childQueryResults, SelectQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping) : base(childQueryResults, op, settings, preferStriping)
			{
				this._selector = op._selector;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00007E1D File Offset: 0x0000601D
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002815E File Offset: 0x0002635E
			internal override int ElementsCount
			{
				get
				{
					return this._childCount;
				}
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x00028166 File Offset: 0x00026366
			internal override TOutput GetElement(int index)
			{
				return this._selector(this._childQueryResults.GetElement(index));
			}

			// Token: 0x040007F3 RID: 2035
			private Func<TInput, TOutput> _selector;

			// Token: 0x040007F4 RID: 2036
			private int _childCount;
		}
	}
}
