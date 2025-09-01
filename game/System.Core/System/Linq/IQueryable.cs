using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.</summary>
	// Token: 0x02000074 RID: 116
	public interface IQueryable : IEnumerable
	{
		/// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000281 RID: 641
		Expression Expression { get; }

		/// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000282 RID: 642
		Type ElementType { get; }

		/// <summary>Gets the query provider that is associated with this data source.</summary>
		/// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000283 RID: 643
		IQueryProvider Provider { get; }
	}
}
