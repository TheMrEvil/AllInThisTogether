using System;

namespace Mono.CSharp
{
	// Token: 0x020002BF RID: 703
	public class Checked : Statement
	{
		// Token: 0x060021E7 RID: 8679 RVA: 0x000A65CE File Offset: 0x000A47CE
		public Checked(Block b, Location loc)
		{
			this.Block = b;
			b.Unchecked = false;
			this.loc = loc;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000A65EC File Offset: 0x000A47EC
		public override bool Resolve(BlockContext ec)
		{
			bool result;
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, true))
			{
				result = this.Block.Resolve(ec);
			}
			return result;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000A6630 File Offset: 0x000A4830
		protected override void DoEmit(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, true))
			{
				this.Block.Emit(ec);
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000A6674 File Offset: 0x000A4874
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return this.Block.FlowAnalysis(fc);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000A6682 File Offset: 0x000A4882
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return this.Block.MarkReachable(rc);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000A6698 File Offset: 0x000A4898
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((Checked)t).Block = clonectx.LookupBlock(this.Block);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000A66B1 File Offset: 0x000A48B1
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C87 RID: 3207
		public Block Block;
	}
}
