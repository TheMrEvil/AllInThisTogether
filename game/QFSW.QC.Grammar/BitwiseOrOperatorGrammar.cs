using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000007 RID: 7
	public class BitwiseOrOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000256A File Offset: 0x0000076A
		public override int Precedence
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000256D File Offset: 0x0000076D
		protected override char OperatorToken
		{
			get
			{
				return '|';
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002571 File Offset: 0x00000771
		protected override string OperatorMethodName
		{
			get
			{
				return "op_bitwiseOr";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002578 File Offset: 0x00000778
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Or);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002586 File Offset: 0x00000786
		public BitwiseOrOperatorGrammar()
		{
		}
	}
}
