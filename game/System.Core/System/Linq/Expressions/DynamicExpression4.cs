using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000252 RID: 594
	internal class DynamicExpression4 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x000379A9 File Offset: 0x00035BA9
		internal DynamicExpression4(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000379D4 File Offset: 0x00035BD4
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
			case 3:
				return this._arg3;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00035070 File Offset: 0x00033270
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00037A24 File Offset: 0x00035C24
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 4)
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
							if (enumerator.Current == this._arg2)
							{
								enumerator.MoveNext();
								return enumerator.Current == this._arg3;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00037AD8 File Offset: 0x00035CD8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00037AE6 File Offset: 0x00035CE6
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x0400098C RID: 2444
		private object _arg0;

		// Token: 0x0400098D RID: 2445
		private readonly Expression _arg1;

		// Token: 0x0400098E RID: 2446
		private readonly Expression _arg2;

		// Token: 0x0400098F RID: 2447
		private readonly Expression _arg3;
	}
}
