using System;

namespace Mono.CSharp
{
	// Token: 0x02000223 RID: 547
	public class TypeParameterExpr : TypeExpression
	{
		// Token: 0x06001BD9 RID: 7129 RVA: 0x00086D4A File Offset: 0x00084F4A
		public TypeParameterExpr(TypeParameter type_parameter, Location loc) : base(type_parameter.Type, loc)
		{
			this.eclass = ExprClass.TypeParameter;
		}
	}
}
