using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000289 RID: 649
	internal sealed class InstanceMethodCallExpression1 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012DA RID: 4826 RVA: 0x0003C121 File Offset: 0x0003A321
		public InstanceMethodCallExpression1(MethodInfo method, Expression instance, Expression arg0) : base(method, instance)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0003C132 File Offset: 0x0003A332
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00007E1D File Offset: 0x0000601D
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003C150 File Offset: 0x0003A350
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

		// Token: 0x060012DE RID: 4830 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003C1BA File Offset: 0x0003A3BA
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x04000A3D RID: 2621
		private object _arg0;
	}
}
