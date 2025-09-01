using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000134 RID: 308
	internal sealed class ZipQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0002126A File Offset: 0x0001F46A
		internal ZipQueryOperator(ParallelQuery<TLeftInput> leftChildSource, ParallelQuery<TRightInput> rightChildSource, Func<TLeftInput, TRightInput, TOutput> resultSelector) : this(QueryOperator<TLeftInput>.AsQueryOperator(leftChildSource), QueryOperator<TRightInput>.AsQueryOperator(rightChildSource), resultSelector)
		{
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00021280 File Offset: 0x0001F480
		private ZipQueryOperator(QueryOperator<TLeftInput> left, QueryOperator<TRightInput> right, Func<TLeftInput, TRightInput, TOutput> resultSelector) : base(left.SpecifiedQuerySettings.Merge(right.SpecifiedQuerySettings))
		{
			this._leftChild = left;
			this._rightChild = right;
			this._resultSelector = resultSelector;
			this._outputOrdered = (this._leftChild.OutputOrdered || this._rightChild.OutputOrdered);
			OrdinalIndexState ordinalIndexState = this._leftChild.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = this._rightChild.OrdinalIndexState;
			this._prematureMergeLeft = (ordinalIndexState > OrdinalIndexState.Indexable);
			this._prematureMergeRight = (ordinalIndexState2 > OrdinalIndexState.Indexable);
			this._limitsParallelism = ((this._prematureMergeLeft && ordinalIndexState != OrdinalIndexState.Shuffled) || (this._prematureMergeRight && ordinalIndexState2 != OrdinalIndexState.Shuffled));
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00021330 File Offset: 0x0001F530
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> queryResults = this._leftChild.Open(settings, preferStriping);
			QueryResults<TRightInput> queryResults2 = this._rightChild.Open(settings, preferStriping);
			int value = settings.DegreeOfParallelism.Value;
			if (this._prematureMergeLeft)
			{
				PartitionedStreamMerger<TLeftInput> partitionedStreamMerger = new PartitionedStreamMerger<TLeftInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this._leftChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				queryResults = new ListQueryResults<TLeftInput>(partitionedStreamMerger.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			if (this._prematureMergeRight)
			{
				PartitionedStreamMerger<TRightInput> partitionedStreamMerger2 = new PartitionedStreamMerger<TRightInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this._rightChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults2.GivePartitionedStream(partitionedStreamMerger2);
				queryResults2 = new ListQueryResults<TRightInput>(partitionedStreamMerger2.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			return new ZipQueryOperator<TLeftInput, TRightInput, TOutput>.ZipQueryOperatorResults(queryResults, queryResults2, this._resultSelector, value, preferStriping);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00021412 File Offset: 0x0001F612
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TLeftInput> leftEnumerator = this._leftChild.AsSequentialQuery(token).GetEnumerator())
			{
				using (IEnumerator<TRightInput> rightEnumerator = this._rightChild.AsSequentialQuery(token).GetEnumerator())
				{
					while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
					{
						yield return this._resultSelector(leftEnumerator.Current, rightEnumerator.Current);
					}
				}
				IEnumerator<TRightInput> rightEnumerator = null;
			}
			IEnumerator<TLeftInput> leftEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return OrdinalIndexState.Indexable;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00021429 File Offset: 0x0001F629
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x040006CA RID: 1738
		private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

		// Token: 0x040006CB RID: 1739
		private readonly QueryOperator<TLeftInput> _leftChild;

		// Token: 0x040006CC RID: 1740
		private readonly QueryOperator<TRightInput> _rightChild;

		// Token: 0x040006CD RID: 1741
		private readonly bool _prematureMergeLeft;

		// Token: 0x040006CE RID: 1742
		private readonly bool _prematureMergeRight;

		// Token: 0x040006CF RID: 1743
		private readonly bool _limitsParallelism;

		// Token: 0x02000135 RID: 309
		internal class ZipQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x0600094D RID: 2381 RVA: 0x00021434 File Offset: 0x0001F634
			internal ZipQueryOperatorResults(QueryResults<TLeftInput> leftChildResults, QueryResults<TRightInput> rightChildResults, Func<TLeftInput, TRightInput, TOutput> resultSelector, int partitionCount, bool preferStriping)
			{
				this._leftChildResults = leftChildResults;
				this._rightChildResults = rightChildResults;
				this._resultSelector = resultSelector;
				this._partitionCount = partitionCount;
				this._preferStriping = preferStriping;
				this._count = Math.Min(this._leftChildResults.Count, this._rightChildResults.Count);
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002148D File Offset: 0x0001F68D
			internal override int ElementsCount
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600094F RID: 2383 RVA: 0x00007E1D File Offset: 0x0000601D
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x00021495 File Offset: 0x0001F695
			internal override TOutput GetElement(int index)
			{
				return this._resultSelector(this._leftChildResults.GetElement(index), this._rightChildResults.GetElement(index));
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x000214BC File Offset: 0x0001F6BC
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(this, this._partitionCount, this._preferStriping);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x040006D0 RID: 1744
			private readonly QueryResults<TLeftInput> _leftChildResults;

			// Token: 0x040006D1 RID: 1745
			private readonly QueryResults<TRightInput> _rightChildResults;

			// Token: 0x040006D2 RID: 1746
			private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

			// Token: 0x040006D3 RID: 1747
			private readonly int _count;

			// Token: 0x040006D4 RID: 1748
			private readonly int _partitionCount;

			// Token: 0x040006D5 RID: 1749
			private readonly bool _preferStriping;
		}

		// Token: 0x02000136 RID: 310
		[CompilerGenerated]
		private sealed class <AsSequentialQuery>d__9 : IEnumerable<!2>, IEnumerable, IEnumerator<!2>, IDisposable, IEnumerator
		{
			// Token: 0x06000952 RID: 2386 RVA: 0x000214E3 File Offset: 0x0001F6E3
			[DebuggerHidden]
			public <AsSequentialQuery>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x00021500 File Offset: 0x0001F700
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x00021558 File Offset: 0x0001F758
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ZipQueryOperator<TLeftInput, TRightInput, TOutput> zipQueryOperator = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -4;
					}
					else
					{
						this.<>1__state = -1;
						leftEnumerator = zipQueryOperator._leftChild.AsSequentialQuery(token).GetEnumerator();
						this.<>1__state = -3;
						rightEnumerator = zipQueryOperator._rightChild.AsSequentialQuery(token).GetEnumerator();
						this.<>1__state = -4;
					}
					if (!leftEnumerator.MoveNext() || !rightEnumerator.MoveNext())
					{
						this.<>m__Finally2();
						rightEnumerator = null;
						this.<>m__Finally1();
						leftEnumerator = null;
						result = false;
					}
					else
					{
						this.<>2__current = zipQueryOperator._resultSelector(leftEnumerator.Current, rightEnumerator.Current);
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

			// Token: 0x06000955 RID: 2389 RVA: 0x00021660 File Offset: 0x0001F860
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (leftEnumerator != null)
				{
					leftEnumerator.Dispose();
				}
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x0002167C File Offset: 0x0001F87C
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (rightEnumerator != null)
				{
					rightEnumerator.Dispose();
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000957 RID: 2391 RVA: 0x00021699 File Offset: 0x0001F899
			TOutput IEnumerator<!2>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000958 RID: 2392 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x000216A1 File Offset: 0x0001F8A1
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x000216B0 File Offset: 0x0001F8B0
			[DebuggerHidden]
			IEnumerator<TOutput> IEnumerable<!2>.GetEnumerator()
			{
				ZipQueryOperator<TLeftInput, TRightInput, TOutput>.<AsSequentialQuery>d__9 <AsSequentialQuery>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<AsSequentialQuery>d__ = this;
				}
				else
				{
					<AsSequentialQuery>d__ = new ZipQueryOperator<TLeftInput, TRightInput, TOutput>.<AsSequentialQuery>d__9(0);
					<AsSequentialQuery>d__.<>4__this = this;
				}
				<AsSequentialQuery>d__.token = token;
				return <AsSequentialQuery>d__;
			}

			// Token: 0x0600095B RID: 2395 RVA: 0x000216FF File Offset: 0x0001F8FF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TOutput>.GetEnumerator();
			}

			// Token: 0x040006D6 RID: 1750
			private int <>1__state;

			// Token: 0x040006D7 RID: 1751
			private TOutput <>2__current;

			// Token: 0x040006D8 RID: 1752
			private int <>l__initialThreadId;

			// Token: 0x040006D9 RID: 1753
			public ZipQueryOperator<TLeftInput, TRightInput, TOutput> <>4__this;

			// Token: 0x040006DA RID: 1754
			private CancellationToken token;

			// Token: 0x040006DB RID: 1755
			public CancellationToken <>3__token;

			// Token: 0x040006DC RID: 1756
			private IEnumerator<TLeftInput> <leftEnumerator>5__2;

			// Token: 0x040006DD RID: 1757
			private IEnumerator<TRightInput> <rightEnumerator>5__3;
		}
	}
}
