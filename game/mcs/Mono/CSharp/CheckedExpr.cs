using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001FB RID: 507
	public class CheckedExpr : Expression
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x00080F97 File Offset: 0x0007F197
		public CheckedExpr(Expression e, Location l)
		{
			this.Expr = e;
			this.loc = l;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00080FAD File Offset: 0x0007F1AD
		public override bool ContainsEmitWithAwait()
		{
			return this.Expr.ContainsEmitWithAwait();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00080FBC File Offset: 0x0007F1BC
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Expression result;
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, true))
			{
				result = this.Expr.CreateExpressionTree(ec);
			}
			return result;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00081000 File Offset: 0x0007F200
		protected override Expression DoResolve(ResolveContext ec)
		{
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, true))
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

		// Token: 0x06001A53 RID: 6739 RVA: 0x000810B0 File Offset: 0x0007F2B0
		public override void Emit(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, true))
			{
				this.Expr.Emit(ec);
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000810F4 File Offset: 0x0007F2F4
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, true))
			{
				this.Expr.EmitBranchable(ec, target, on_true);
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00081138 File Offset: 0x0007F338
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.Expr.FlowAnalysis(fc);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00081148 File Offset: 0x0007F348
		public override Expression MakeExpression(BuilderContext ctx)
		{
			Expression result;
			using (ctx.With(BuilderContext.Options.CheckedScope, true))
			{
				result = this.Expr.MakeExpression(ctx);
			}
			return result;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0008118C File Offset: 0x0007F38C
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((CheckedExpr)t).Expr = this.Expr.Clone(clonectx);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000811A5 File Offset: 0x0007F3A5
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009E9 RID: 2537
		public Expression Expr;
	}
}
