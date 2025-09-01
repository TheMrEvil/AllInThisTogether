using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200026E RID: 622
	internal sealed class Expression1<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x0003B0AF File Offset: 0x000392AF
		public Expression1(Expression body, ParameterExpression par0) : base(body)
		{
			this._par0 = par0;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00007E1D File Offset: 0x0000601D
		internal override int ParameterCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003B0BF File Offset: 0x000392BF
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0003B0DC File Offset: 0x000392DC
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 1)
			{
				using (IEnumerator<ParameterExpression> enumerator = parameters.GetEnumerator())
				{
					enumerator.MoveNext();
					return enumerator.Current == ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0003B138 File Offset: 0x00039338
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003B146 File Offset: 0x00039346
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0)
			});
		}

		// Token: 0x04000A0E RID: 2574
		private object _par0;
	}
}
