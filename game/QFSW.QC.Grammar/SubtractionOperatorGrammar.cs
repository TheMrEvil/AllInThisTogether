using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000E RID: 14
	public class SubtractionOperatorGrammar : BinaryAndUnaryOperatorGrammar
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002676 File Offset: 0x00000876
		public override int Precedence
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002679 File Offset: 0x00000879
		protected override char OperatorToken
		{
			get
			{
				return '-';
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000267D File Offset: 0x0000087D
		protected override string OperatorMethodName
		{
			get
			{
				return "op_Subtraction";
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002684 File Offset: 0x00000884
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Subtract);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002692 File Offset: 0x00000892
		public SubtractionOperatorGrammar()
		{
		}
	}
}
