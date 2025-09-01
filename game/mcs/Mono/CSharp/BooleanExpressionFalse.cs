using System;

namespace Mono.CSharp
{
	// Token: 0x020001E2 RID: 482
	public class BooleanExpressionFalse : Unary
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x0007C50F File Offset: 0x0007A70F
		public BooleanExpressionFalse(Expression expr) : base(Unary.Operator.LogicalNot, expr, expr.Location)
		{
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0007C51F File Offset: 0x0007A71F
		protected override Expression ResolveOperator(ResolveContext ec, Expression expr)
		{
			return Expression.GetOperatorFalse(ec, expr, this.loc) ?? base.ResolveOperator(ec, expr);
		}
	}
}
