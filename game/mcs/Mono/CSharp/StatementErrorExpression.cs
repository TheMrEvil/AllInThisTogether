using System;

namespace Mono.CSharp
{
	// Token: 0x020002A1 RID: 673
	public class StatementErrorExpression : Statement
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x000A03F6 File Offset: 0x0009E5F6
		public StatementErrorExpression(Expression expr)
		{
			this.expr = expr;
			this.loc = expr.StartLocation;
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000A0411 File Offset: 0x0009E611
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000A0419 File Offset: 0x0009E619
		public override bool Resolve(BlockContext bc)
		{
			this.expr.Error_InvalidExpressionStatement(bc);
			return true;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0000225C File Offset: 0x0000045C
		protected override void DoEmit(EmitContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000022F4 File Offset: 0x000004F4
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return false;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000A0428 File Offset: 0x0009E628
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			((StatementErrorExpression)target).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000A0441 File Offset: 0x0009E641
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C28 RID: 3112
		private Expression expr;
	}
}
