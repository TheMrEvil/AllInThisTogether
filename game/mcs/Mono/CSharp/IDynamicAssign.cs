using System;
using System.Linq.Expressions;

namespace Mono.CSharp
{
	// Token: 0x02000164 RID: 356
	internal interface IDynamicAssign : IAssignMethod
	{
		// Token: 0x06001180 RID: 4480
		Expression MakeAssignExpression(BuilderContext ctx, Expression source);
	}
}
