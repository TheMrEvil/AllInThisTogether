using System;

namespace Mono.CSharp
{
	// Token: 0x020001D6 RID: 470
	internal abstract class PatternExpression : Expression
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x000419DF File Offset: 0x0003FBDF
		protected PatternExpression(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotImplementedException();
		}
	}
}
