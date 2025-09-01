using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200023F RID: 575
	internal class FullConditionalExpression : ConditionalExpression
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003585F File Offset: 0x00033A5F
		internal FullConditionalExpression(Expression test, Expression ifTrue, Expression ifFalse) : base(test, ifTrue)
		{
			this._false = ifFalse;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00035870 File Offset: 0x00033A70
		internal override Expression GetFalse()
		{
			return this._false;
		}

		// Token: 0x04000965 RID: 2405
		private readonly Expression _false;
	}
}
