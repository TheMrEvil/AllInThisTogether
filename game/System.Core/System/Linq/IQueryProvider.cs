using System;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Defines methods to create and execute queries that are described by an <see cref="T:System.Linq.IQueryable" /> object.</summary>
	// Token: 0x02000076 RID: 118
	public interface IQueryProvider
	{
		/// <summary>Constructs an <see cref="T:System.Linq.IQueryable" /> object that can evaluate the query represented by a specified expression tree.</summary>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		/// <returns>An <see cref="T:System.Linq.IQueryable" /> that can evaluate the query represented by the specified expression tree.</returns>
		// Token: 0x06000284 RID: 644
		IQueryable CreateQuery(Expression expression);

		/// <summary>Constructs an <see cref="T:System.Linq.IQueryable`1" /> object that can evaluate the query represented by a specified expression tree.</summary>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		/// <typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1" /> that is returned.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that can evaluate the query represented by the specified expression tree.</returns>
		// Token: 0x06000285 RID: 645
		IQueryable<TElement> CreateQuery<TElement>(Expression expression);

		/// <summary>Executes the query represented by a specified expression tree.</summary>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		/// <returns>The value that results from executing the specified query.</returns>
		// Token: 0x06000286 RID: 646
		object Execute(Expression expression);

		/// <summary>Executes the strongly-typed query represented by a specified expression tree.</summary>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		/// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
		/// <returns>The value that results from executing the specified query.</returns>
		// Token: 0x06000287 RID: 647
		TResult Execute<TResult>(Expression expression);
	}
}
