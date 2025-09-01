using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F1 RID: 497
	public class RefTypeExpr : ShimExpression
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x000742B9 File Offset: 0x000724B9
		public RefTypeExpr(Expression expr, Location loc) : base(expr)
		{
			this.loc = loc;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0007FC54 File Offset: 0x0007DE54
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.expr = this.expr.Resolve(rc);
			if (this.expr == null)
			{
				return null;
			}
			this.expr = Convert.ImplicitConversionRequired(rc, this.expr, rc.Module.PredefinedTypes.TypedReference.Resolve(), this.loc);
			if (this.expr == null)
			{
				return null;
			}
			this.type = rc.BuiltinTypes.Type;
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0007FCD0 File Offset: 0x0007DED0
		public override void Emit(EmitContext ec)
		{
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Refanytype);
			MethodSpec methodSpec = ec.Module.PredefinedMembers.TypeGetTypeFromHandle.Resolve(this.loc);
			if (methodSpec != null)
			{
				ec.Emit(OpCodes.Call, methodSpec);
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0007FD1F File Offset: 0x0007DF1F
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
