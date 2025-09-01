using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024A RID: 586
	internal class DynamicExpressionN : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001050 RID: 4176 RVA: 0x00037623 File Offset: 0x00035823
		internal DynamicExpressionN(Type delegateType, CallSiteBinder binder, IReadOnlyList<Expression> arguments) : base(delegateType, binder)
		{
			this._arguments = arguments;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00037634 File Offset: 0x00035834
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00037642 File Offset: 0x00035842
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00037650 File Offset: 0x00035850
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003765D File Offset: 0x0003585D
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003766A File Offset: 0x0003586A
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args);
		}

		// Token: 0x04000981 RID: 2433
		private IReadOnlyList<Expression> _arguments;
	}
}
