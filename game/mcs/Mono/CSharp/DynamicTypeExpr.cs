using System;

namespace Mono.CSharp
{
	// Token: 0x02000160 RID: 352
	internal class DynamicTypeExpr : TypeExpr
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x00047D53 File Offset: 0x00045F53
		public DynamicTypeExpr(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00047D62 File Offset: 0x00045F62
		public override TypeSpec ResolveAsType(IMemberContext ec, bool allowUnboundTypeArguments)
		{
			this.eclass = ExprClass.Type;
			this.type = ec.Module.Compiler.BuiltinTypes.Dynamic;
			return this.type;
		}
	}
}
