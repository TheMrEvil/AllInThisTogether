using System;

namespace Mono.CSharp
{
	// Token: 0x02000213 RID: 531
	public class CatchFilterExpression : BooleanExpression
	{
		// Token: 0x06001B25 RID: 6949 RVA: 0x00083ED5 File Offset: 0x000820D5
		public CatchFilterExpression(Expression expr, Location loc) : base(expr)
		{
			this.loc = loc;
		}
	}
}
