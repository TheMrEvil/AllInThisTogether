using System;

namespace Mono.CSharp
{
	// Token: 0x02000261 RID: 609
	public class UsingExternAlias : UsingAliasNamespace
	{
		// Token: 0x06001E1B RID: 7707 RVA: 0x00093619 File Offset: 0x00091819
		public UsingExternAlias(SimpleMemberName alias, Location loc) : base(alias, null, loc)
		{
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00093624 File Offset: 0x00091824
		public override void Define(NamespaceContainer ctx)
		{
			RootNamespace rootNamespace = ctx.Module.GetRootNamespace(this.Alias.Value);
			if (rootNamespace == null)
			{
				ctx.Module.Compiler.Report.Error(430, base.Location, "The extern alias `{0}' was not specified in -reference option", this.Alias.Value);
				return;
			}
			this.resolved = new NamespaceExpression(rootNamespace, base.Location);
		}
	}
}
