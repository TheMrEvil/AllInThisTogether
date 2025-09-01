using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000264 RID: 612
	internal sealed class InvocationExpression1 : InvocationExpression
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x0003AAF5 File Offset: 0x00038CF5
		public InvocationExpression1(Expression lambda, Type returnType, Expression arg0) : base(lambda, returnType)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003AB06 File Offset: 0x00038D06
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003AB14 File Offset: 0x00038D14
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00007E1D File Offset: 0x0000601D
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0003AB2F File Offset: 0x00038D2F
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x040009FA RID: 2554
		private object _arg0;
	}
}
