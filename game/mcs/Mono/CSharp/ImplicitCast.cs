using System;

namespace Mono.CSharp
{
	// Token: 0x020001DA RID: 474
	public class ImplicitCast : ShimExpression
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x000775D1 File Offset: 0x000757D1
		public ImplicitCast(Expression expr, TypeSpec target, bool arrayAccess) : base(expr)
		{
			this.loc = expr.Location;
			this.type = target;
			this.arrayAccess = arrayAccess;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000775F4 File Offset: 0x000757F4
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return null;
			}
			if (this.arrayAccess)
			{
				this.expr = base.ConvertExpressionToArrayIndex(ec, this.expr, false);
			}
			else
			{
				this.expr = Convert.ImplicitConversionRequired(ec, this.expr, this.type, this.loc);
			}
			return this.expr;
		}

		// Token: 0x040009B2 RID: 2482
		private bool arrayAccess;
	}
}
