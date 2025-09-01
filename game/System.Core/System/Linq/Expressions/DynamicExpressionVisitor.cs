using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents a visitor or rewriter for dynamic expression trees.</summary>
	// Token: 0x02000255 RID: 597
	public class DynamicExpressionVisitor : ExpressionVisitor
	{
		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.DynamicExpression" />.</summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>Returns <see cref="T:System.Linq.Expressions.Expression" />, the modified expression, if it or any subexpression is modified; otherwise, returns the original expression.</returns>
		// Token: 0x06001088 RID: 4232 RVA: 0x00038308 File Offset: 0x00036508
		protected internal override Expression VisitDynamic(DynamicExpression node)
		{
			Expression[] array = ExpressionVisitorUtils.VisitArguments(this, node);
			if (array == null)
			{
				return node;
			}
			return node.Rewrite(array);
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Linq.Expressions.DynamicExpressionVisitor" />.</summary>
		// Token: 0x06001089 RID: 4233 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		public DynamicExpressionVisitor()
		{
		}
	}
}
