using System;

namespace Mono.CSharp
{
	// Token: 0x020002A3 RID: 675
	public abstract class ExitStatement : Statement
	{
		// Token: 0x06002092 RID: 8338
		protected abstract bool DoResolve(BlockContext bc);

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002093 RID: 8339
		protected abstract bool IsLocalExit { get; }

		// Token: 0x06002094 RID: 8340 RVA: 0x000A0668 File Offset: 0x0009E868
		public override bool Resolve(BlockContext bc)
		{
			bool result = this.DoResolve(bc);
			if (!this.IsLocalExit && bc.HasSet(ResolveContext.Options.FinallyScope))
			{
				for (Block block = bc.CurrentBlock; block != null; block = block.Parent)
				{
					if (block.IsFinallyBlock)
					{
						base.Error_FinallyClauseExit(bc);
						break;
					}
					if (block is ParametersBlock)
					{
						break;
					}
				}
			}
			this.unwind_protect = bc.HasAny(ResolveContext.Options.CatchScope | ResolveContext.Options.TryScope);
			return result;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000A06CD File Offset: 0x0009E8CD
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.IsLocalExit)
			{
				return true;
			}
			if (fc.TryFinally != null)
			{
				fc.TryFinally.RegisterForControlExitCheck(new DefiniteAssignmentBitSet(fc.DefiniteAssignment));
			}
			else
			{
				fc.ParametersBlock.CheckControlExit(fc);
			}
			return true;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000A0706 File Offset: 0x0009E906
		protected ExitStatement()
		{
		}

		// Token: 0x04000C2A RID: 3114
		protected bool unwind_protect;
	}
}
