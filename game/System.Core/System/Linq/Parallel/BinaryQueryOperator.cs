using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000137 RID: 311
	internal abstract class BinaryQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x00021707 File Offset: 0x0001F907
		internal BinaryQueryOperator(ParallelQuery<TLeftInput> leftChild, ParallelQuery<TRightInput> rightChild) : this(QueryOperator<TLeftInput>.AsQueryOperator(leftChild), QueryOperator<TRightInput>.AsQueryOperator(rightChild))
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002171C File Offset: 0x0001F91C
		internal BinaryQueryOperator(QueryOperator<TLeftInput> leftChild, QueryOperator<TRightInput> rightChild) : base(false, leftChild.SpecifiedQuerySettings.Merge(rightChild.SpecifiedQuerySettings))
		{
			this._leftChild = leftChild;
			this._rightChild = rightChild;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00021759 File Offset: 0x0001F959
		internal QueryOperator<TLeftInput> LeftChild
		{
			get
			{
				return this._leftChild;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00021761 File Offset: 0x0001F961
		internal QueryOperator<TRightInput> RightChild
		{
			get
			{
				return this._rightChild;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00021769 File Offset: 0x0001F969
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00021771 File Offset: 0x0001F971
		protected void SetOrdinalIndex(OrdinalIndexState indexState)
		{
			this._indexState = indexState;
		}

		// Token: 0x06000962 RID: 2402
		public abstract void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings);

		// Token: 0x040006DE RID: 1758
		private readonly QueryOperator<TLeftInput> _leftChild;

		// Token: 0x040006DF RID: 1759
		private readonly QueryOperator<TRightInput> _rightChild;

		// Token: 0x040006E0 RID: 1760
		private OrdinalIndexState _indexState = OrdinalIndexState.Shuffled;

		// Token: 0x02000138 RID: 312
		internal class BinaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06000963 RID: 2403 RVA: 0x0002177A File Offset: 0x0001F97A
			internal BinaryQueryOperatorResults(QueryResults<TLeftInput> leftChildQueryResults, QueryResults<TRightInput> rightChildQueryResults, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this._leftChildQueryResults = leftChildQueryResults;
				this._rightChildQueryResults = rightChildQueryResults;
				this._op = op;
				this._settings = settings;
				this._preferStriping = preferStriping;
			}

			// Token: 0x06000964 RID: 2404 RVA: 0x000217A8 File Offset: 0x0001F9A8
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				if (this._settings.ExecutionMode.Value == ParallelExecutionMode.Default && this._op.LimitsParallelism)
				{
					PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(this._op.AsSequentialQuery(this._settings.CancellationState.ExternalCancellationToken), this._settings.DegreeOfParallelism.Value, this._preferStriping);
					recipient.Receive<int>(partitionedStream);
					return;
				}
				if (this.IsIndexible)
				{
					PartitionedStream<TOutput, int> partitionedStream2 = ExchangeUtilities.PartitionDataSource<TOutput>(this, this._settings.DegreeOfParallelism.Value, this._preferStriping);
					recipient.Receive<int>(partitionedStream2);
					return;
				}
				this._leftChildQueryResults.GivePartitionedStream(new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.LeftChildResultsRecipient(recipient, this, this._preferStriping, this._settings));
			}

			// Token: 0x040006E1 RID: 1761
			protected QueryResults<TLeftInput> _leftChildQueryResults;

			// Token: 0x040006E2 RID: 1762
			protected QueryResults<TRightInput> _rightChildQueryResults;

			// Token: 0x040006E3 RID: 1763
			private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> _op;

			// Token: 0x040006E4 RID: 1764
			private QuerySettings _settings;

			// Token: 0x040006E5 RID: 1765
			private bool _preferStriping;

			// Token: 0x02000139 RID: 313
			private class LeftChildResultsRecipient : IPartitionedStreamRecipient<TLeftInput>
			{
				// Token: 0x06000965 RID: 2405 RVA: 0x00021867 File Offset: 0x0001FA67
				internal LeftChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults results, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._results = results;
					this._preferStriping = preferStriping;
					this._settings = settings;
				}

				// Token: 0x06000966 RID: 2406 RVA: 0x0002188C File Offset: 0x0001FA8C
				public void Receive<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> source)
				{
					BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey> recipient = new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults.RightChildResultsRecipient<TLeftKey>(this._outputRecipient, this._results._op, source, this._preferStriping, this._settings);
					this._results._rightChildQueryResults.GivePartitionedStream(recipient);
				}

				// Token: 0x040006E6 RID: 1766
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x040006E7 RID: 1767
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults _results;

				// Token: 0x040006E8 RID: 1768
				private bool _preferStriping;

				// Token: 0x040006E9 RID: 1769
				private QuerySettings _settings;
			}

			// Token: 0x0200013A RID: 314
			private class RightChildResultsRecipient<TLeftKey> : IPartitionedStreamRecipient<TRightInput>
			{
				// Token: 0x06000967 RID: 2407 RVA: 0x000218CE File Offset: 0x0001FACE
				internal RightChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, BinaryQueryOperator<TLeftInput, TRightInput, TOutput> op, PartitionedStream<TLeftInput, TLeftKey> leftPartitionedStream, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._op = op;
					this._preferStriping = preferStriping;
					this._leftPartitionedStream = leftPartitionedStream;
					this._settings = settings;
				}

				// Token: 0x06000968 RID: 2408 RVA: 0x000218FB File Offset: 0x0001FAFB
				public void Receive<TRightKey>(PartitionedStream<TRightInput, TRightKey> rightPartitionedStream)
				{
					this._op.WrapPartitionedStream<TLeftKey, TRightKey>(this._leftPartitionedStream, rightPartitionedStream, this._outputRecipient, this._preferStriping, this._settings);
				}

				// Token: 0x040006EA RID: 1770
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x040006EB RID: 1771
				private PartitionedStream<TLeftInput, TLeftKey> _leftPartitionedStream;

				// Token: 0x040006EC RID: 1772
				private BinaryQueryOperator<TLeftInput, TRightInput, TOutput> _op;

				// Token: 0x040006ED RID: 1773
				private bool _preferStriping;

				// Token: 0x040006EE RID: 1774
				private QuerySettings _settings;
			}
		}
	}
}
