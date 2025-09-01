using System;

namespace Mono.CSharp
{
	// Token: 0x02000226 RID: 550
	public class UnboundTypeArguments : TypeArguments
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x000877F0 File Offset: 0x000859F0
		public UnboundTypeArguments(int arity, Location loc) : base(new FullNamedExpression[arity])
		{
			this.loc = loc;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsEmpty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00087805 File Offset: 0x00085A05
		public override bool Resolve(IMemberContext mc, bool allowUnbound)
		{
			if (!allowUnbound)
			{
				mc.Module.Compiler.Report.Error(7003, this.loc, "Unbound generic name is not valid in this context");
			}
			return true;
		}

		// Token: 0x04000A5D RID: 2653
		private Location loc;
	}
}
