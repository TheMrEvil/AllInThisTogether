using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000108 RID: 264
	internal class OrderPreservingPipeliningMergeHelper<TOutput, TKey> : IMergeHelper<!0>
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0001DE68 File Offset: 0x0001C068
		internal OrderPreservingPipeliningMergeHelper(PartitionedStream<TOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, bool autoBuffered, int queryId, IComparer<TKey> keyComparer)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._taskScheduler = taskScheduler;
			this._autoBuffered = autoBuffered;
			int partitionCount = this._partitions.PartitionCount;
			this._buffers = new Queue<Pair<TKey, TOutput>>[partitionCount];
			this._producerDone = new bool[partitionCount];
			this._consumerWaiting = new bool[partitionCount];
			this._producerWaiting = new bool[partitionCount];
			this._bufferLocks = new object[partitionCount];
			if (keyComparer == Util.GetDefaultComparer<int>())
			{
				this._producerComparer = (IComparer<Producer<TKey>>)new ProducerComparerInt();
				return;
			}
			this._producerComparer = new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.ProducerComparer(keyComparer);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001DF10 File Offset: 0x0001C110
		void IMergeHelper<!0>.Execute()
		{
			OrderPreservingPipeliningSpoolingTask<TOutput, TKey>.Spool(this._taskGroupState, this._partitions, this._consumerWaiting, this._producerWaiting, this._producerDone, this._buffers, this._bufferLocks, this._taskScheduler, this._autoBuffered);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001DF58 File Offset: 0x0001C158
		IEnumerator<TOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.OrderedPipeliningMergeEnumerator(this, this._producerComparer);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001DF66 File Offset: 0x0001C166
		[ExcludeFromCodeCoverage]
		public TOutput[] GetResultsAsArray()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400060D RID: 1549
		private readonly QueryTaskGroupState _taskGroupState;

		// Token: 0x0400060E RID: 1550
		private readonly PartitionedStream<TOutput, TKey> _partitions;

		// Token: 0x0400060F RID: 1551
		private readonly TaskScheduler _taskScheduler;

		// Token: 0x04000610 RID: 1552
		private readonly bool _autoBuffered;

		// Token: 0x04000611 RID: 1553
		private readonly Queue<Pair<TKey, TOutput>>[] _buffers;

		// Token: 0x04000612 RID: 1554
		private readonly bool[] _producerDone;

		// Token: 0x04000613 RID: 1555
		private readonly bool[] _producerWaiting;

		// Token: 0x04000614 RID: 1556
		private readonly bool[] _consumerWaiting;

		// Token: 0x04000615 RID: 1557
		private readonly object[] _bufferLocks;

		// Token: 0x04000616 RID: 1558
		private IComparer<Producer<TKey>> _producerComparer;

		// Token: 0x04000617 RID: 1559
		internal const int INITIAL_BUFFER_SIZE = 128;

		// Token: 0x04000618 RID: 1560
		internal const int STEAL_BUFFER_SIZE = 1024;

		// Token: 0x04000619 RID: 1561
		internal const int MAX_BUFFER_SIZE = 8192;

		// Token: 0x02000109 RID: 265
		private class ProducerComparer : IComparer<Producer<TKey>>
		{
			// Token: 0x060008BC RID: 2236 RVA: 0x0001DF6D File Offset: 0x0001C16D
			internal ProducerComparer(IComparer<TKey> keyComparer)
			{
				this._keyComparer = keyComparer;
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x0001DF7C File Offset: 0x0001C17C
			public int Compare(Producer<TKey> x, Producer<TKey> y)
			{
				return this._keyComparer.Compare(y.MaxKey, x.MaxKey);
			}

			// Token: 0x0400061A RID: 1562
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x0200010A RID: 266
		private class OrderedPipeliningMergeEnumerator : MergeEnumerator<TOutput>
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x0001DF98 File Offset: 0x0001C198
			internal OrderedPipeliningMergeEnumerator(OrderPreservingPipeliningMergeHelper<TOutput, TKey> mergeHelper, IComparer<Producer<TKey>> producerComparer) : base(mergeHelper._taskGroupState)
			{
				int partitionCount = mergeHelper._partitions.PartitionCount;
				this._mergeHelper = mergeHelper;
				this._producerHeap = new FixedMaxHeap<Producer<TKey>>(partitionCount, producerComparer);
				this._privateBuffer = new Queue<Pair<TKey, TOutput>>[partitionCount];
				this._producerNextElement = new TOutput[partitionCount];
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001DFEC File Offset: 0x0001C1EC
			public override TOutput Current
			{
				get
				{
					int producerIndex = this._producerHeap.MaxValue.ProducerIndex;
					return this._producerNextElement[producerIndex];
				}
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x0001E018 File Offset: 0x0001C218
			public override bool MoveNext()
			{
				if (!this._initialized)
				{
					this._initialized = true;
					for (int i = 0; i < this._mergeHelper._partitions.PartitionCount; i++)
					{
						Pair<TKey, TOutput> pair = default(Pair<TKey, TOutput>);
						if (this.TryWaitForElement(i, ref pair))
						{
							this._producerHeap.Insert(new Producer<TKey>(pair.First, i));
							this._producerNextElement[i] = pair.Second;
						}
						else
						{
							this.ThrowIfInTearDown();
						}
					}
				}
				else
				{
					if (this._producerHeap.Count == 0)
					{
						return false;
					}
					int producerIndex = this._producerHeap.MaxValue.ProducerIndex;
					Pair<TKey, TOutput> pair2 = default(Pair<TKey, TOutput>);
					if (this.TryGetPrivateElement(producerIndex, ref pair2) || this.TryWaitForElement(producerIndex, ref pair2))
					{
						this._producerHeap.ReplaceMax(new Producer<TKey>(pair2.First, producerIndex));
						this._producerNextElement[producerIndex] = pair2.Second;
					}
					else
					{
						this.ThrowIfInTearDown();
						this._producerHeap.RemoveMax();
					}
				}
				return this._producerHeap.Count > 0;
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x0001E124 File Offset: 0x0001C324
			private void ThrowIfInTearDown()
			{
				if (this._mergeHelper._taskGroupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					try
					{
						object[] bufferLocks = this._mergeHelper._bufferLocks;
						for (int i = 0; i < bufferLocks.Length; i++)
						{
							object obj = bufferLocks[i];
							lock (obj)
							{
								Monitor.Pulse(bufferLocks[i]);
							}
						}
						this._taskGroupState.QueryEnd(false);
					}
					finally
					{
						this._producerHeap.Clear();
					}
				}
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
			private bool TryWaitForElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this._mergeHelper._buffers[producer];
				object obj = this._mergeHelper._bufferLocks[producer];
				object obj2 = obj;
				lock (obj2)
				{
					if (queue.Count == 0)
					{
						if (this._mergeHelper._producerDone[producer])
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
						this._mergeHelper._consumerWaiting[producer] = true;
						Monitor.Wait(obj);
						if (queue.Count == 0)
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
					}
					if (this._mergeHelper._producerWaiting[producer])
					{
						Monitor.Pulse(obj);
						this._mergeHelper._producerWaiting[producer] = false;
					}
					if (queue.Count < 1024)
					{
						element = queue.Dequeue();
						return true;
					}
					this._privateBuffer[producer] = this._mergeHelper._buffers[producer];
					this._mergeHelper._buffers[producer] = new Queue<Pair<TKey, TOutput>>(128);
				}
				this.TryGetPrivateElement(producer, ref element);
				return true;
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
			private bool TryGetPrivateElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this._privateBuffer[producer];
				if (queue != null)
				{
					if (queue.Count > 0)
					{
						element = queue.Dequeue();
						return true;
					}
					this._privateBuffer[producer] = null;
				}
				return false;
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x0001E320 File Offset: 0x0001C520
			public override void Dispose()
			{
				int num = this._mergeHelper._buffers.Length;
				for (int i = 0; i < num; i++)
				{
					object obj = this._mergeHelper._bufferLocks[i];
					object obj2 = obj;
					lock (obj2)
					{
						if (this._mergeHelper._producerWaiting[i])
						{
							Monitor.Pulse(obj);
						}
					}
				}
				base.Dispose();
			}

			// Token: 0x0400061B RID: 1563
			private OrderPreservingPipeliningMergeHelper<TOutput, TKey> _mergeHelper;

			// Token: 0x0400061C RID: 1564
			private readonly FixedMaxHeap<Producer<TKey>> _producerHeap;

			// Token: 0x0400061D RID: 1565
			private readonly TOutput[] _producerNextElement;

			// Token: 0x0400061E RID: 1566
			private readonly Queue<Pair<TKey, TOutput>>[] _privateBuffer;

			// Token: 0x0400061F RID: 1567
			private bool _initialized;
		}
	}
}
