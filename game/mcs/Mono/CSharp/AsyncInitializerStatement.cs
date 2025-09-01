using System;

namespace Mono.CSharp
{
	// Token: 0x0200011B RID: 283
	internal class AsyncInitializerStatement : StatementExpression
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x000337A5 File Offset: 0x000319A5
		public AsyncInitializerStatement(AsyncInitializer expr) : base(expr)
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000337B0 File Offset: 0x000319B0
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			base.DoFlowAnalysis(fc);
			AsyncInitializer asyncInitializer = (AsyncInitializer)base.Expr;
			bool flag = !asyncInitializer.Block.HasReachableClosingBrace;
			return !((AsyncTaskStorey)asyncInitializer.Storey).ReturnType.IsGenericTask || flag;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000337F8 File Offset: 0x000319F8
		public override Reachability MarkReachable(Reachability rc)
		{
			if (!rc.IsUnreachable)
			{
				this.reachable = true;
			}
			AsyncInitializer asyncInitializer = (AsyncInitializer)base.Expr;
			rc = asyncInitializer.Block.MarkReachable(rc);
			AsyncTaskStorey asyncTaskStorey = (AsyncTaskStorey)asyncInitializer.Storey;
			if (asyncTaskStorey.ReturnType != null && asyncTaskStorey.ReturnType.IsGenericTask)
			{
				return rc;
			}
			return Reachability.CreateUnreachable();
		}
	}
}
