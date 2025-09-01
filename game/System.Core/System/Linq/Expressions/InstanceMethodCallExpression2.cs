using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200028A RID: 650
	internal sealed class InstanceMethodCallExpression2 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x0003C1EA File Offset: 0x0003A3EA
		public InstanceMethodCallExpression2(MethodInfo method, Expression instance, Expression arg0, Expression arg1) : base(method, instance)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0003C203 File Offset: 0x0003A403
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

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00034E1C File Offset: 0x0003301C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003C22C File Offset: 0x0003A42C
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

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0003C2BE File Offset: 0x0003A4BE
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x04000A3E RID: 2622
		private object _arg0;

		// Token: 0x04000A3F RID: 2623
		private readonly Expression _arg1;
	}
}
