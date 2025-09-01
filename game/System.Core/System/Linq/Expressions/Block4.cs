using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000231 RID: 561
	internal sealed class Block4 : BlockExpression
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x00034F4B File Offset: 0x0003314B
		internal Block4(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00034F70 File Offset: 0x00033170
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 4)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._arg0 as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(expressions, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = expressions.GetEnumerator())
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

		// Token: 0x06000F67 RID: 3943 RVA: 0x00035020 File Offset: 0x00033220
		internal override Expression GetExpression(int index)
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
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00035070 File Offset: 0x00033270
		internal override int ExpressionCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00035073 File Offset: 0x00033273
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00035081 File Offset: 0x00033281
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block4(args[0], args[1], args[2], args[3]);
		}

		// Token: 0x04000946 RID: 2374
		private object _arg0;

		// Token: 0x04000947 RID: 2375
		private readonly Expression _arg1;

		// Token: 0x04000948 RID: 2376
		private readonly Expression _arg2;

		// Token: 0x04000949 RID: 2377
		private readonly Expression _arg3;
	}
}
