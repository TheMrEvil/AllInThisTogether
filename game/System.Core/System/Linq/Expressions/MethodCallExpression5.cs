using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000287 RID: 647
	internal sealed class MethodCallExpression5 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x0003BF38 File Offset: 0x0003A138
		public MethodCallExpression5(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003BF68 File Offset: 0x0003A168
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
			case 3:
				return this._arg3;
			case 4:
				return this._arg4;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000351E4 File Offset: 0x000333E4
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 5)
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
								if (enumerator.Current == this._arg3)
								{
									enumerator.MoveNext();
									return enumerator.Current == this._arg4;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003C08C File Offset: 0x0003A28C
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0003C09C File Offset: 0x0003A29C
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3], args[4]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x04000A38 RID: 2616
		private object _arg0;

		// Token: 0x04000A39 RID: 2617
		private readonly Expression _arg1;

		// Token: 0x04000A3A RID: 2618
		private readonly Expression _arg2;

		// Token: 0x04000A3B RID: 2619
		private readonly Expression _arg3;

		// Token: 0x04000A3C RID: 2620
		private readonly Expression _arg4;
	}
}
