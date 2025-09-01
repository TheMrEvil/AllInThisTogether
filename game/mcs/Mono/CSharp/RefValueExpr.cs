using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F0 RID: 496
	public class RefValueExpr : ShimExpression, IAssignMethod, IMemoryLocation
	{
		// Token: 0x060019F2 RID: 6642 RVA: 0x0007FAF3 File Offset: 0x0007DCF3
		public RefValueExpr(Expression expr, FullNamedExpression texpr, Location loc) : base(expr)
		{
			this.texpr = texpr;
			this.loc = loc;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0007FB0A File Offset: 0x0007DD0A
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.texpr;
			}
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0007FB12 File Offset: 0x0007DD12
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Refanyval, this.type);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0007FB34 File Offset: 0x0007DD34
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.expr = this.expr.Resolve(rc);
			this.type = this.texpr.ResolveAsType(rc, false);
			if (this.expr == null || this.type == null)
			{
				return null;
			}
			this.expr = Convert.ImplicitConversionRequired(rc, this.expr, rc.Module.PredefinedTypes.TypedReference.Resolve(), this.loc);
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0007568C File Offset: 0x0007388C
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			return this.DoResolve(rc);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0007FBAD File Offset: 0x0007DDAD
		public override void Emit(EmitContext ec)
		{
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Refanyval, this.type);
			ec.EmitLoadFromPtr(this.type);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void Emit(EmitContext ec, bool leave_copy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0007FBD8 File Offset: 0x0007DDD8
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Refanyval, this.type);
			source.Emit(ec);
			LocalTemporary localTemporary = null;
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				localTemporary = new LocalTemporary(source.Type);
				localTemporary.Store(ec);
			}
			ec.EmitStoreFromPtr(this.type);
			if (localTemporary != null)
			{
				localTemporary.Emit(ec);
				localTemporary.Release(ec);
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0007FC49 File Offset: 0x0007DE49
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009E0 RID: 2528
		private FullNamedExpression texpr;
	}
}
