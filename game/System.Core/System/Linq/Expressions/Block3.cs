using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000230 RID: 560
	internal sealed class Block3 : BlockExpression
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x00034E3A File Offset: 0x0003303A
		internal Block3(Expression arg0, Expression arg1, Expression arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00034E58 File Offset: 0x00033058
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 3)
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
							return enumerator.Current == this._arg2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00034EF0 File Offset: 0x000330F0
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
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00034F2A File Offset: 0x0003312A
		internal override int ExpressionCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00034F2D File Offset: 0x0003312D
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00034F3B File Offset: 0x0003313B
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block3(args[0], args[1], args[2]);
		}

		// Token: 0x04000943 RID: 2371
		private object _arg0;

		// Token: 0x04000944 RID: 2372
		private readonly Expression _arg1;

		// Token: 0x04000945 RID: 2373
		private readonly Expression _arg2;
	}
}
