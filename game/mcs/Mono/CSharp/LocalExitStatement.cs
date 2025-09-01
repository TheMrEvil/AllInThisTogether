using System;

namespace Mono.CSharp
{
	// Token: 0x020002AD RID: 685
	public abstract class LocalExitStatement : ExitStatement
	{
		// Token: 0x060020DF RID: 8415 RVA: 0x000894FF File Offset: 0x000876FF
		protected LocalExitStatement(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0000212D File Offset: 0x0000032D
		protected override bool IsLocalExit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000A154C File Offset: 0x0009F74C
		protected override bool DoResolve(BlockContext bc)
		{
			if (this.enclosing_loop == null)
			{
				bc.Report.Error(139, this.loc, "No enclosing loop out of which to break or continue");
				return false;
			}
			Block block = this.enclosing_loop.Statement as Block;
			if (block != null)
			{
				base.CheckExitBoundaries(bc, block);
			}
			return true;
		}

		// Token: 0x04000C3A RID: 3130
		protected LoopStatement enclosing_loop;
	}
}
