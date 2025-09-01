using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000281 RID: 641
	internal sealed class InstanceMethodCallExpressionN : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x0003B9EC File Offset: 0x00039BEC
		public InstanceMethodCallExpressionN(MethodInfo method, Expression instance, IReadOnlyList<Expression> args) : base(method, instance)
		{
			this._arguments = args;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003B9FD File Offset: 0x00039BFD
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0003BA0B File Offset: 0x00039C0B
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003BA18 File Offset: 0x00039C18
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003BA26 File Offset: 0x00039C26
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003BA33 File Offset: 0x00039C33
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method, args ?? this._arguments);
		}

		// Token: 0x04000A2D RID: 2605
		private IReadOnlyList<Expression> _arguments;
	}
}
