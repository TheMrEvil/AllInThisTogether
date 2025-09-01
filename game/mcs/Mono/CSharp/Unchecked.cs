using System;

namespace Mono.CSharp
{
	// Token: 0x020002BE RID: 702
	public class Unchecked : Statement
	{
		// Token: 0x060021E0 RID: 8672 RVA: 0x000A64E0 File Offset: 0x000A46E0
		public Unchecked(Block b, Location loc)
		{
			this.Block = b;
			b.Unchecked = true;
			this.loc = loc;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000A6500 File Offset: 0x000A4700
		public override bool Resolve(BlockContext ec)
		{
			bool result;
			using (ec.With(ResolveContext.Options.AllCheckStateFlags, false))
			{
				result = this.Block.Resolve(ec);
			}
			return result;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000A6544 File Offset: 0x000A4744
		protected override void DoEmit(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.CheckedScope, false))
			{
				this.Block.Emit(ec);
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000A6588 File Offset: 0x000A4788
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return this.Block.FlowAnalysis(fc);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000A6596 File Offset: 0x000A4796
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return this.Block.MarkReachable(rc);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000A65AC File Offset: 0x000A47AC
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((Unchecked)t).Block = clonectx.LookupBlock(this.Block);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000A65C5 File Offset: 0x000A47C5
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C86 RID: 3206
		public Block Block;
	}
}
