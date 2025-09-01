using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200028D RID: 653
	internal sealed class NewArrayInitExpression : NewArrayExpression
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x0003C4F2 File Offset: 0x0003A6F2
		internal NewArrayInitExpression(Type type, ReadOnlyCollection<Expression> expressions) : base(type, expressions)
		{
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0003C4FC File Offset: 0x0003A6FC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayInit;
			}
		}
	}
}
