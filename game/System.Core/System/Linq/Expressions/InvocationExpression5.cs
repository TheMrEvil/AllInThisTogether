using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000268 RID: 616
	internal sealed class InvocationExpression5 : InvocationExpression
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x0003AD27 File Offset: 0x00038F27
		public InvocationExpression5(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4) : base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003AD58 File Offset: 0x00038F58
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003AD68 File Offset: 0x00038F68
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
			case 3:
				return this._arg3;
			case 4:
				return this._arg4;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000351E4 File Offset: 0x000333E4
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003ADC4 File Offset: 0x00038FC4
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x04000A04 RID: 2564
		private object _arg0;

		// Token: 0x04000A05 RID: 2565
		private readonly Expression _arg1;

		// Token: 0x04000A06 RID: 2566
		private readonly Expression _arg2;

		// Token: 0x04000A07 RID: 2567
		private readonly Expression _arg3;

		// Token: 0x04000A08 RID: 2568
		private readonly Expression _arg4;
	}
}
