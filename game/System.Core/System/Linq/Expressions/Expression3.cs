using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000270 RID: 624
	internal sealed class Expression3<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x0003B26E File Offset: 0x0003946E
		public Expression3(Expression body, ParameterExpression par0, ParameterExpression par1, ParameterExpression par2) : base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
			this._par2 = par2;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00034F2A File Offset: 0x0003312A
		internal override int ParameterCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003B28D File Offset: 0x0003948D
		internal override ParameterExpression GetParameter(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			case 1:
				return this._par1;
			case 2:
				return this._par2;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003B2C8 File Offset: 0x000394C8
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 3)
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
						if (enumerator.Current == this._par1)
						{
							enumerator.MoveNext();
							return enumerator.Current == this._par2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003B360 File Offset: 0x00039560
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003B36E File Offset: 0x0003956E
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1,
				this._par2
			});
		}

		// Token: 0x04000A11 RID: 2577
		private object _par0;

		// Token: 0x04000A12 RID: 2578
		private readonly ParameterExpression _par1;

		// Token: 0x04000A13 RID: 2579
		private readonly ParameterExpression _par2;
	}
}
