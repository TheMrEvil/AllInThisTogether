using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F6 RID: 758
	public class OrderByDescending : AQueryClause
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x000ADA61 File Offset: 0x000ABC61
		public OrderByDescending(QueryBlock block, Expression expr) : base(block, expr, expr.Location)
		{
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x000ADA81 File Offset: 0x000ABC81
		protected override string MethodName
		{
			get
			{
				return "OrderByDescending";
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000ADA88 File Offset: 0x000ABC88
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
