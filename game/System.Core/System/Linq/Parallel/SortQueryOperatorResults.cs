using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001C5 RID: 453
	internal class SortQueryOperatorResults<TInputOutput, TSortKey> : QueryResults<TInputOutput>
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00028427 File Offset: 0x00026627
		internal SortQueryOperatorResults(QueryResults<TInputOutput> childQueryResults, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings)
		{
			this._childQueryResults = childQueryResults;
			this._op = op;
			this._settings = settings;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00028444 File Offset: 0x00026644
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<TInputOutput> recipient)
		{
			this._childQueryResults.GivePartitionedStream(new SortQueryOperatorResults<TInputOutput, TSortKey>.ChildResultsRecipient(recipient, this._op, this._settings));
		}

		// Token: 0x040007FF RID: 2047
		protected QueryResults<TInputOutput> _childQueryResults;

		// Token: 0x04000800 RID: 2048
		private SortQueryOperator<TInputOutput, TSortKey> _op;

		// Token: 0x04000801 RID: 2049
		private QuerySettings _settings;

		// Token: 0x020001C6 RID: 454
		private class ChildResultsRecipient : IPartitionedStreamRecipient<TInputOutput>
		{
			// Token: 0x06000B82 RID: 2946 RVA: 0x00028463 File Offset: 0x00026663
			internal ChildResultsRecipient(IPartitionedStreamRecipient<TInputOutput> outputRecipient, SortQueryOperator<TInputOutput, TSortKey> op, QuerySettings settings)
			{
				this._outputRecipient = outputRecipient;
				this._op = op;
				this._settings = settings;
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x00028480 File Offset: 0x00026680
			public void Receive<TKey>(PartitionedStream<TInputOutput, TKey> childPartitionedStream)
			{
				this._op.WrapPartitionedStream<TKey>(childPartitionedStream, this._outputRecipient, false, this._settings);
			}

			// Token: 0x04000802 RID: 2050
			private IPartitionedStreamRecipient<TInputOutput> _outputRecipient;

			// Token: 0x04000803 RID: 2051
			private SortQueryOperator<TInputOutput, TSortKey> _op;

			// Token: 0x04000804 RID: 2052
			private QuerySettings _settings;
		}
	}
}
