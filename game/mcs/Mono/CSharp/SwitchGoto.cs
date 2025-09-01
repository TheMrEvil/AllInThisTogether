using System;

namespace Mono.CSharp
{
	// Token: 0x020002A9 RID: 681
	public abstract class SwitchGoto : Statement
	{
		// Token: 0x060020C6 RID: 8390 RVA: 0x0009F6CA File Offset: 0x0009D8CA
		protected SwitchGoto(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A11D2 File Offset: 0x0009F3D2
		public override bool Resolve(BlockContext bc)
		{
			base.CheckExitBoundaries(bc, bc.Switch.Block);
			this.unwind_protect = bc.HasAny(ResolveContext.Options.CatchScope | ResolveContext.Options.TryScope);
			this.switch_statement = bc.Switch;
			return true;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0000212D File Offset: 0x0000032D
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return true;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0008953C File Offset: 0x0008773C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return Reachability.CreateUnreachable();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000A1204 File Offset: 0x0009F404
		protected void Error_GotoCaseRequiresSwitchBlock(BlockContext bc)
		{
			bc.Report.Error(153, this.loc, "A goto case is only valid inside a switch statement");
		}

		// Token: 0x04000C37 RID: 3127
		protected bool unwind_protect;

		// Token: 0x04000C38 RID: 3128
		protected Switch switch_statement;
	}
}
