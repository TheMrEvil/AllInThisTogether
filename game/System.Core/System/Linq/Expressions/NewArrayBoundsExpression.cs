using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200028E RID: 654
	internal sealed class NewArrayBoundsExpression : NewArrayExpression
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x0003C4F2 File Offset: 0x0003A6F2
		internal NewArrayBoundsExpression(Type type, ReadOnlyCollection<Expression> expressions) : base(type, expressions)
		{
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0003C500 File Offset: 0x0003A700
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayBounds;
			}
		}
	}
}
