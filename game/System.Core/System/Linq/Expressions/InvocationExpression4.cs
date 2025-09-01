using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000267 RID: 615
	internal sealed class InvocationExpression4 : InvocationExpression
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x0003AC62 File Offset: 0x00038E62
		public InvocationExpression4(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003AC8B File Offset: 0x00038E8B
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003AC9C File Offset: 0x00038E9C
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00035070 File Offset: 0x00033270
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003ACEC File Offset: 0x00038EEC
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x04000A00 RID: 2560
		private object _arg0;

		// Token: 0x04000A01 RID: 2561
		private readonly Expression _arg1;

		// Token: 0x04000A02 RID: 2562
		private readonly Expression _arg2;

		// Token: 0x04000A03 RID: 2563
		private readonly Expression _arg3;
	}
}
