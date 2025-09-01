using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000251 RID: 593
	internal sealed class TypedDynamicExpression3 : DynamicExpression3
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x0003798A File Offset: 0x00035B8A
		internal TypedDynamicExpression3(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2) : base(delegateType, binder, arg0, arg1, arg2)
		{
			this.Type = retType;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000379A1 File Offset: 0x00035BA1
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x0400098B RID: 2443
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
