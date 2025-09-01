using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002AC RID: 684
	public class Continue : LocalExitStatement
	{
		// Token: 0x060020DA RID: 8410 RVA: 0x000A13F7 File Offset: 0x0009F5F7
		public Continue(Location l) : base(l)
		{
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000A14AF File Offset: 0x0009F6AF
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000A14B8 File Offset: 0x0009F6B8
		protected override void DoEmit(EmitContext ec)
		{
			Label label = ec.LoopBegin;
			if (ec.TryFinallyUnwind != null)
			{
				AsyncInitializer initializer = (AsyncInitializer)ec.CurrentAnonymousMethod;
				label = TryFinally.EmitRedirectedJump(ec, initializer, label, this.enclosing_loop.Statement as Block);
			}
			ec.Emit(this.unwind_protect ? OpCodes.Leave : OpCodes.Br, label);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000A1514 File Offset: 0x0009F714
		protected override bool DoResolve(BlockContext bc)
		{
			this.enclosing_loop = bc.EnclosingLoop;
			return base.DoResolve(bc);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A1529 File Offset: 0x0009F729
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			if (!rc.IsUnreachable)
			{
				this.enclosing_loop.SetIteratorReachable();
			}
			return Reachability.CreateUnreachable();
		}
	}
}
