using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001CE RID: 462
	public class Indirection : Expression, IMemoryLocation, IAssignMethod, IFixedExpression
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x00075545 File Offset: 0x00073745
		public Indirection(Expression expr, Location l)
		{
			this.expr = expr;
			this.loc = l;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0007555B File Offset: 0x0007375B
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0000212D File Offset: 0x0000032D
		public bool IsFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00075563 File Offset: 0x00073763
		public override Location StartLocation
		{
			get
			{
				return this.expr.StartLocation;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00075570 File Offset: 0x00073770
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((Indirection)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override bool ContainsEmitWithAwait()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00075589 File Offset: 0x00073789
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			base.Error_PointerInsideExpressionTree(ec);
			return null;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00075593 File Offset: 0x00073793
		public override void Emit(EmitContext ec)
		{
			if (!this.prepared)
			{
				this.expr.Emit(ec);
			}
			ec.EmitLoadFromPtr(base.Type);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000755B5 File Offset: 0x000737B5
		public void Emit(EmitContext ec, bool leave_copy)
		{
			this.Emit(ec);
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				this.temporary = new LocalTemporary(this.expr.Type);
				this.temporary.Store(ec);
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000755F0 File Offset: 0x000737F0
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			this.prepared = isCompound;
			this.expr.Emit(ec);
			if (isCompound)
			{
				ec.Emit(OpCodes.Dup);
			}
			source.Emit(ec);
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				this.temporary = new LocalTemporary(source.Type);
				this.temporary.Store(ec);
			}
			ec.EmitStoreFromPtr(this.type);
			if (this.temporary != null)
			{
				this.temporary.Emit(ec);
				this.temporary.Release(ec);
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0007567E File Offset: 0x0007387E
		public void AddressOf(EmitContext ec, AddressOp Mode)
		{
			this.expr.Emit(ec);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0007568C File Offset: 0x0007388C
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return this.DoResolve(ec);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00075698 File Offset: 0x00073898
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return null;
			}
			if (!ec.IsUnsafe)
			{
				Expression.UnsafeError(ec, this.loc);
			}
			PointerContainer pointerContainer = this.expr.Type as PointerContainer;
			if (pointerContainer == null)
			{
				ec.Report.Error(193, this.loc, "The * or -> operator must be applied to a pointer");
				return null;
			}
			this.type = pointerContainer.Element;
			if (this.type.Kind == MemberKind.Void)
			{
				base.Error_VoidPointerOperation(ec);
				return null;
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00075735 File Offset: 0x00073935
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000998 RID: 2456
		private Expression expr;

		// Token: 0x04000999 RID: 2457
		private LocalTemporary temporary;

		// Token: 0x0400099A RID: 2458
		private bool prepared;
	}
}
