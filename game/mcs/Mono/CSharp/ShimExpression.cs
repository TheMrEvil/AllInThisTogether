using System;

namespace Mono.CSharp
{
	// Token: 0x020001B3 RID: 435
	public abstract class ShimExpression : Expression
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x0006D467 File Offset: 0x0006B667
		protected ShimExpression(Expression expr)
		{
			this.expr = expr;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0006D476 File Offset: 0x0006B676
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0006D47E File Offset: 0x0006B67E
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			if (this.expr == null)
			{
				return;
			}
			((ShimExpression)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0006D4A0 File Offset: 0x0006B6A0
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0006D4B9 File Offset: 0x0006B6B9
		public override void Emit(EmitContext ec)
		{
			throw new InternalErrorException("Missing Resolve call");
		}

		// Token: 0x0400095E RID: 2398
		protected Expression expr;
	}
}
