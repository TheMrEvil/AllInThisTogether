using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002BA RID: 698
	public abstract class ResumableStatement : Statement
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x000A5BB6 File Offset: 0x000A3DB6
		public Label PrepareForEmit(EmitContext ec)
		{
			if (!this.prepared)
			{
				this.prepared = true;
				this.resume_point = ec.DefineLabel();
			}
			return this.resume_point;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000A5BD9 File Offset: 0x000A3DD9
		public virtual Label PrepareForDispose(EmitContext ec, Label end)
		{
			return end;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void EmitForDispose(EmitContext ec, LocalBuilder pc, Label end, bool have_dispatcher)
		{
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000A0706 File Offset: 0x0009E906
		protected ResumableStatement()
		{
		}

		// Token: 0x04000C79 RID: 3193
		private bool prepared;

		// Token: 0x04000C7A RID: 3194
		protected Label resume_point;
	}
}
