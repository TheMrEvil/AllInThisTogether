using System;

namespace Mono.CSharp
{
	// Token: 0x020001B9 RID: 441
	public class TypeExpression : TypeExpr
	{
		// Token: 0x06001711 RID: 5905 RVA: 0x0006E110 File Offset: 0x0006C310
		public TypeExpression(TypeSpec t, Location l)
		{
			base.Type = t;
			this.eclass = ExprClass.Type;
			this.loc = l;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0006AD14 File Offset: 0x00068F14
		public sealed override TypeSpec ResolveAsType(IMemberContext mc, bool allowUnboundTypeArguments = false)
		{
			return this.type;
		}
	}
}
