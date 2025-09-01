using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002A7 RID: 679
	public class GotoDefault : SwitchGoto
	{
		// Token: 0x060020B8 RID: 8376 RVA: 0x000A0FA1 File Offset: 0x0009F1A1
		public GotoDefault(Location l) : base(l)
		{
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000A0FAA File Offset: 0x0009F1AA
		public override bool Resolve(BlockContext bc)
		{
			if (bc.Switch == null)
			{
				base.Error_GotoCaseRequiresSwitchBlock(bc);
				return false;
			}
			bc.Switch.RegisterGotoCase(null, null);
			base.Resolve(bc);
			return true;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000A0FD3 File Offset: 0x0009F1D3
		protected override void DoEmit(EmitContext ec)
		{
			ec.Emit(this.unwind_protect ? OpCodes.Leave : OpCodes.Br, ec.Switch.DefaultLabel.GetILLabel(ec));
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A1000 File Offset: 0x0009F200
		public override Reachability MarkReachable(Reachability rc)
		{
			if (!rc.IsUnreachable)
			{
				SwitchLabel defaultLabel = this.switch_statement.DefaultLabel;
				if (defaultLabel.IsUnreachable)
				{
					defaultLabel.MarkReachable(rc);
					this.switch_statement.Block.ScanGotoJump(defaultLabel);
				}
			}
			return base.MarkReachable(rc);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000A104A File Offset: 0x0009F24A
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
