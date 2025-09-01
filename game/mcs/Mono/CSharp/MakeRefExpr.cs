using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F2 RID: 498
	public class MakeRefExpr : ShimExpression
	{
		// Token: 0x06001A00 RID: 6656 RVA: 0x000742B9 File Offset: 0x000724B9
		public MakeRefExpr(Expression expr, Location loc) : base(expr)
		{
			this.loc = loc;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override bool ContainsEmitWithAwait()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0007FD28 File Offset: 0x0007DF28
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.expr = this.expr.ResolveLValue(rc, EmptyExpression.LValueMemberAccess);
			this.type = rc.Module.PredefinedTypes.TypedReference.Resolve();
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0007FD64 File Offset: 0x0007DF64
		public override void Emit(EmitContext ec)
		{
			((IMemoryLocation)this.expr).AddressOf(ec, AddressOp.Load);
			ec.Emit(OpCodes.Mkrefany, this.expr.Type);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0007FD8E File Offset: 0x0007DF8E
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
