using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000188 RID: 392
	internal abstract class QueryOperator<TOutput> : ParallelQuery<TOutput>
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x00024F6C File Offset: 0x0002316C
		internal QueryOperator(QuerySettings settings) : this(false, settings)
		{
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00024F76 File Offset: 0x00023176
		internal QueryOperator(bool isOrdered, QuerySettings settings) : base(settings)
		{
			this._outputOrdered = isOrdered;
		}

		// Token: 0x06000A69 RID: 2665
		internal abstract QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping);

		// Token: 0x06000A6A RID: 2666 RVA: 0x00024F88 File Offset: 0x00023188
		public override IEnumerator<TOutput> GetEnumerator()
		{
			return this.GetEnumerator(null, false);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00024FA5 File Offset: 0x000231A5
		public IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions)
		{
			return this.GetEnumerator(mergeOptions, false);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00024FAF File Offset: 0x000231AF
		internal bool OutputOrdered
		{
			get
			{
				return this._outputOrdered;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00024FB7 File Offset: 0x000231B7
		internal virtual IEnumerator<TOutput> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return new QueryOpeningEnumerator<TOutput>(this, mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00024FC4 File Offset: 0x000231C4
		internal IEnumerator<TOutput> GetOpenedEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrder, bool forEffect, QuerySettings querySettings)
		{
			if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TOutput>(this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).GetEnumerator();
			}
			QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
			if (mergeOptions == null)
			{
				mergeOptions = querySettings.MergeOptions;
			}
			if (querySettings.CancellationState.MergedCancellationToken.IsCancellationRequested)
			{
				if (querySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(querySettings.CancellationState.ExternalCancellationToken);
				}
				throw new OperationCanceledException();
			}
			else
			{
				bool outputOrdered = this.OutputOrdered && !suppressOrder;
				PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(forEffect, mergeOptions.GetValueOrDefault(), querySettings.TaskScheduler, outputOrdered, querySettings.CancellationState, querySettings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				if (forEffect)
				{
					return null;
				}
				return partitionedStreamMerger.MergeExecutor.GetEnumerator();
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000250B3 File Offset: 0x000232B3
		private QueryResults<TOutput> GetQueryResults(QuerySettings querySettings)
		{
			return this.Open(querySettings, false);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000250C0 File Offset: 0x000232C0
		internal TOutput[] ExecuteAndGetResultsAsArray()
		{
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings().WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			TOutput[] result;
			try
			{
				if (querySettings.ExecutionMode.Value == ParallelExecutionMode.Default && this.LimitsParallelism)
				{
					result = ExceptionAggregator.WrapEnumerable<TOutput>(CancellableEnumerable.Wrap<TOutput>(this.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).ToArray<TOutput>();
				}
				else
				{
					QueryResults<TOutput> queryResults = this.GetQueryResults(querySettings);
					if (querySettings.CancellationState.MergedCancellationToken.IsCancellationRequested)
					{
						if (querySettings.CancellationState.ExternalCancellationToken.IsCancellationRequested)
						{
							throw new OperationCanceledException(querySettings.CancellationState.ExternalCancellationToken);
						}
						throw new OperationCanceledException();
					}
					else if (queryResults.IsIndexible && this.OutputOrdered)
					{
						ArrayMergeHelper<TOutput> arrayMergeHelper = new ArrayMergeHelper<TOutput>(base.SpecifiedQuerySettings, queryResults);
						arrayMergeHelper.Execute();
						TOutput[] resultsAsArray = arrayMergeHelper.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						result = resultsAsArray;
					}
					else
					{
						PartitionedStreamMerger<TOutput> partitionedStreamMerger = new PartitionedStreamMerger<TOutput>(false, ParallelMergeOptions.FullyBuffered, querySettings.TaskScheduler, this.OutputOrdered, querySettings.CancellationState, querySettings.QueryId);
						queryResults.GivePartitionedStream(partitionedStreamMerger);
						TOutput[] resultsAsArray2 = partitionedStreamMerger.MergeExecutor.GetResultsAsArray();
						querySettings.CleanStateAtQueryEnd();
						result = resultsAsArray2;
					}
				}
			}
			finally
			{
				QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
			}
			return result;
		}

		// Token: 0x06000A71 RID: 2673
		internal abstract IEnumerable<TOutput> AsSequentialQuery(CancellationToken token);

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A72 RID: 2674
		internal abstract bool LimitsParallelism { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A73 RID: 2675
		internal abstract OrdinalIndexState OrdinalIndexState { get; }

		// Token: 0x06000A74 RID: 2676 RVA: 0x00025230 File Offset: 0x00023430
		internal static ListQueryResults<TOutput> ExecuteAndCollectResults<TKey>(PartitionedStream<TOutput, TKey> openedChild, int partitionCount, bool outputOrdered, bool useStriping, QuerySettings settings)
		{
			TaskScheduler taskScheduler = settings.TaskScheduler;
			return new ListQueryResults<TOutput>(MergeExecutor<TOutput>.Execute<TKey>(openedChild, false, ParallelMergeOptions.FullyBuffered, taskScheduler, outputOrdered, settings.CancellationState, settings.QueryId).GetResultsAsArray(), partitionCount, useStriping);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002526C File Offset: 0x0002346C
		internal static QueryOperator<TOutput> AsQueryOperator(IEnumerable<TOutput> source)
		{
			QueryOperator<TOutput> queryOperator = source as QueryOperator<TOutput>;
			if (queryOperator == null)
			{
				OrderedParallelQuery<TOutput> orderedParallelQuery = source as OrderedParallelQuery<TOutput>;
				if (orderedParallelQuery != null)
				{
					queryOperator = orderedParallelQuery.SortOperator;
				}
				else
				{
					queryOperator = new ScanQueryOperator<TOutput>(source);
				}
			}
			return queryOperator;
		}

		// Token: 0x0400074C RID: 1868
		protected bool _outputOrdered;
	}
}
