using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000103 RID: 259
	internal class DefaultMergeHelper<TInputOutput, TIgnoreKey> : IMergeHelper<TInputOutput>
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0001DA78 File Offset: 0x0001BC78
		internal DefaultMergeHelper(PartitionedStream<TInputOutput, TIgnoreKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._taskScheduler = taskScheduler;
			this._ignoreOutput = ignoreOutput;
			IntValueEvent consumerEvent = new IntValueEvent();
			if (!ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered)
				{
					if (partitions.PartitionCount > 1)
					{
						this._asyncChannels = MergeExecutor<TInputOutput>.MakeAsynchronousChannels(partitions.PartitionCount, options, consumerEvent, cancellationState.MergedCancellationToken);
						this._channelEnumerator = new AsynchronousChannelMergeEnumerator<TInputOutput>(this._taskGroupState, this._asyncChannels, consumerEvent);
						return;
					}
					this._channelEnumerator = ExceptionAggregator.WrapQueryEnumerator<TInputOutput, TIgnoreKey>(partitions[0], this._taskGroupState.CancellationState).GetEnumerator();
					return;
				}
				else
				{
					this._syncChannels = MergeExecutor<TInputOutput>.MakeSynchronousChannels(partitions.PartitionCount);
					this._channelEnumerator = new SynchronousChannelMergeEnumerator<TInputOutput>(this._taskGroupState, this._syncChannels);
				}
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001DB48 File Offset: 0x0001BD48
		void IMergeHelper<!0>.Execute()
		{
			if (this._asyncChannels != null)
			{
				SpoolingTask.SpoolPipeline<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._asyncChannels, this._taskScheduler);
				return;
			}
			if (this._syncChannels != null)
			{
				SpoolingTask.SpoolStopAndGo<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._syncChannels, this._taskScheduler);
				return;
			}
			if (this._ignoreOutput)
			{
				SpoolingTask.SpoolForAll<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._taskScheduler);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		IEnumerator<TInputOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return this._channelEnumerator;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		public TInputOutput[] GetResultsAsArray()
		{
			if (this._syncChannels != null)
			{
				int num = 0;
				for (int i = 0; i < this._syncChannels.Length; i++)
				{
					num += this._syncChannels[i].Count;
				}
				TInputOutput[] array = new TInputOutput[num];
				int num2 = 0;
				for (int j = 0; j < this._syncChannels.Length; j++)
				{
					this._syncChannels[j].CopyTo(array, num2);
					num2 += this._syncChannels[j].Count;
				}
				return array;
			}
			List<TInputOutput> list = new List<TInputOutput>();
			foreach (!0 item in ((IMergeHelper<!0>)this))
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x04000600 RID: 1536
		private QueryTaskGroupState _taskGroupState;

		// Token: 0x04000601 RID: 1537
		private PartitionedStream<TInputOutput, TIgnoreKey> _partitions;

		// Token: 0x04000602 RID: 1538
		private AsynchronousChannel<TInputOutput>[] _asyncChannels;

		// Token: 0x04000603 RID: 1539
		private SynchronousChannel<TInputOutput>[] _syncChannels;

		// Token: 0x04000604 RID: 1540
		private IEnumerator<TInputOutput> _channelEnumerator;

		// Token: 0x04000605 RID: 1541
		private TaskScheduler _taskScheduler;

		// Token: 0x04000606 RID: 1542
		private bool _ignoreOutput;
	}
}
