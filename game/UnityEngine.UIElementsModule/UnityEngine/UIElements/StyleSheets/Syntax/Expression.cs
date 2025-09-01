using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x0200037C RID: 892
	internal class Expression
	{
		// Token: 0x06001CAE RID: 7342 RVA: 0x000880B9 File Offset: 0x000862B9
		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}

		// Token: 0x04000E57 RID: 3671
		public ExpressionType type;

		// Token: 0x04000E58 RID: 3672
		public ExpressionMultiplier multiplier;

		// Token: 0x04000E59 RID: 3673
		public DataType dataType;

		// Token: 0x04000E5A RID: 3674
		public ExpressionCombinator combinator;

		// Token: 0x04000E5B RID: 3675
		public Expression[] subExpressions;

		// Token: 0x04000E5C RID: 3676
		public string keyword;
	}
}
