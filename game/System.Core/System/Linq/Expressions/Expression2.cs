using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200026F RID: 623
	internal sealed class Expression2<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x0003B16D File Offset: 0x0003936D
		public Expression2(Expression body, ParameterExpression par0, ParameterExpression par1) : base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00034E1C File Offset: 0x0003301C
		internal override int ParameterCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0003B184 File Offset: 0x00039384
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._par1;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003B1AC File Offset: 0x000393AC
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 2)
			{
				ReadOnlyCollection<ParameterExpression> readOnlyCollection = this._par0 as ReadOnlyCollection<ParameterExpression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<ParameterExpression>(parameters, readOnlyCollection);
				}
				using (IEnumerator<ParameterExpression> enumerator = parameters.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._par0)
					{
						enumerator.MoveNext();
						return enumerator.Current == this._par1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0003B230 File Offset: 0x00039430
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0003B23E File Offset: 0x0003943E
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1
			});
		}

		// Token: 0x04000A0F RID: 2575
		private object _par0;

		// Token: 0x04000A10 RID: 2576
		private readonly ParameterExpression _par1;
	}
}
