using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001FC RID: 508
	public class UnCheckedExpr : Expression
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x000811AE File Offset: 0x0007F3AE
		public UnCheckedExpr(Expression e, Location l)
		{
			this.Expr = e;
			this.loc = l;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000811C4 File Offset: 0x0007F3C4
		public override bool ContainsEmitWithAwait()
		{
			return this.Expr.ContainsEmitWithAwait();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000811D4 File Offset: 0x0007F3D4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Expression result;
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, false))
			{
				result = this.Expr.CreateExpressionTree(ec);
			}
			return result;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00081218 File Offset: 0x0007F418
		protected override Expression DoResolve(ResolveContext ec)
		{
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, false))
			{
				this.Expr = this.Expr.Resolve(ec);
			}
			if (this.Expr == null)
			{
				return null;
			}
			if (this.Expr is Constant || this.Expr is MethodGroupExpr || this.Expr is AnonymousMethodExpression || this.Expr is DefaultValueExpression)
			{
				return this.Expr;
			}
			this.eclass = this.Expr.eclass;
			this.type = this.Expr.Type;
			return this;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000812C8 File Offset: 0x0007F4C8
		public override void Emit(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, false))
			{
				this.Expr.Emit(ec);
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0008130C File Offset: 0x0007F50C
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, false))
			{
				this.Expr.EmitBranchable(ec, target, on_true);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00081350 File Offset: 0x0007F550
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.Expr.FlowAnalysis(fc);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0008135E File Offset: 0x0007F55E
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((UnCheckedExpr)t).Expr = this.Expr.Clone(clonectx);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00081377 File Offset: 0x0007F577
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009EA RID: 2538
		public Expression Expr;
	}
}
