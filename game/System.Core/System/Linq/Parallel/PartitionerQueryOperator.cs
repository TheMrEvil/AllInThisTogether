using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000182 RID: 386
	internal class PartitionerQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x000249C3 File Offset: 0x00022BC3
		internal PartitionerQueryOperator(Partitioner<TElement> partitioner) : base(false, QuerySettings.Empty)
		{
			this._partitioner = partitioner;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000249D8 File Offset: 0x00022BD8
		internal bool Orderable
		{
			get
			{
				return this._partitioner is OrderablePartitioner<TElement>;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000249E8 File Offset: 0x00022BE8
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			return new PartitionerQueryOperator<TElement>.PartitionerQueryOperatorResults(this._partitioner, settings);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000249F6 File Offset: 0x00022BF6
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TElement> enumerator = this._partitioner.GetPartitions(1)[0])
			{
				while (enumerator.MoveNext())
				{
					!0 ! = enumerator.Current;
					yield return !;
				}
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00024A06 File Offset: 0x00022C06
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return PartitionerQueryOperator<TElement>.GetOrdinalIndexState(this._partitioner);
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00024A14 File Offset: 0x00022C14
		internal static OrdinalIndexState GetOrdinalIndexState(Partitioner<TElement> partitioner)
		{
			OrderablePartitioner<TElement> orderablePartitioner = partitioner as OrderablePartitioner<TElement>;
			if (orderablePartitioner == null)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (!orderablePartitioner.KeysOrderedInEachPartition)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (orderablePartitioner.KeysNormalized)
			{
				return OrdinalIndexState.Correct;
			}
			return OrdinalIndexState.Increasing;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000739 RID: 1849
		private Partitioner<TElement> _partitioner;

		// Token: 0x02000183 RID: 387
		private class PartitionerQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06000A4F RID: 2639 RVA: 0x00024A42 File Offset: 0x00022C42
			internal PartitionerQueryOperatorResults(Partitioner<TElement> partitioner, QuerySettings settings)
			{
				this._partitioner = partitioner;
				this._settings = settings;
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x00024A58 File Offset: 0x00022C58
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				int value = this._settings.DegreeOfParallelism.Value;
				OrderablePartitioner<TElement> orderablePartitioner = this._partitioner as OrderablePartitioner<TElement>;
				OrdinalIndexState indexState = (orderablePartitioner != null) ? PartitionerQueryOperator<TElement>.GetOrdinalIndexState(orderablePartitioner) : OrdinalIndexState.Shuffled;
				PartitionedStream<TElement, int> partitionedStream = new PartitionedStream<TElement, int>(value, Util.GetDefaultComparer<int>(), indexState);
				if (orderablePartitioner != null)
				{
					IList<IEnumerator<KeyValuePair<long, TElement>>> orderablePartitions = orderablePartitioner.GetOrderablePartitions(value);
					if (orderablePartitions == null)
					{
						throw new InvalidOperationException("Partitioner returned null instead of a list of partitions.");
					}
					if (orderablePartitions.Count != value)
					{
						throw new InvalidOperationException("Partitioner returned a wrong number of partitions.");
					}
					for (int i = 0; i < value; i++)
					{
						IEnumerator<KeyValuePair<long, TElement>> enumerator = orderablePartitions[i];
						if (enumerator == null)
						{
							throw new InvalidOperationException("Partitioner returned a null partition.");
						}
						partitionedStream[i] = new PartitionerQueryOperator<TElement>.OrderablePartitionerEnumerator(enumerator);
					}
				}
				else
				{
					IList<IEnumerator<TElement>> partitions = this._partitioner.GetPartitions(value);
					if (partitions == null)
					{
						throw new InvalidOperationException("Partitioner returned null instead of a list of partitions.");
					}
					if (partitions.Count != value)
					{
						throw new InvalidOperationException("Partitioner returned a wrong number of partitions.");
					}
					for (int j = 0; j < value; j++)
					{
						IEnumerator<TElement> enumerator2 = partitions[j];
						if (enumerator2 == null)
						{
							throw new InvalidOperationException("Partitioner returned a null partition.");
						}
						partitionedStream[j] = new PartitionerQueryOperator<TElement>.PartitionerEnumerator(enumerator2);
					}
				}
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x0400073A RID: 1850
			private Partitioner<TElement> _partitioner;

			// Token: 0x0400073B RID: 1851
			private QuerySettings _settings;
		}

		// Token: 0x02000184 RID: 388
		private class OrderablePartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06000A51 RID: 2641 RVA: 0x00024B7D File Offset: 0x00022D7D
			internal OrderablePartitionerEnumerator(IEnumerator<KeyValuePair<long, TElement>> sourceEnumerator)
			{
				this._sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x00024B8C File Offset: 0x00022D8C
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this._sourceEnumerator.MoveNext())
				{
					return false;
				}
				KeyValuePair<long, TElement> keyValuePair = this._sourceEnumerator.Current;
				currentElement = keyValuePair.Value;
				currentKey = checked((int)keyValuePair.Key);
				return true;
			}

			// Token: 0x06000A53 RID: 2643 RVA: 0x00024BCC File Offset: 0x00022DCC
			protected override void Dispose(bool disposing)
			{
				this._sourceEnumerator.Dispose();
			}

			// Token: 0x0400073C RID: 1852
			private IEnumerator<KeyValuePair<long, TElement>> _sourceEnumerator;
		}

		// Token: 0x02000185 RID: 389
		private class PartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06000A54 RID: 2644 RVA: 0x00024BD9 File Offset: 0x00022DD9
			internal PartitionerEnumerator(IEnumerator<TElement> sourceEnumerator)
			{
				this._sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06000A55 RID: 2645 RVA: 0x00024BE8 File Offset: 0x00022DE8
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this._sourceEnumerator.MoveNext())
				{
					return false;
				}
				currentElement = this._sourceEnumerator.Current;
				currentKey = 0;
				return true;
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x00024C0E File Offset: 0x00022E0E
			protected override void Dispose(bool disposing)
			{
				this._sourceEnumerator.Dispose();
			}

			// Token: 0x0400073D RID: 1853
			private IEnumerator<TElement> _sourceEnumerator;
		}

		// Token: 0x02000186 RID: 390
		[CompilerGenerated]
		private sealed class <AsSequentialQuery>d__5 : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000A57 RID: 2647 RVA: 0x00024C1B File Offset: 0x00022E1B
			[DebuggerHidden]
			public <AsSequentialQuery>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x00024C38 File Offset: 0x00022E38
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x00024C70 File Offset: 0x00022E70
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					PartitionerQueryOperator<TElement> partitionerQueryOperator = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = partitionerQueryOperator._partitioner.GetPartitions(1)[0];
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						this.<>2__current = enumerator.Current;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x00024D1C File Offset: 0x00022F1C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00024D38 File Offset: 0x00022F38
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00024D40 File Offset: 0x00022F40
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000A5E RID: 2654 RVA: 0x00024D50 File Offset: 0x00022F50
			[DebuggerHidden]
			IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
			{
				PartitionerQueryOperator<TElement>.<AsSequentialQuery>d__5 <AsSequentialQuery>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<AsSequentialQuery>d__ = this;
				}
				else
				{
					<AsSequentialQuery>d__ = new PartitionerQueryOperator<TElement>.<AsSequentialQuery>d__5(0);
					<AsSequentialQuery>d__.<>4__this = this;
				}
				return <AsSequentialQuery>d__;
			}

			// Token: 0x06000A5F RID: 2655 RVA: 0x00024D93 File Offset: 0x00022F93
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TElement>.GetEnumerator();
			}

			// Token: 0x0400073E RID: 1854
			private int <>1__state;

			// Token: 0x0400073F RID: 1855
			private TElement <>2__current;

			// Token: 0x04000740 RID: 1856
			private int <>l__initialThreadId;

			// Token: 0x04000741 RID: 1857
			public PartitionerQueryOperator<TElement> <>4__this;

			// Token: 0x04000742 RID: 1858
			private IEnumerator<TElement> <enumerator>5__2;
		}
	}
}
