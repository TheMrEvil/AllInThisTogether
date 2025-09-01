using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F7 RID: 759
	public class ThenByAscending : OrderByAscending
	{
		// Token: 0x06002435 RID: 9269 RVA: 0x000ADA91 File Offset: 0x000ABC91
		public ThenByAscending(QueryBlock block, Expression expr) : base(block, expr)
		{
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x000ADA9B File Offset: 0x000ABC9B
		protected override string MethodName
		{
			get
			{
				return "ThenBy";
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000ADAA2 File Offset: 0x000ABCA2
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
