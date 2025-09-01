using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000235 RID: 565
	internal sealed class Scope1 : ScopeExpression
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x000352AB File Offset: 0x000334AB
		internal Scope1(IReadOnlyList<ParameterExpression> variables, Expression body) : this(variables, body)
		{
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000352B5 File Offset: 0x000334B5
		private Scope1(IReadOnlyList<ParameterExpression> variables, object body) : base(variables)
		{
			this._body = body;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000352C8 File Offset: 0x000334C8
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 1)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._body as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(expressions, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = expressions.GetEnumerator())
				{
					enumerator.MoveNext();
					return ExpressionUtils.ReturnObject<Expression>(this._body) == enumerator.Current;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00035338 File Offset: 0x00033538
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._body);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00007E1D File Offset: 0x0000601D
		internal override int ExpressionCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00035353 File Offset: 0x00033553
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._body);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00035361 File Offset: 0x00033561
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new Scope1(variables, this._body);
			}
			return new Scope1(base.ReuseOrValidateVariables(variables), args[0]);
		}

		// Token: 0x04000951 RID: 2385
		private object _body;
	}
}
