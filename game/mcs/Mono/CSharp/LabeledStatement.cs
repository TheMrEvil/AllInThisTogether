using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002A6 RID: 678
	public class LabeledStatement : Statement
	{
		// Token: 0x060020AD RID: 8365 RVA: 0x000A0E86 File Offset: 0x0009F086
		public LabeledStatement(string name, Block block, Location l)
		{
			this.name = name;
			this.block = block;
			this.loc = l;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000A0EA3 File Offset: 0x0009F0A3
		public Label LabelTarget(EmitContext ec)
		{
			if (this.defined)
			{
				return this.label;
			}
			this.label = ec.DefineLabel();
			this.defined = true;
			return this.label;
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x000A0ECD File Offset: 0x0009F0CD
		public Block Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x000A0ED5 File Offset: 0x0009F0D5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000A0EDD File Offset: 0x0009F0DD
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			((LabeledStatement)target).block = clonectx.RemapBlockCopy(this.block);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool Resolve(BlockContext bc)
		{
			return true;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A0EF6 File Offset: 0x0009F0F6
		protected override void DoEmit(EmitContext ec)
		{
			this.LabelTarget(ec);
			ec.MarkLabel(this.label);
			if (this.finalTarget)
			{
				ec.Emit(OpCodes.Br_S, this.label);
			}
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A0F25 File Offset: 0x0009F125
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (!this.referenced)
			{
				fc.Report.Warning(164, 2, this.loc, "This label has not been referenced");
			}
			return false;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A0F4C File Offset: 0x0009F14C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			if (this.referenced)
			{
				rc = default(Reachability);
			}
			return rc;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000A0F67 File Offset: 0x0009F167
		public void AddGotoReference(Reachability rc, bool finalTarget)
		{
			if (this.referenced)
			{
				return;
			}
			this.referenced = true;
			this.MarkReachable(rc);
			if (finalTarget)
			{
				this.finalTarget = true;
				return;
			}
			this.block.ScanGotoJump(this);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000A0F98 File Offset: 0x0009F198
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C2F RID: 3119
		private string name;

		// Token: 0x04000C30 RID: 3120
		private bool defined;

		// Token: 0x04000C31 RID: 3121
		private bool referenced;

		// Token: 0x04000C32 RID: 3122
		private bool finalTarget;

		// Token: 0x04000C33 RID: 3123
		private Label label;

		// Token: 0x04000C34 RID: 3124
		private Block block;
	}
}
