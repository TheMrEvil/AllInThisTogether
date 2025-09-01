using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002A5 RID: 677
	public class Goto : ExitStatement
	{
		// Token: 0x060020A2 RID: 8354 RVA: 0x000A0C93 File Offset: 0x0009EE93
		public Goto(string label, Location l)
		{
			this.loc = l;
			this.target = label;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x000A0CA9 File Offset: 0x0009EEA9
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0000212D File Offset: 0x0000032D
		protected override bool IsLocalExit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000A0CB4 File Offset: 0x0009EEB4
		protected override bool DoResolve(BlockContext bc)
		{
			this.label = bc.CurrentBlock.LookupLabel(this.target);
			if (this.label == null)
			{
				Goto.Error_UnknownLabel(bc, this.target, this.loc);
				return false;
			}
			this.try_finally = (bc.CurrentTryBlock as TryFinally);
			base.CheckExitBoundaries(bc, this.label.Block);
			return true;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000A0D18 File Offset: 0x0009EF18
		public static void Error_UnknownLabel(BlockContext bc, string label, Location loc)
		{
			bc.Report.Error(159, loc, "The label `{0}:' could not be found within the scope of the goto statement", label);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000A0D31 File Offset: 0x0009EF31
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (fc.AddReachedLabel(this.label))
			{
				return true;
			}
			this.label.Block.ScanGotoJump(this.label, fc);
			return true;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000A0D5C File Offset: 0x0009EF5C
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			base.MarkReachable(rc);
			if (this.try_finally != null)
			{
				if (this.try_finally.FinallyBlock.HasReachableClosingBrace)
				{
					this.label.AddGotoReference(rc, false);
				}
				else
				{
					this.label.AddGotoReference(rc, true);
				}
			}
			else
			{
				this.label.AddGotoReference(rc, false);
			}
			return Reachability.CreateUnreachable();
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000A0DC8 File Offset: 0x0009EFC8
		protected override void DoEmit(EmitContext ec)
		{
			if (this.label == null)
			{
				throw new InternalErrorException("goto emitted before target resolved");
			}
			Label label = this.label.LabelTarget(ec);
			if (ec.TryFinallyUnwind != null && this.IsLeavingFinally(this.label.Block))
			{
				AsyncInitializer initializer = (AsyncInitializer)ec.CurrentAnonymousMethod;
				label = TryFinally.EmitRedirectedJump(ec, initializer, label, this.label.Block);
			}
			ec.Emit(this.unwind_protect ? OpCodes.Leave : OpCodes.Br, label);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000A0E4C File Offset: 0x0009F04C
		private bool IsLeavingFinally(Block labelBlock)
		{
			for (Block block = this.try_finally.Statement as Block; block != null; block = block.Parent)
			{
				if (block == labelBlock)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A0E7D File Offset: 0x0009F07D
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C2C RID: 3116
		private string target;

		// Token: 0x04000C2D RID: 3117
		private LabeledStatement label;

		// Token: 0x04000C2E RID: 3118
		private TryFinally try_finally;
	}
}
