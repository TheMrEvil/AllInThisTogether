using System;

namespace Mono.CSharp
{
	// Token: 0x0200025E RID: 606
	public class UsingNamespace : UsingClause
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x000934E8 File Offset: 0x000916E8
		public UsingNamespace(ATypeNameExpression expr, Location loc) : base(expr, loc)
		{
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000934F4 File Offset: 0x000916F4
		public override void Define(NamespaceContainer ctx)
		{
			base.Define(ctx);
			if (this.resolved is NamespaceExpression)
			{
				return;
			}
			if (this.resolved != null)
			{
				CompilerContext compiler = ctx.Module.Compiler;
				TypeSpec type = this.resolved.Type;
				compiler.Report.SymbolRelatedToPreviousError(type);
				compiler.Report.Error(138, base.Location, "A `using' directive can only be applied to namespaces but `{0}' denotes a type. Consider using a `using static' instead", type.GetSignatureForError());
			}
		}
	}
}
