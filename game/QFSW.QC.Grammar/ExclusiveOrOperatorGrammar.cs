using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000A RID: 10
	public class ExclusiveOrOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000260A File Offset: 0x0000080A
		public override int Precedence
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000260D File Offset: 0x0000080D
		protected override char OperatorToken
		{
			get
			{
				return '^';
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002611 File Offset: 0x00000811
		protected override string OperatorMethodName
		{
			get
			{
				return "op_ExclusiveOr";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002618 File Offset: 0x00000818
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.ExclusiveOr);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002626 File Offset: 0x00000826
		public ExclusiveOrOperatorGrammar()
		{
		}
	}
}
