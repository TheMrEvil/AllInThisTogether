using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200022F RID: 559
	internal sealed class Block2 : BlockExpression
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x00034D5B File Offset: 0x00032F5B
		internal Block2(Expression arg0, Expression arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00034D71 File Offset: 0x00032F71
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._arg1;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00034D9C File Offset: 0x00032F9C
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 2)
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
						return enumerator.Current == this._arg1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00034E1C File Offset: 0x0003301C
		internal override int ExpressionCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00034E1F File Offset: 0x0003301F
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00034E2D File Offset: 0x0003302D
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block2(args[0], args[1]);
		}

		// Token: 0x04000941 RID: 2369
		private object _arg0;

		// Token: 0x04000942 RID: 2370
		private readonly Expression _arg1;
	}
}
