using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024F RID: 591
	internal sealed class TypedDynamicExpression2 : DynamicExpression2
	{
		// Token: 0x06001066 RID: 4198 RVA: 0x0003784F File Offset: 0x00035A4F
		internal TypedDynamicExpression2(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1) : base(delegateType, binder, arg0, arg1)
		{
			this.Type = retType;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00037864 File Offset: 0x00035A64
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000987 RID: 2439
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
