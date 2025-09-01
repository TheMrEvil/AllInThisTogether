using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000234 RID: 564
	internal class ScopeExpression : BlockExpression
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x00035258 File Offset: 0x00033458
		internal ScopeExpression(IReadOnlyList<ParameterExpression> variables)
		{
			this._variables = variables;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00035267 File Offset: 0x00033467
		internal override bool SameVariables(ICollection<ParameterExpression> variables)
		{
			return ExpressionUtils.SameElements<ParameterExpression>(variables, this._variables);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00035275 File Offset: 0x00033475
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._variables);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00035282 File Offset: 0x00033482
		protected IReadOnlyList<ParameterExpression> VariablesList
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003528A File Offset: 0x0003348A
		internal IReadOnlyList<ParameterExpression> ReuseOrValidateVariables(ReadOnlyCollection<ParameterExpression> variables)
		{
			if (variables != null && variables != this.VariablesList)
			{
				Expression.ValidateVariables(variables, "variables");
				return variables;
			}
			return this.VariablesList;
		}

		// Token: 0x04000950 RID: 2384
		private IReadOnlyList<ParameterExpression> _variables;
	}
}
