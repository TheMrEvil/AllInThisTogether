using System;
using System.Collections.Generic;
using System.Linq.Parallel;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a sorted, parallel sequence.</summary>
	/// <typeparam name="TSource">The type of elements in the source collection.</typeparam>
	// Token: 0x0200007D RID: 125
	public class OrderedParallelQuery<TSource> : ParallelQuery<TSource>
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00008095 File Offset: 0x00006295
		internal OrderedParallelQuery(QueryOperator<TSource> sortOp) : base(sortOp.SpecifiedQuerySettings)
		{
			this._sortOp = sortOp;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000080AA File Offset: 0x000062AA
		internal QueryOperator<TSource> SortOperator
		{
			get
			{
				return this._sortOp;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000080B2 File Offset: 0x000062B2
		internal IOrderedEnumerable<TSource> OrderedEnumerable
		{
			get
			{
				return (IOrderedEnumerable<!0>)this._sortOp;
			}
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x06000298 RID: 664 RVA: 0x000080BF File Offset: 0x000062BF
		public override IEnumerator<TSource> GetEnumerator()
		{
			return this._sortOp.GetEnumerator();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000235B File Offset: 0x0000055B
		internal OrderedParallelQuery()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040003B2 RID: 946
		private QueryOperator<TSource> _sortOp;
	}
}
