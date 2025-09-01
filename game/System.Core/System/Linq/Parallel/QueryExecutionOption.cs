using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200017F RID: 383
	internal class QueryExecutionOption<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x000248EF File Offset: 0x00022AEF
		internal QueryExecutionOption(QueryOperator<TSource> source, QuerySettings settings) : base(source.OutputOrdered, settings.Merge(source.SpecifiedQuerySettings))
		{
			this._child = source;
			this._indexState = this._child.OrdinalIndexState;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00024922 File Offset: 0x00022B22
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this._child.Open(settings, preferStriping);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00024931 File Offset: 0x00022B31
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this._child.AsSequentialQuery(token);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002493F File Offset: 0x00022B3F
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00024947 File Offset: 0x00022B47
		internal override bool LimitsParallelism
		{
			get
			{
				return this._child.LimitsParallelism;
			}
		}

		// Token: 0x0400072B RID: 1835
		private QueryOperator<TSource> _child;

		// Token: 0x0400072C RID: 1836
		private OrdinalIndexState _indexState;
	}
}
