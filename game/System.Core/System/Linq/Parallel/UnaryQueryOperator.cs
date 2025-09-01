using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001D0 RID: 464
	internal abstract class UnaryQueryOperator<TInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00029223 File Offset: 0x00027423
		internal UnaryQueryOperator(IEnumerable<TInput> child) : this(QueryOperator<TInput>.AsQueryOperator(child))
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00029231 File Offset: 0x00027431
		internal UnaryQueryOperator(IEnumerable<TInput> child, bool outputOrdered) : this(QueryOperator<TInput>.AsQueryOperator(child), outputOrdered)
		{
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00029240 File Offset: 0x00027440
		private UnaryQueryOperator(QueryOperator<TInput> child) : this(child, child.OutputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00029255 File Offset: 0x00027455
		internal UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered) : this(child, outputOrdered, child.SpecifiedQuerySettings)
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00029265 File Offset: 0x00027465
		private UnaryQueryOperator(QueryOperator<TInput> child, bool outputOrdered, QuerySettings settings) : base(outputOrdered, settings)
		{
			this._child = child;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002927D File Offset: 0x0002747D
		internal QueryOperator<TInput> Child
		{
			get
			{
				return this._child;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00029285 File Offset: 0x00027485
		internal sealed override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002928D File Offset: 0x0002748D
		protected void SetOrdinalIndexState(OrdinalIndexState indexState)
		{
			this._indexState = indexState;
		}

		// Token: 0x06000BB1 RID: 2993
		internal abstract void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings);

		// Token: 0x0400082D RID: 2093
		private readonly QueryOperator<TInput> _child;

		// Token: 0x0400082E RID: 2094
		private OrdinalIndexState _indexState = OrdinalIndexState.Shuffled;

		// Token: 0x020001D1 RID: 465
		internal class UnaryQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06000BB2 RID: 2994 RVA: 0x00029296 File Offset: 0x00027496
			internal UnaryQueryOperatorResults(QueryResults<TInput> childQueryResults, UnaryQueryOperator<TInput, TOutput> op, QuerySettings settings, bool preferStriping)
			{
				this._childQueryResults = childQueryResults;
				this._op = op;
				this._settings = settings;
				this._preferStriping = preferStriping;
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x000292BC File Offset: 0x000274BC
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
				this._childQueryResults.GivePartitionedStream(new UnaryQueryOperator<TInput, TOutput>.UnaryQueryOperatorResults.ChildResultsRecipient(recipient, this._op, this._preferStriping, this._settings));
			}

			// Token: 0x0400082F RID: 2095
			protected QueryResults<TInput> _childQueryResults;

			// Token: 0x04000830 RID: 2096
			private UnaryQueryOperator<TInput, TOutput> _op;

			// Token: 0x04000831 RID: 2097
			private QuerySettings _settings;

			// Token: 0x04000832 RID: 2098
			private bool _preferStriping;

			// Token: 0x020001D2 RID: 466
			private class ChildResultsRecipient : IPartitionedStreamRecipient<TInput>
			{
				// Token: 0x06000BB4 RID: 2996 RVA: 0x00029380 File Offset: 0x00027580
				internal ChildResultsRecipient(IPartitionedStreamRecipient<TOutput> outputRecipient, UnaryQueryOperator<TInput, TOutput> op, bool preferStriping, QuerySettings settings)
				{
					this._outputRecipient = outputRecipient;
					this._op = op;
					this._preferStriping = preferStriping;
					this._settings = settings;
				}

				// Token: 0x06000BB5 RID: 2997 RVA: 0x000293A5 File Offset: 0x000275A5
				public void Receive<TKey>(PartitionedStream<TInput, TKey> inputStream)
				{
					this._op.WrapPartitionedStream<TKey>(inputStream, this._outputRecipient, this._preferStriping, this._settings);
				}

				// Token: 0x04000833 RID: 2099
				private IPartitionedStreamRecipient<TOutput> _outputRecipient;

				// Token: 0x04000834 RID: 2100
				private UnaryQueryOperator<TInput, TOutput> _op;

				// Token: 0x04000835 RID: 2101
				private bool _preferStriping;

				// Token: 0x04000836 RID: 2102
				private QuerySettings _settings;
			}
		}
	}
}
