using System;

namespace Mono.CSharp
{
	// Token: 0x020002C0 RID: 704
	public class Unsafe : Statement
	{
		// Token: 0x060021EE RID: 8686 RVA: 0x000A66BA File Offset: 0x000A48BA
		public Unsafe(Block b, Location loc)
		{
			this.Block = b;
			this.Block.Unsafe = true;
			this.loc = loc;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000A66DC File Offset: 0x000A48DC
		public override bool Resolve(BlockContext ec)
		{
			if (ec.CurrentIterator != null)
			{
				ec.Report.Error(1629, this.loc, "Unsafe code may not appear in iterators");
			}
			bool result;
			using (ec.Set(ResolveContext.Options.UnsafeScope))
			{
				result = this.Block.Resolve(ec);
			}
			return result;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000A6744 File Offset: 0x000A4944
		protected override void DoEmit(EmitContext ec)
		{
			this.Block.Emit(ec);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000A6752 File Offset: 0x000A4952
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return this.Block.FlowAnalysis(fc);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000A6760 File Offset: 0x000A4960
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return this.Block.MarkReachable(rc);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000A6776 File Offset: 0x000A4976
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((Unsafe)t).Block = clonectx.LookupBlock(this.Block);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A678F File Offset: 0x000A498F
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C88 RID: 3208
		public Block Block;
	}
}
