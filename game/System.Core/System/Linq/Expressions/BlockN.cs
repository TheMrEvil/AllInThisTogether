using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000233 RID: 563
	internal class BlockN : BlockExpression
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003520B File Offset: 0x0003340B
		internal BlockN(IReadOnlyList<Expression> expressions)
		{
			this._expressions = expressions;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003521A File Offset: 0x0003341A
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			return ExpressionUtils.SameElements<Expression>(expressions, this._expressions);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00035228 File Offset: 0x00033428
		internal override Expression GetExpression(int index)
		{
			return this._expressions[index];
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00035236 File Offset: 0x00033436
		internal override int ExpressionCount
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00035243 File Offset: 0x00033443
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._expressions);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00035250 File Offset: 0x00033450
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new BlockN(args);
		}

		// Token: 0x0400094F RID: 2383
		private IReadOnlyList<Expression> _expressions;
	}
}
