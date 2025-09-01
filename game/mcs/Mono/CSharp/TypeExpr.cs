using System;

namespace Mono.CSharp
{
	// Token: 0x020001B8 RID: 440
	public abstract class TypeExpr : FullNamedExpression
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x0006E0C8 File Offset: 0x0006C2C8
		public sealed override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments)
		{
			this.ResolveAsType(mc, false);
			return this;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0006E0C8 File Offset: 0x0006C2C8
		protected sealed override Expression DoResolve(ResolveContext ec)
		{
			this.ResolveAsType(ec, false);
			return this;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
		public override bool Equals(object obj)
		{
			TypeExpr typeExpr = obj as TypeExpr;
			return typeExpr != null && base.Type == typeExpr.Type;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0006E0FB File Offset: 0x0006C2FB
		public override int GetHashCode()
		{
			return base.Type.GetHashCode();
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0006E108 File Offset: 0x0006C308
		protected TypeExpr()
		{
		}
	}
}
