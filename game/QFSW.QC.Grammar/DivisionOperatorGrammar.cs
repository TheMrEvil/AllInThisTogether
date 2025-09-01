using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000008 RID: 8
	public class DivisionOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000258E File Offset: 0x0000078E
		public override int Precedence
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002591 File Offset: 0x00000791
		protected override char OperatorToken
		{
			get
			{
				return '/';
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002595 File Offset: 0x00000795
		protected override string OperatorMethodName
		{
			get
			{
				return "op_Division";
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000259C File Offset: 0x0000079C
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Divide);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025AA File Offset: 0x000007AA
		public DivisionOperatorGrammar()
		{
		}
	}
}
