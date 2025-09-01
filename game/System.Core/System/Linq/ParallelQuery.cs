using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Parallel;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a parallel sequence.</summary>
	// Token: 0x0200007E RID: 126
	public class ParallelQuery : IEnumerable
	{
		// Token: 0x0600029A RID: 666 RVA: 0x000080CC File Offset: 0x000062CC
		internal ParallelQuery(QuerySettings specifiedSettings)
		{
			this._specifiedSettings = specifiedSettings;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000080DB File Offset: 0x000062DB
		internal QuerySettings SpecifiedQuerySettings
		{
			get
			{
				return this._specifiedSettings;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal virtual ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal virtual ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000080E3 File Offset: 0x000062E3
		[ExcludeFromCodeCoverage]
		internal virtual IEnumerator GetEnumeratorUntyped()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x0600029F RID: 671 RVA: 0x000080EA File Offset: 0x000062EA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorUntyped();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000235B File Offset: 0x0000055B
		internal ParallelQuery()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040003B3 RID: 947
		private QuerySettings _specifiedSettings;
	}
}
