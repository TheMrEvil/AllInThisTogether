using System;

namespace Mono.CSharp
{
	// Token: 0x020002A0 RID: 672
	public class StatementExpression : Statement
	{
		// Token: 0x06002079 RID: 8313 RVA: 0x000A0345 File Offset: 0x0009E545
		public StatementExpression(ExpressionStatement expr)
		{
			this.expr = expr;
			this.loc = expr.StartLocation;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000A0360 File Offset: 0x0009E560
		public StatementExpression(ExpressionStatement expr, Location loc)
		{
			this.expr = expr;
			this.loc = loc;
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x000A0376 File Offset: 0x0009E576
		public ExpressionStatement Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000A037E File Offset: 0x0009E57E
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((StatementExpression)t).expr = (ExpressionStatement)this.expr.Clone(clonectx);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000A039C File Offset: 0x0009E59C
		protected override void DoEmit(EmitContext ec)
		{
			this.expr.EmitStatement(ec);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000A03AA File Offset: 0x0009E5AA
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
			return false;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000A03B9 File Offset: 0x0009E5B9
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			this.expr.MarkReachable(rc);
			return rc;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000A03D0 File Offset: 0x0009E5D0
		public override bool Resolve(BlockContext ec)
		{
			this.expr = this.expr.ResolveStatement(ec);
			return this.expr != null;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000A03ED File Offset: 0x0009E5ED
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C27 RID: 3111
		private ExpressionStatement expr;
	}
}
