using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200018E RID: 398
	internal sealed class ScanQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x000257A4 File Offset: 0x000239A4
		internal ScanQueryOperator(IEnumerable<TElement> data) : base(false, QuerySettings.Empty)
		{
			ParallelEnumerableWrapper<TElement> parallelEnumerableWrapper = data as ParallelEnumerableWrapper<TElement>;
			if (parallelEnumerableWrapper != null)
			{
				data = parallelEnumerableWrapper.WrappedEnumerable;
			}
			this._data = data;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000257D6 File Offset: 0x000239D6
		public IEnumerable<TElement> Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000257E0 File Offset: 0x000239E0
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			IList<TElement> list = this._data as IList<!0>;
			if (list != null)
			{
				return new ListQueryResults<TElement>(list, settings.DegreeOfParallelism.GetValueOrDefault(), preferStriping);
			}
			return new ScanQueryOperator<TElement>.ScanEnumerableQueryOperatorResults(this._data, settings);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002581F File Offset: 0x00023A1F
		internal override IEnumerator<TElement> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000257D6 File Offset: 0x000239D6
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			return this._data;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002582C File Offset: 0x00023A2C
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				if (!(this._data is IList<!0>))
				{
					return OrdinalIndexState.Correct;
				}
				return OrdinalIndexState.Indexable;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000759 RID: 1881
		private readonly IEnumerable<TElement> _data;

		// Token: 0x0200018F RID: 399
		private class ScanEnumerableQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06000AB4 RID: 2740 RVA: 0x0002583E File Offset: 0x00023A3E
			internal ScanEnumerableQueryOperatorResults(IEnumerable<TElement> data, QuerySettings settings)
			{
				this._data = data;
				this._settings = settings;
			}

			// Token: 0x06000AB5 RID: 2741 RVA: 0x00025854 File Offset: 0x00023A54
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				PartitionedStream<TElement, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TElement>(this._data, this._settings.DegreeOfParallelism.Value, false);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x0400075A RID: 1882
			private IEnumerable<TElement> _data;

			// Token: 0x0400075B RID: 1883
			private QuerySettings _settings;
		}
	}
}
