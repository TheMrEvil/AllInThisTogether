using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000236 RID: 566
	internal class ScopeN : ScopeExpression
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0003538D File Offset: 0x0003358D
		internal ScopeN(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> body) : base(variables)
		{
			this._body = body;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003539D File Offset: 0x0003359D
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			return ExpressionUtils.SameElements<Expression>(expressions, this._body);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x000353AB File Offset: 0x000335AB
		protected IReadOnlyList<Expression> Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000353B3 File Offset: 0x000335B3
		internal override Expression GetExpression(int index)
		{
			return this._body[index];
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x000353C1 File Offset: 0x000335C1
		internal override int ExpressionCount
		{
			get
			{
				return this._body.Count;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000353CE File Offset: 0x000335CE
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._body);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000353DB File Offset: 0x000335DB
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeN(variables, this._body);
			}
			return new ScopeN(base.ReuseOrValidateVariables(variables), args);
		}

		// Token: 0x04000952 RID: 2386
		private IReadOnlyList<Expression> _body;
	}
}
