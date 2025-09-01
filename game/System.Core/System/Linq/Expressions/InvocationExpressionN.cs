using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000262 RID: 610
	internal sealed class InvocationExpressionN : InvocationExpression
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x0003AA75 File Offset: 0x00038C75
		public InvocationExpressionN(Expression lambda, IReadOnlyList<Expression> arguments, Type returnType) : base(lambda, returnType)
		{
			this._arguments = arguments;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003AA86 File Offset: 0x00038C86
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003AA93 File Offset: 0x00038C93
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0003AAA1 File Offset: 0x00038CA1
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda, arguments ?? this._arguments);
		}

		// Token: 0x040009F9 RID: 2553
		private IReadOnlyList<Expression> _arguments;
	}
}
