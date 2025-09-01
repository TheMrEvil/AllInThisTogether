using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000285 RID: 645
	internal sealed class MethodCallExpression3 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x0003BC45 File Offset: 0x00039E45
		public MethodCallExpression3(MethodInfo method, Expression arg0, Expression arg1, Expression arg2) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003BC64 File Offset: 0x00039E64
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00034F2A File Offset: 0x0003312A
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003BCA0 File Offset: 0x00039EA0
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

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003BD38 File Offset: 0x00039F38
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003BD48 File Offset: 0x00039F48
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x04000A31 RID: 2609
		private object _arg0;

		// Token: 0x04000A32 RID: 2610
		private readonly Expression _arg1;

		// Token: 0x04000A33 RID: 2611
		private readonly Expression _arg2;
	}
}
