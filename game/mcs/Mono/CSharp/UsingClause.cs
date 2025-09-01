using System;

namespace Mono.CSharp
{
	// Token: 0x02000260 RID: 608
	public class UsingClause
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x000935BC File Offset: 0x000917BC
		public UsingClause(ATypeNameExpression expr, Location loc)
		{
			this.expr = expr;
			this.loc = loc;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual SimpleMemberName Alias
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x000935D2 File Offset: 0x000917D2
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x000935DA File Offset: 0x000917DA
		public ATypeNameExpression NamespaceExpression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000935E2 File Offset: 0x000917E2
		public FullNamedExpression ResolvedExpression
		{
			get
			{
				return this.resolved;
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000935EA File Offset: 0x000917EA
		public string GetSignatureForError()
		{
			return this.expr.GetSignatureForError();
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000935F7 File Offset: 0x000917F7
		public virtual void Define(NamespaceContainer ctx)
		{
			this.resolved = this.expr.ResolveAsTypeOrNamespace(ctx, false);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0009360C File Offset: 0x0009180C
		public override string ToString()
		{
			return this.resolved.ToString();
		}

		// Token: 0x04000B29 RID: 2857
		private readonly ATypeNameExpression expr;

		// Token: 0x04000B2A RID: 2858
		private readonly Location loc;

		// Token: 0x04000B2B RID: 2859
		protected FullNamedExpression resolved;
	}
}
