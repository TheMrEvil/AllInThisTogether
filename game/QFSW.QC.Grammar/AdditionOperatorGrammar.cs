using System;
using System.Linq.Expressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000002 RID: 2
	public class AdditionOperatorGrammar : BinaryAndUnaryOperatorGrammar
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		protected override char OperatorToken
		{
			get
			{
				return '+';
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002057 File Offset: 0x00000257
		protected override string OperatorMethodName
		{
			get
			{
				return "op_Addition";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000205E File Offset: 0x0000025E
		protected override Func<Expression, Expression, BinaryExpression> PrimitiveExpressionGenerator
		{
			get
			{
				return new Func<Expression, Expression, BinaryExpression>(Expression.Add);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000206C File Offset: 0x0000026C
		public AdditionOperatorGrammar()
		{
		}
	}
}
