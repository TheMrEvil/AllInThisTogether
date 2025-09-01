using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000263 RID: 611
	internal sealed class InvocationExpression0 : InvocationExpression
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x0003AAD0 File Offset: 0x00038CD0
		public InvocationExpression0(Expression lambda, Type returnType) : base(lambda, returnType)
		{
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003AADA File Offset: 0x00038CDA
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003AAE1 File Offset: 0x00038CE1
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000023D1 File Offset: 0x000005D1
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003AAED File Offset: 0x00038CED
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda);
		}
	}
}
