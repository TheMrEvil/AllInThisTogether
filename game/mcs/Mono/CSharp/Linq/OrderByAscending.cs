using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F5 RID: 757
	public class OrderByAscending : AQueryClause
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x000ADA61 File Offset: 0x000ABC61
		public OrderByAscending(QueryBlock block, Expression expr) : base(block, expr, expr.Location)
		{
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000ADA71 File Offset: 0x000ABC71
		protected override string MethodName
		{
			get
			{
				return "OrderBy";
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000ADA78 File Offset: 0x000ABC78
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
