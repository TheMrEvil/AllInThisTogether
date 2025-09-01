using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection as an <see cref="T:System.Linq.IQueryable`1" /> data source.</summary>
	/// <typeparam name="T">The type of the data in the collection.</typeparam>
	// Token: 0x0200008F RID: 143
	public class EnumerableQuery<T> : EnumerableQuery, IOrderedQueryable<T>, IQueryable<T>, IEnumerable<!0>, IEnumerable, IQueryable, IOrderedQueryable, IQueryProvider
	{
		/// <summary>Gets the query provider that is associated with this instance.</summary>
		/// <returns>The query provider that is associated with this instance.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000022A7 File Offset: 0x000004A7
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery`1" /> class and associates it with an <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</summary>
		/// <param name="enumerable">A collection to associate with the new instance.</param>
		// Token: 0x06000416 RID: 1046 RVA: 0x0000BE70 File Offset: 0x0000A070
		public EnumerableQuery(IEnumerable<T> enumerable)
		{
			this._enumerable = enumerable;
			this._expression = Expression.Constant(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery`1" /> class and associates the instance with an expression tree.</summary>
		/// <param name="expression">An expression tree to associate with the new instance.</param>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000BE8B File Offset: 0x0000A08B
		public EnumerableQuery(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000BE9A File Offset: 0x0000A09A
		internal override Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000BEA2 File Offset: 0x0000A0A2
		internal override IEnumerable Enumerable
		{
			get
			{
				return this._enumerable;
			}
		}

		/// <summary>Gets the expression tree that is associated with or that represents this instance.</summary>
		/// <returns>The expression tree that is associated with or that represents this instance.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000BE9A File Offset: 0x0000A09A
		Expression IQueryable.Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets the type of the data in the collection that this instance represents.</summary>
		/// <returns>The type of the data in the collection that this instance represents.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		/// <summary>Constructs a new <see cref="T:System.Linq.EnumerableQuery`1" /> object and associates it with a specified expression tree that represents an <see cref="T:System.Linq.IQueryable" /> collection of data.</summary>
		/// <param name="expression">An expression tree that represents an <see cref="T:System.Linq.IQueryable" /> collection of data.</param>
		/// <returns>An <see cref="T:System.Linq.EnumerableQuery`1" /> object that is associated with <paramref name="expression" />.</returns>
		// Token: 0x0600041C RID: 1052 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			Type type = TypeHelper.FindGenericType(typeof(IQueryable<>), expression.Type);
			if (type == null)
			{
				throw Error.ArgumentNotValid("expression");
			}
			return EnumerableQuery.Create(type.GetGenericArguments()[0], expression);
		}

		/// <summary>Constructs a new <see cref="T:System.Linq.EnumerableQuery`1" /> object and associates it with a specified expression tree that represents an <see cref="T:System.Linq.IQueryable`1" /> collection of data.</summary>
		/// <param name="expression">An expression tree to execute.</param>
		/// <typeparam name="S">The type of the data in the collection that <paramref name="expression" /> represents.</typeparam>
		/// <returns>An EnumerableQuery object that is associated with <paramref name="expression" />.</returns>
		// Token: 0x0600041D RID: 1053 RVA: 0x0000BF09 File Offset: 0x0000A109
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableQuery<TElement>(expression);
		}

		/// <summary>Executes an expression after rewriting it to call <see cref="T:System.Linq.Enumerable" /> methods instead of <see cref="T:System.Linq.Queryable" /> methods on any enumerable data sources that cannot be queried by <see cref="T:System.Linq.Queryable" /> methods.</summary>
		/// <param name="expression">An expression tree to execute.</param>
		/// <returns>The value that results from executing <paramref name="expression" />.</returns>
		// Token: 0x0600041E RID: 1054 RVA: 0x0000BF41 File Offset: 0x0000A141
		object IQueryProvider.Execute(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			return EnumerableExecutor.Create(expression).ExecuteBoxed();
		}

		/// <summary>Executes an expression after rewriting it to call <see cref="T:System.Linq.Enumerable" /> methods instead of <see cref="T:System.Linq.Queryable" /> methods on any enumerable data sources that cannot be queried by <see cref="T:System.Linq.Queryable" /> methods.</summary>
		/// <param name="expression">An expression tree to execute.</param>
		/// <typeparam name="S">The type of the data in the collection that <paramref name="expression" /> represents.</typeparam>
		/// <returns>The value that results from executing <paramref name="expression" />.</returns>
		// Token: 0x0600041F RID: 1055 RVA: 0x0000BF5C File Offset: 0x0000A15C
		TElement IQueryProvider.Execute<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(TElement).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableExecutor<TElement>(expression).Execute();
		}

		/// <summary>Returns an enumerator that can iterate through the associated <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection, or, if it is null, through the collection that results from rewriting the associated expression tree as a query on an <see cref="T:System.Collections.Generic.IEnumerable`1" /> data source and executing it.</summary>
		/// <returns>An enumerator that can be used to iterate through the associated data source.</returns>
		// Token: 0x06000420 RID: 1056 RVA: 0x0000BF99 File Offset: 0x0000A199
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000BF99 File Offset: 0x0000A199
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		private IEnumerator<T> GetEnumerator()
		{
			if (this._enumerable == null)
			{
				IEnumerable<T> enumerable = Expression.Lambda<Func<IEnumerable<T>>>(new EnumerableRewriter().Visit(this._expression), null).Compile()();
				if (enumerable == this)
				{
					throw Error.EnumeratingNullEnumerableExpression();
				}
				this._enumerable = enumerable;
			}
			return this._enumerable.GetEnumerator();
		}

		/// <summary>Returns a textual representation of the enumerable collection or, if it is null, of the expression tree that is associated with this instance.</summary>
		/// <returns>A textual representation of the enumerable collection or, if it is null, of the expression tree that is associated with this instance.</returns>
		// Token: 0x06000423 RID: 1059 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		public override string ToString()
		{
			ConstantExpression constantExpression = this._expression as ConstantExpression;
			if (constantExpression == null || constantExpression.Value != this)
			{
				return this._expression.ToString();
			}
			if (this._enumerable != null)
			{
				return this._enumerable.ToString();
			}
			return "null";
		}

		// Token: 0x04000448 RID: 1096
		private readonly Expression _expression;

		// Token: 0x04000449 RID: 1097
		private IEnumerable<T> _enumerable;
	}
}
