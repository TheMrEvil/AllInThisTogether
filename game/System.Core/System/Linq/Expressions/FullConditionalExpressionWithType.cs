using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000240 RID: 576
	internal sealed class FullConditionalExpressionWithType : FullConditionalExpression
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x00035878 File Offset: 0x00033A78
		internal FullConditionalExpressionWithType(Expression test, Expression ifTrue, Expression ifFalse, Type type) : base(test, ifTrue, ifFalse)
		{
			this.Type = type;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003588B File Offset: 0x00033A8B
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x04000966 RID: 2406
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
