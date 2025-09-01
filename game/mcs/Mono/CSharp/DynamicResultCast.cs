using System;

namespace Mono.CSharp
{
	// Token: 0x02000162 RID: 354
	public class DynamicResultCast : ShimExpression
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x00047DEC File Offset: 0x00045FEC
		public DynamicResultCast(TypeSpec type, Expression expr) : base(expr)
		{
			this.type = type;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00047DFC File Offset: 0x00045FFC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			this.eclass = ExprClass.Value;
			return this;
		}
	}
}
