using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000006 RID: 6
	public class BitwiseAndOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002546 File Offset: 0x00000746
		public override int Precedence
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002549 File Offset: 0x00000749
		protected override char OperatorToken
		{
			get
			{
				return '&';
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000254D File Offset: 0x0000074D
		protected override string OperatorMethodName
		{
			get
			{
				return "op_bitwiseAnd";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002554 File Offset: 0x00000754
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.And);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002562 File Offset: 0x00000762
		public BitwiseAndOperatorGrammar()
		{
		}
	}
}
