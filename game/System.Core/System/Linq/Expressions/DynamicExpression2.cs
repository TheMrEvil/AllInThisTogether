using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024E RID: 590
	internal class DynamicExpression2 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x00037763 File Offset: 0x00035963
		internal DynamicExpression2(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003777C File Offset: 0x0003597C
		Expression IArgumentProvider.GetArgument(int index)
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00034E1C File Offset: 0x0003301C
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000377A4 File Offset: 0x000359A4
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 2)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._arg0 as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(arguments, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._arg0)
					{
						enumerator.MoveNext();
						return enumerator.Current == this._arg1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00037828 File Offset: 0x00035A28
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00037836 File Offset: 0x00035A36
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1]);
		}

		// Token: 0x04000985 RID: 2437
		private object _arg0;

		// Token: 0x04000986 RID: 2438
		private readonly Expression _arg1;
	}
}
