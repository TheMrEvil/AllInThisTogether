using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000283 RID: 643
	internal sealed class MethodCallExpression1 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x0003BA72 File Offset: 0x00039C72
		public MethodCallExpression1(MethodInfo method, Expression arg0) : base(method)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003BA82 File Offset: 0x00039C82
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00007E1D File Offset: 0x0000601D
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003BA9D File Offset: 0x00039C9D
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003BAAC File Offset: 0x00039CAC
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

		// Token: 0x060012BB RID: 4795 RVA: 0x0003BB08 File Offset: 0x00039D08
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x04000A2E RID: 2606
		private object _arg0;
	}
}
