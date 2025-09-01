using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019E RID: 414
	internal sealed class ForAllOperator<TInput> : UnaryQueryOperator<TInput, TInput>
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x00026680 File Offset: 0x00024880
		internal ForAllOperator(IEnumerable<TInput> child, Action<TInput> elementAction) : base(child)
		{
			this._elementAction = elementAction;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00026690 File Offset: 0x00024890
		internal void RunSynchronously()
		{
			Shared<bool> topLevelDisposedFlag = new Shared<bool>(false);
			CancellationTokenSource topLevelCancellationTokenSource = new CancellationTokenSource();
			QuerySettings querySettings = base.SpecifiedQuerySettings.WithPerExecutionSettings(topLevelCancellationTokenSource, topLevelDisposedFlag).WithDefaults();
			QueryLifecycle.LogicalQueryExecutionBegin(querySettings.QueryId);
			base.GetOpenedEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true, true, querySettings);
			querySettings.CleanStateAtQueryEnd();
			QueryLifecycle.LogicalQueryExecutionEnd(querySettings.QueryId);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00025C6F File Offset: 0x00023E6F
		internal override QueryResults<TInput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInput, TInput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000266F4 File Offset: 0x000248F4
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TInput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TInput, int> partitionedStream = new PartitionedStream<TInput, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ForAllOperator<TInput>.ForAllEnumerator<TKey>(inputStream[i], this._elementAction, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001DF66 File Offset: 0x0001C166
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TInput> AsSequentialQuery(CancellationToken token)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000792 RID: 1938
		private readonly Action<TInput> _elementAction;

		// Token: 0x0200019F RID: 415
		private class ForAllEnumerator<TKey> : QueryOperatorEnumerator<TInput, int>
		{
			// Token: 0x06000AF5 RID: 2805 RVA: 0x0002674D File Offset: 0x0002494D
			internal ForAllEnumerator(QueryOperatorEnumerator<TInput, TKey> source, Action<TInput> elementAction, CancellationToken cancellationToken)
			{
				this._source = source;
				this._elementAction = elementAction;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x0002676C File Offset: 0x0002496C
			internal override bool MoveNext(ref TInput currentElement, ref int currentKey)
			{
				TInput obj = default(TInput);
				TKey tkey = default(TKey);
				int num = 0;
				while (this._source.MoveNext(ref obj, ref tkey))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					this._elementAction(obj);
				}
				return false;
			}

			// Token: 0x06000AF7 RID: 2807 RVA: 0x000267C0 File Offset: 0x000249C0
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000793 RID: 1939
			private readonly QueryOperatorEnumerator<TInput, TKey> _source;

			// Token: 0x04000794 RID: 1940
			private readonly Action<TInput> _elementAction;

			// Token: 0x04000795 RID: 1941
			private CancellationToken _cancellationToken;
		}
	}
}
