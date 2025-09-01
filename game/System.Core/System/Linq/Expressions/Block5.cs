using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000232 RID: 562
	internal sealed class Block5 : BlockExpression
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x00035094 File Offset: 0x00033294
		internal Block5(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000350C4 File Offset: 0x000332C4
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
			case 4:
				return this._arg4;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00035120 File Offset: 0x00033320
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 5)
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000351E4 File Offset: 0x000333E4
		internal override int ExpressionCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000351E7 File Offset: 0x000333E7
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000351F5 File Offset: 0x000333F5
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block5(args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x0400094A RID: 2378
		private object _arg0;

		// Token: 0x0400094B RID: 2379
		private readonly Expression _arg1;

		// Token: 0x0400094C RID: 2380
		private readonly Expression _arg2;

		// Token: 0x0400094D RID: 2381
		private readonly Expression _arg3;

		// Token: 0x0400094E RID: 2382
		private readonly Expression _arg4;
	}
}
