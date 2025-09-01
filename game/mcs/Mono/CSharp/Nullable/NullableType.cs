using System;

namespace Mono.CSharp.Nullable
{
	// Token: 0x020002FB RID: 763
	public class NullableType : TypeExpr
	{
		// Token: 0x06002444 RID: 9284 RVA: 0x000ADBEF File Offset: 0x000ABDEF
		public NullableType(TypeSpec type, Location loc)
		{
			this.underlying = type;
			this.loc = loc;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000ADC08 File Offset: 0x000ABE08
		public override TypeSpec ResolveAsType(IMemberContext ec, bool allowUnboundTypeArguments = false)
		{
			this.eclass = ExprClass.Type;
			TypeSpec typeSpec = ec.Module.PredefinedTypes.Nullable.Resolve();
			if (typeSpec == null)
			{
				return null;
			}
			TypeArguments args = new TypeArguments(new FullNamedExpression[]
			{
				new TypeExpression(this.underlying, this.loc)
			});
			GenericTypeExpr genericTypeExpr = new GenericTypeExpr(typeSpec, args, this.loc);
			this.type = genericTypeExpr.ResolveAsType(ec, false);
			return this.type;
		}

		// Token: 0x04000D8B RID: 3467
		private readonly TypeSpec underlying;
	}
}
