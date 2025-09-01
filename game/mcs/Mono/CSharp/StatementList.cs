using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020002A2 RID: 674
	public class StatementList : Statement
	{
		// Token: 0x06002089 RID: 8329 RVA: 0x000A044A File Offset: 0x0009E64A
		public StatementList(Statement first, Statement second)
		{
			this.statements = new List<Statement>
			{
				first,
				second
			};
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x000A046B File Offset: 0x0009E66B
		public IList<Statement> Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000A0473 File Offset: 0x0009E673
		public void Add(Statement statement)
		{
			this.statements.Add(statement);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000A0484 File Offset: 0x0009E684
		public override bool Resolve(BlockContext ec)
		{
			foreach (Statement statement in this.statements)
			{
				statement.Resolve(ec);
			}
			return true;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000A04D8 File Offset: 0x0009E6D8
		protected override void DoEmit(EmitContext ec)
		{
			foreach (Statement statement in this.statements)
			{
				statement.Emit(ec);
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000A052C File Offset: 0x0009E72C
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			foreach (Statement statement in this.statements)
			{
				statement.FlowAnalysis(fc);
			}
			return false;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000A0580 File Offset: 0x0009E780
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			Reachability result = rc;
			foreach (Statement statement in this.statements)
			{
				result = statement.MarkReachable(rc);
			}
			return result;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000A05E0 File Offset: 0x0009E7E0
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			StatementList statementList = (StatementList)target;
			statementList.statements = new List<Statement>(this.statements.Count);
			foreach (Statement statement in this.statements)
			{
				statementList.statements.Add(statement.Clone(clonectx));
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000A065C File Offset: 0x0009E85C
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C29 RID: 3113
		private List<Statement> statements;
	}
}
