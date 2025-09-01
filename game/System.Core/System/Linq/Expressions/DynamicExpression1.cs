using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024C RID: 588
	internal class DynamicExpression1 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x00037699 File Offset: 0x00035899
		internal DynamicExpression1(Type delegateType, CallSiteBinder binder, Expression arg0) : base(delegateType, binder)
		{
			this._arg0 = arg0;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000376AA File Offset: 0x000358AA
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x00007E1D File Offset: 0x0000601D
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000376C8 File Offset: 0x000358C8
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 1)
			{
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					return enumerator.Current == ExpressionUtils.ReturnObject<Expression>(this._arg0);
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00037724 File Offset: 0x00035924
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00037732 File Offset: 0x00035932
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0]);
		}

		// Token: 0x04000983 RID: 2435
		private object _arg0;
	}
}
