using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000253 RID: 595
	internal sealed class TypedDynamicExpression4 : DynamicExpression4
	{
		// Token: 0x06001076 RID: 4214 RVA: 0x00037B05 File Offset: 0x00035D05
		internal TypedDynamicExpression4(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(delegateType, binder, arg0, arg1, arg2, arg3)
		{
			this.Type = retType;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00037B1E File Offset: 0x00035D1E
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000990 RID: 2448
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
