using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000284 RID: 644
	internal sealed class MethodCallExpression2 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012BC RID: 4796 RVA: 0x0003BB36 File Offset: 0x00039D36
		public MethodCallExpression2(MethodInfo method, Expression arg0, Expression arg1) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003BB4D File Offset: 0x00039D4D
		public override Expression GetArgument(int index)
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00034E1C File Offset: 0x0003301C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003BB78 File Offset: 0x00039D78
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

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003BBFC File Offset: 0x00039DFC
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003BC0A File Offset: 0x00039E0A
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x04000A2F RID: 2607
		private object _arg0;

		// Token: 0x04000A30 RID: 2608
		private readonly Expression _arg1;
	}
}
