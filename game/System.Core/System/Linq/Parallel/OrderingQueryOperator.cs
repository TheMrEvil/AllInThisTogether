using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017E RID: 382
	internal sealed class OrderingQueryOperator<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00024863 File Offset: 0x00022A63
		public OrderingQueryOperator(QueryOperator<TSource> child, bool orderOn) : base(orderOn, child.SpecifiedQuerySettings)
		{
			this._child = child;
			this._ordinalIndexState = this._child.OrdinalIndexState;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002488A File Offset: 0x00022A8A
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this._child.Open(settings, preferStriping);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002489C File Offset: 0x00022A9C
		internal override IEnumerator<TSource> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			ScanQueryOperator<TSource> scanQueryOperator = this._child as ScanQueryOperator<TSource>;
			if (scanQueryOperator != null)
			{
				return scanQueryOperator.Data.GetEnumerator();
			}
			return base.GetEnumerator(mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000248CC File Offset: 0x00022ACC
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this._child.AsSequentialQuery(token);
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000248DA File Offset: 0x00022ADA
		internal override bool LimitsParallelism
		{
			get
			{
				return this._child.LimitsParallelism;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000248E7 File Offset: 0x00022AE7
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._ordinalIndexState;
			}
		}

		// Token: 0x04000729 RID: 1833
		private QueryOperator<TSource> _child;

		// Token: 0x0400072A RID: 1834
		private OrdinalIndexState _ordinalIndexState;
	}
}
