using System;

namespace Mono.CSharp
{
	// Token: 0x02000119 RID: 281
	public class Await : ExpressionStatement
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x00033110 File Offset: 0x00031310
		public Await(Expression expr, Location loc)
		{
			this.expr = expr;
			this.loc = loc;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00033126 File Offset: 0x00031326
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0003312E File Offset: 0x0003132E
		public AwaitStatement Statement
		{
			get
			{
				return this.stmt;
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00033136 File Offset: 0x00031336
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			((Await)target).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003314F File Offset: 0x0003134F
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotImplementedException("ET");
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool ContainsEmitWithAwait()
		{
			return true;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003315B File Offset: 0x0003135B
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.stmt.Expr.FlowAnalysis(fc);
			this.stmt.RegisterResumePoint();
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003317C File Offset: 0x0003137C
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (rc.HasSet(ResolveContext.Options.LockScope))
			{
				rc.Report.Error(1996, this.loc, "The `await' operator cannot be used in the body of a lock statement");
			}
			if (rc.IsUnsafe)
			{
				rc.Report.Error(4004, this.loc, "The `await' operator cannot be used in an unsafe context");
			}
			BlockContext bc = (BlockContext)rc;
			this.stmt = new AwaitStatement(this.expr, this.loc);
			if (!this.stmt.Resolve(bc))
			{
				return null;
			}
			this.type = this.stmt.ResultType;
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003321C File Offset: 0x0003141C
		public override void Emit(EmitContext ec)
		{
			this.stmt.EmitPrologue(ec);
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				this.stmt.Emit(ec);
			}
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003326C File Offset: 0x0003146C
		public override Expression EmitToField(EmitContext ec)
		{
			this.stmt.EmitPrologue(ec);
			return this.stmt.GetResultExpression(ec);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00033286 File Offset: 0x00031486
		public void EmitAssign(EmitContext ec, FieldExpr field)
		{
			this.stmt.EmitPrologue(ec);
			field.InstanceExpression.Emit(ec);
			this.stmt.Emit(ec);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000332AC File Offset: 0x000314AC
		public override void EmitStatement(EmitContext ec)
		{
			this.stmt.EmitStatement(ec);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000332BA File Offset: 0x000314BA
		public override void MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			this.stmt.MarkReachable(rc);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000332D0 File Offset: 0x000314D0
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400066C RID: 1644
		private Expression expr;

		// Token: 0x0400066D RID: 1645
		private AwaitStatement stmt;
	}
}
