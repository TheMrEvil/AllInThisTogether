using System;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an expression tree and provides functionality to execute the expression tree after rewriting it.</summary>
	/// <typeparam name="T">The data type of the value that results from executing the expression tree.</typeparam>
	// Token: 0x0200008D RID: 141
	public class EnumerableExecutor<T> : EnumerableExecutor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableExecutor`1" /> class.</summary>
		/// <param name="expression">An expression tree to associate with the new instance.</param>
		// Token: 0x0600040D RID: 1037 RVA: 0x0000BE03 File Offset: 0x0000A003
		public EnumerableExecutor(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000BE12 File Offset: 0x0000A012
		internal override object ExecuteBoxed()
		{
			return this.Execute();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BE1F File Offset: 0x0000A01F
		internal T Execute()
		{
			return Expression.Lambda<Func<T>>(new EnumerableRewriter().Visit(this._expression), null).Compile()();
		}

		// Token: 0x04000447 RID: 1095
		private readonly Expression _expression;
	}
}
