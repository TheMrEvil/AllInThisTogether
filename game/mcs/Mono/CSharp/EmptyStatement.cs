using System;

namespace Mono.CSharp
{
	// Token: 0x0200029A RID: 666
	public sealed class EmptyStatement : Statement
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x0009F6CA File Offset: 0x0009D8CA
		public EmptyStatement(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool Resolve(BlockContext ec)
		{
			return true;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0000225C File Offset: 0x0000045C
		protected override void DoEmit(EmitContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000022F4 File Offset: 0x000004F4
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			return false;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0009F6D9 File Offset: 0x0009D8D9
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
