using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002AB RID: 683
	public class Break : LocalExitStatement
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x000A13F7 File Offset: 0x0009F5F7
		public Break(Location l) : base(l)
		{
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000A1400 File Offset: 0x0009F600
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000A140C File Offset: 0x0009F60C
		protected override void DoEmit(EmitContext ec)
		{
			Label label = ec.LoopEnd;
			if (ec.TryFinallyUnwind != null)
			{
				AsyncInitializer initializer = (AsyncInitializer)ec.CurrentAnonymousMethod;
				label = TryFinally.EmitRedirectedJump(ec, initializer, label, this.enclosing_loop.Statement as Block);
			}
			ec.Emit(this.unwind_protect ? OpCodes.Leave : OpCodes.Br, label);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000A1468 File Offset: 0x0009F668
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.enclosing_loop.AddEndDefiniteAssignment(fc);
			return true;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000A1477 File Offset: 0x0009F677
		protected override bool DoResolve(BlockContext bc)
		{
			this.enclosing_loop = bc.EnclosingLoopOrSwitch;
			return base.DoResolve(bc);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000A148C File Offset: 0x0009F68C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			if (!rc.IsUnreachable)
			{
				this.enclosing_loop.SetEndReachable();
			}
			return Reachability.CreateUnreachable();
		}
	}
}
