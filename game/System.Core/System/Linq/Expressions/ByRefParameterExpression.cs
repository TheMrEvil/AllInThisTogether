using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000292 RID: 658
	internal sealed class ByRefParameterExpression : TypedParameterExpression
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0003C72F File Offset: 0x0003A92F
		internal ByRefParameterExpression(Type type, string name) : base(type, name)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00007E1D File Offset: 0x0000601D
		internal override bool GetIsByRef()
		{
			return true;
		}
	}
}
