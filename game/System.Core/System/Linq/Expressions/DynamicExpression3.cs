using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000250 RID: 592
	internal class DynamicExpression3 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0003786C File Offset: 0x00035A6C
		internal DynamicExpression3(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003788D File Offset: 0x00035A8D
		Expression IArgumentProvider.GetArgument(int index)
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00034F2A File Offset: 0x0003312A
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000378C8 File Offset: 0x00035AC8
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 3)
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
						if (enumerator.Current == this._arg1)
						{
							enumerator.MoveNext();
							return enumerator.Current == this._arg2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00037960 File Offset: 0x00035B60
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003796E File Offset: 0x00035B6E
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2]);
		}

		// Token: 0x04000988 RID: 2440
		private object _arg0;

		// Token: 0x04000989 RID: 2441
		private readonly Expression _arg1;

		// Token: 0x0400098A RID: 2442
		private readonly Expression _arg2;
	}
}
