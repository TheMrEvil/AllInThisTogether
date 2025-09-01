using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200024D RID: 589
	internal sealed class TypedDynamicExpression1 : DynamicExpression1
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x00037748 File Offset: 0x00035948
		internal TypedDynamicExpression1(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0) : base(delegateType, binder, arg0)
		{
			this.Type = retType;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0003775B File Offset: 0x0003595B
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000984 RID: 2436
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
