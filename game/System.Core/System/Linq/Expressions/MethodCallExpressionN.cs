using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000280 RID: 640
	internal sealed class MethodCallExpressionN : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x0003B98E File Offset: 0x00039B8E
		public MethodCallExpressionN(MethodInfo method, IReadOnlyList<Expression> args) : base(method)
		{
			this._arguments = args;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003B99E File Offset: 0x00039B9E
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0003B9AC File Offset: 0x00039BAC
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003B9B9 File Offset: 0x00039BB9
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003B9C6 File Offset: 0x00039BC6
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003B9D4 File Offset: 0x00039BD4
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method, args ?? this._arguments);
		}

		// Token: 0x04000A2C RID: 2604
		private IReadOnlyList<Expression> _arguments;
	}
}
