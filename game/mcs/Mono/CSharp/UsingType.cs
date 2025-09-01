using System;

namespace Mono.CSharp
{
	// Token: 0x0200025F RID: 607
	public class UsingType : UsingClause
	{
		// Token: 0x06001E11 RID: 7697 RVA: 0x000934E8 File Offset: 0x000916E8
		public UsingType(ATypeNameExpression expr, Location loc) : base(expr, loc)
		{
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00093564 File Offset: 0x00091764
		public override void Define(NamespaceContainer ctx)
		{
			base.Define(ctx);
			if (this.resolved == null)
			{
				return;
			}
			NamespaceExpression namespaceExpression = this.resolved as NamespaceExpression;
			if (namespaceExpression != null)
			{
				ctx.Module.Compiler.Report.Error(7007, base.Location, "A 'using static' directive can only be applied to types but `{0}' denotes a namespace. Consider using a `using' directive instead", namespaceExpression.GetSignatureForError());
				return;
			}
		}
	}
}
