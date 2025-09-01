using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200014F RID: 335
	internal abstract class InlinedAggregationOperator<TSource, TIntermediate, TResult> : UnaryQueryOperator<TSource, TIntermediate>
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0002262A File Offset: 0x0002082A
		internal InlinedAggregationOperator(IEnumerable<TSource> child) : base(child)
		{
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00022634 File Offset: 0x00020834
		internal TResult Aggregate()
		{
			Exception ex = null;
			TResult result;
			try
			{
				result = this.InternalAggregate(ref ex);
			}
			catch (Exception ex2)
			{
				if (ex2 is AggregateException)
				{
					throw;
				}
				OperationCanceledException ex3 = ex2 as OperationCanceledException;
				if (ex3 != null && ex3.CancellationToken == base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken && base.SpecifiedQuerySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw;
				}
				throw new AggregateException(new Exception[]
				{
					ex2
				});
			}
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x060009A7 RID: 2471
		protected abstract TResult InternalAggregate(ref Exception singularExceptionToThrow);

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001F730 File Offset: 0x0001D930
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TIntermediate>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000226C8 File Offset: 0x000208C8
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = this.CreateEnumerator<TKey>(i, partitionCount, inputStream[i], null, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x060009AA RID: 2474
		protected abstract QueryOperatorEnumerator<TIntermediate, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<TSource, TKey> source, object sharedData, CancellationToken cancellationToken);

		// Token: 0x060009AB RID: 2475 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}
	}
}
