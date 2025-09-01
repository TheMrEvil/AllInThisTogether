using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000242 RID: 578
	internal class TypedConstantExpression : ConstantExpression
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x000358D7 File Offset: 0x00033AD7
		internal TypedConstantExpression(object value, Type type) : base(value)
		{
			this.Type = type;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000358E7 File Offset: 0x00033AE7
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000968 RID: 2408
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
