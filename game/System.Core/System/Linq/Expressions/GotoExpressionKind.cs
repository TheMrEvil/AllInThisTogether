using System;

namespace System.Linq.Expressions
{
	/// <summary>Specifies what kind of jump this <see cref="T:System.Linq.Expressions.GotoExpression" /> represents.</summary>
	// Token: 0x0200025B RID: 603
	public enum GotoExpressionKind
	{
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a jump to some location.</summary>
		// Token: 0x040009EC RID: 2540
		Goto,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a return statement.</summary>
		// Token: 0x040009ED RID: 2541
		Return,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a break statement.</summary>
		// Token: 0x040009EE RID: 2542
		Break,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a continue statement.</summary>
		// Token: 0x040009EF RID: 2543
		Continue
	}
}
