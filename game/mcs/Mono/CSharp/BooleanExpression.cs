using System;

namespace Mono.CSharp
{
	// Token: 0x020001E1 RID: 481
	public class BooleanExpression : ShimExpression
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0007C3D0 File Offset: 0x0007A5D0
		public BooleanExpression(Expression expr) : base(expr)
		{
			this.loc = expr.Location;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0007C3E5 File Offset: 0x0007A5E5
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0007C3F0 File Offset: 0x0007A5F0
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return null;
			}
			Assign assign = this.expr as Assign;
			if (assign != null && assign.Source is Constant)
			{
				ec.Report.Warning(665, 3, this.loc, "Assignment in conditional expression is always constant. Did you mean to use `==' instead ?");
			}
			if (this.expr.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive)
			{
				return this.expr;
			}
			if (this.expr.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.expr));
				return DynamicUnaryConversion.CreateIsTrue(ec, arguments, this.loc).Resolve(ec);
			}
			this.type = ec.BuiltinTypes.Bool;
			Expression expression = Convert.ImplicitConversion(ec, this.expr, this.type, this.loc);
			if (expression != null)
			{
				return expression;
			}
			expression = Expression.GetOperatorTrue(ec, this.expr, this.loc);
			if (expression == null)
			{
				this.expr.Error_ValueCannotBeConverted(ec, this.type, false);
				return null;
			}
			return expression;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0007C506 File Offset: 0x0007A706
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
