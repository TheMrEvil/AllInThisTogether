using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000237 RID: 567
	internal sealed class ScopeWithType : ScopeN
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x00035405 File Offset: 0x00033605
		internal ScopeWithType(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> expressions, Type type) : base(variables, expressions)
		{
			this.Type = type;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00035416 File Offset: 0x00033616
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003541E File Offset: 0x0003361E
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeWithType(variables, base.Body, this.Type);
			}
			return new ScopeWithType(base.ReuseOrValidateVariables(variables), args, this.Type);
		}

		// Token: 0x04000953 RID: 2387
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
