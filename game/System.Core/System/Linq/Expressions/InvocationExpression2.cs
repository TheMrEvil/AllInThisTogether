using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000265 RID: 613
	internal sealed class InvocationExpression2 : InvocationExpression
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x0003AB4F File Offset: 0x00038D4F
		public InvocationExpression2(Expression lambda, Type returnType, Expression arg0, Expression arg1) : base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003AB68 File Offset: 0x00038D68
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0003AB76 File Offset: 0x00038D76
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this._arg1;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00034E1C File Offset: 0x0003301C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003AB9E File Offset: 0x00038D9E
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x040009FB RID: 2555
		private object _arg0;

		// Token: 0x040009FC RID: 2556
		private readonly Expression _arg1;
	}
}
