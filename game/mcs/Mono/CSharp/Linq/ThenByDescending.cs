using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F8 RID: 760
	public class ThenByDescending : OrderByDescending
	{
		// Token: 0x06002438 RID: 9272 RVA: 0x000ADAAB File Offset: 0x000ABCAB
		public ThenByDescending(QueryBlock block, Expression expr) : base(block, expr)
		{
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000ADAB5 File Offset: 0x000ABCB5
		protected override string MethodName
		{
			get
			{
				return "ThenByDescending";
			}
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000ADABC File Offset: 0x000ABCBC
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
