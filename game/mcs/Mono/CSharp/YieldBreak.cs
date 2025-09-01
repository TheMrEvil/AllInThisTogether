using System;

namespace Mono.CSharp
{
	// Token: 0x0200022F RID: 559
	public class YieldBreak : ExitStatement
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x000894FF File Offset: 0x000876FF
		public YieldBreak(Location l)
		{
			this.loc = l;
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x000022F4 File Offset: 0x000004F4
		protected override bool IsLocalExit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0000225C File Offset: 0x0000045C
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0008950E File Offset: 0x0008770E
		protected override bool DoResolve(BlockContext bc)
		{
			this.iterator = bc.CurrentIterator;
			return Yield.CheckContext(bc, this.loc);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00089528 File Offset: 0x00087728
		protected override void DoEmit(EmitContext ec)
		{
			this.iterator.EmitYieldBreak(ec, this.unwind_protect);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0000212D File Offset: 0x0000032D
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return true;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0008953C File Offset: 0x0008773C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return Reachability.CreateUnreachable();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0008954B File Offset: 0x0008774B
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A6F RID: 2671
		private Iterator iterator;
	}
}
