using System;

namespace Mono.CSharp
{
	// Token: 0x02000229 RID: 553
	internal class GenericOpenTypeExpr : TypeExpression
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x00087D39 File Offset: 0x00085F39
		public GenericOpenTypeExpr(TypeSpec type, Location loc) : base(type.GetDefinition(), loc)
		{
		}
	}
}
