using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000282 RID: 642
	internal sealed class MethodCallExpression0 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012B0 RID: 4784 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public MethodCallExpression0(MethodInfo method) : base(method)
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003AAE1 File Offset: 0x00038CE1
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000023D1 File Offset: 0x000005D1
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003AADA File Offset: 0x00038CDA
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003BA55 File Offset: 0x00039C55
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return arguments == null || arguments.Count == 0;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003BA65 File Offset: 0x00039C65
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method);
		}
	}
}
