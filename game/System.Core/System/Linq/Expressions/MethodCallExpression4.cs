using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000286 RID: 646
	internal sealed class MethodCallExpression4 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x0003BD9B File Offset: 0x00039F9B
		public MethodCallExpression4(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003BDC4 File Offset: 0x00039FC4
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00035070 File Offset: 0x00033270
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003BE14 File Offset: 0x0003A014
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

		// Token: 0x060012CC RID: 4812 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003BED8 File Offset: 0x0003A0D8
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x04000A34 RID: 2612
		private object _arg0;

		// Token: 0x04000A35 RID: 2613
		private readonly Expression _arg1;

		// Token: 0x04000A36 RID: 2614
		private readonly Expression _arg2;

		// Token: 0x04000A37 RID: 2615
		private readonly Expression _arg3;
	}
}
