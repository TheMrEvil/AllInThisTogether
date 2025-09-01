using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000288 RID: 648
	internal sealed class InstanceMethodCallExpression0 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x0003C109 File Offset: 0x0003A309
		public InstanceMethodCallExpression0(MethodInfo method, Expression instance) : base(method, instance)
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003AAE1 File Offset: 0x00038CE1
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000023D1 File Offset: 0x000005D1
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0003AADA File Offset: 0x00038CDA
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0003BA55 File Offset: 0x00039C55
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return arguments == null || arguments.Count == 0;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003C113 File Offset: 0x0003A313
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method);
		}
	}
}
