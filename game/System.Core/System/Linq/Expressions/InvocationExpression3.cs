using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000266 RID: 614
	internal sealed class InvocationExpression3 : InvocationExpression
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x0003ABC7 File Offset: 0x00038DC7
		public InvocationExpression3(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2) : base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003ABF6 File Offset: 0x00038DF6
		public override Expression GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00034F2A File Offset: 0x0003312A
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003AC30 File Offset: 0x00038E30
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x040009FD RID: 2557
		private object _arg0;

		// Token: 0x040009FE RID: 2558
		private readonly Expression _arg1;

		// Token: 0x040009FF RID: 2559
		private readonly Expression _arg2;
	}
}
