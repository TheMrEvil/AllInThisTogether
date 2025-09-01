using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000290 RID: 656
	internal sealed class NewValueTypeExpression : NewExpression
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x0003C5AD File Offset: 0x0003A7AD
		internal NewValueTypeExpression(Type type, ReadOnlyCollection<Expression> arguments, ReadOnlyCollection<MemberInfo> members) : base(null, arguments, members)
		{
			this.Type = type;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0003C5BF File Offset: 0x0003A7BF
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000A48 RID: 2632
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
