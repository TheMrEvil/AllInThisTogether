using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200026D RID: 621
	internal sealed class Expression0<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x0003B091 File Offset: 0x00039291
		public Expression0(Expression body) : base(body)
		{
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000023D1 File Offset: 0x000005D1
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00034D08 File Offset: 0x00032F08
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			return parameters == null || parameters.Count == 0;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003B09A File Offset: 0x0003929A
		internal override ParameterExpression GetParameter(int index)
		{
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00034D1F File Offset: 0x00032F1F
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0003B0A6 File Offset: 0x000392A6
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, parameters);
		}
	}
}
