using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F4 RID: 756
	public class Where : AQueryClause
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x000AD8F8 File Offset: 0x000ABAF8
		public Where(QueryBlock block, Expression expr, Location loc) : base(block, expr, loc)
		{
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x000ADA51 File Offset: 0x000ABC51
		protected override string MethodName
		{
			get
			{
				return "Where";
			}
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000ADA58 File Offset: 0x000ABC58
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
