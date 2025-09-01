using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000271 RID: 625
	internal class ExpressionN<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x0003B3A7 File Offset: 0x000395A7
		public ExpressionN(Expression body, IReadOnlyList<ParameterExpression> parameters) : base(body)
		{
			this._parameters = parameters;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0003B3B7 File Offset: 0x000395B7
		internal override int ParameterCount
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003B3C4 File Offset: 0x000395C4
		internal override ParameterExpression GetParameter(int index)
		{
			return this._parameters[index];
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003B3D2 File Offset: 0x000395D2
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			return ExpressionUtils.SameElements<ParameterExpression>(parameters, this._parameters);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003B3E0 File Offset: 0x000395E0
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._parameters);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003B3F0 File Offset: 0x000395F0
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, base.Name, base.TailCall, parameters ?? this._parameters);
		}

		// Token: 0x04000A14 RID: 2580
		private IReadOnlyList<ParameterExpression> _parameters;
	}
}
