using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000106 RID: 262
	internal class MergeExecutor<TInputOutput> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00002162 File Offset: 0x00000362
		private MergeExecutor()
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001DCCC File Offset: 0x0001BECC
		internal static MergeExecutor<TInputOutput> Execute<TKey>(PartitionedStream<TInputOutput, TKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, bool isOrdered, CancellationState cancellationState, int queryId)
		{
			MergeExecutor<TInputOutput> mergeExecutor = new MergeExecutor<TInputOutput>();
			if (isOrdered && !ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered && !partitions.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
				{
					bool autoBuffered = options == ParallelMergeOptions.AutoBuffered;
					if (partitions.PartitionCount > 1)
					{
						mergeExecutor._mergeHelper = new OrderPreservingPipeliningMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, autoBuffered, queryId, partitions.KeyComparer);
					}
					else
					{
						mergeExecutor._mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, false, options, taskScheduler, cancellationState, queryId);
					}
				}
				else
				{
					mergeExecutor._mergeHelper = new OrderPreservingMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, queryId);
				}
			}
			else
			{
				mergeExecutor._mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, ignoreOutput, options, taskScheduler, cancellationState, queryId);
			}
			mergeExecutor.Execute();
			return mergeExecutor;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001DD62 File Offset: 0x0001BF62
		private void Execute()
		{
			this._mergeHelper.Execute();
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001DD6F File Offset: 0x0001BF6F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001DD77 File Offset: 0x0001BF77
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this._mergeHelper.GetEnumerator();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001DD84 File Offset: 0x0001BF84
		internal TInputOutput[] GetResultsAsArray()
		{
			return this._mergeHelper.GetResultsAsArray();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001DD94 File Offset: 0x0001BF94
		internal static AsynchronousChannel<TInputOutput>[] MakeAsynchronousChannels(int partitionCount, ParallelMergeOptions options, IntValueEvent consumerEvent, CancellationToken cancellationToken)
		{
			AsynchronousChannel<TInputOutput>[] array = new AsynchronousChannel<TInputOutput>[partitionCount];
			int chunkSize = 0;
			if (options == ParallelMergeOptions.NotBuffered)
			{
				chunkSize = 1;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AsynchronousChannel<TInputOutput>(i, chunkSize, cancellationToken, consumerEvent);
			}
			return array;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		internal static SynchronousChannel<TInputOutput>[] MakeSynchronousChannels(int partitionCount)
		{
			SynchronousChannel<TInputOutput>[] array = new SynchronousChannel<TInputOutput>[partitionCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SynchronousChannel<TInputOutput>();
			}
			return array;
		}

		// Token: 0x04000608 RID: 1544
		private IMergeHelper<TInputOutput> _mergeHelper;
	}
}
