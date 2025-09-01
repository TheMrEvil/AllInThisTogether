using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024B RID: 587
	internal class TypedDynamicExpressionN : DynamicExpressionN
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x0003767E File Offset: 0x0003587E
		internal TypedDynamicExpressionN(Type returnType, Type delegateType, CallSiteBinder binder, IReadOnlyList<Expression> arguments) : base(delegateType, binder, arguments)
		{
			this.Type = returnType;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00037691 File Offset: 0x00035891
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000982 RID: 2434
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
