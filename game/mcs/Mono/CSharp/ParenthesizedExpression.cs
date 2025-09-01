using System;

namespace Mono.CSharp
{
	// Token: 0x020001CC RID: 460
	public class ParenthesizedExpression : ShimExpression
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x000742B9 File Offset: 0x000724B9
		public ParenthesizedExpression(Expression expr, Location loc) : base(expr)
		{
			this.loc = loc;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000742CC File Offset: 0x000724CC
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = this.expr.Resolve(ec);
			Constant constant = expression as Constant;
			if (constant != null && constant.IsLiteral)
			{
				return Constant.CreateConstantFromValue(expression.Type, constant.GetValue(), this.expr.Location);
			}
			return expression;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00074316 File Offset: 0x00072516
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return this.expr.DoResolveLValue(ec, right_side);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00074325 File Offset: 0x00072525
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0007432E File Offset: 0x0007252E
		public override bool HasConditionalAccess()
		{
			return this.expr.HasConditionalAccess();
		}
	}
}
