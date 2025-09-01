using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000C RID: 12
	public class ModulusOperatorGrammar : BinaryOperatorGrammar
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000262E File Offset: 0x0000082E
		public override int Precedence
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002631 File Offset: 0x00000831
		protected override char OperatorToken
		{
			get
			{
				return '%';
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002635 File Offset: 0x00000835
		protected override string OperatorMethodName
		{
			get
			{
				return "op_Modulus";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000263C File Offset: 0x0000083C
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Modulo);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000264A File Offset: 0x0000084A
		public ModulusOperatorGrammar()
		{
		}
	}
}
