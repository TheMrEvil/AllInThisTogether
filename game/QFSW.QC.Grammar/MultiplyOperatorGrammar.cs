using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000D RID: 13
	public class MultiplyOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002652 File Offset: 0x00000852
		public override int Precedence
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002655 File Offset: 0x00000855
		protected override char OperatorToken
		{
			get
			{
				return '*';
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002659 File Offset: 0x00000859
		protected override string OperatorMethodName
		{
			get
			{
				return "op_Multiply";
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002660 File Offset: 0x00000860
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Multiply);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000266E File Offset: 0x0000086E
		public MultiplyOperatorGrammar()
		{
		}
	}
}
