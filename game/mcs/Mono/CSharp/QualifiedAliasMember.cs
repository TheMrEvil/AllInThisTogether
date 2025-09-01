using System;

namespace Mono.CSharp
{
	// Token: 0x020001F8 RID: 504
	public class QualifiedAliasMember : MemberAccess
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x00080361 File Offset: 0x0007E561
		public QualifiedAliasMember(string alias, string identifier, Location l) : base(null, identifier, l)
		{
			this.alias = alias;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00080373 File Offset: 0x0007E573
		public QualifiedAliasMember(string alias, string identifier, TypeArguments targs, Location l) : base(null, identifier, targs, l)
		{
			this.alias = alias;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00080387 File Offset: 0x0007E587
		public QualifiedAliasMember(string alias, string identifier, int arity, Location l) : base(null, identifier, arity, l)
		{
			this.alias = alias;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0008039B File Offset: 0x0007E59B
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000803A4 File Offset: 0x0007E5A4
		public FullNamedExpression CreateExpressionFromAlias(IMemberContext mc)
		{
			if (this.alias == QualifiedAliasMember.GlobalAlias)
			{
				return new NamespaceExpression(mc.Module.GlobalRootNamespace, this.loc);
			}
			int errors = mc.Module.Compiler.Report.Errors;
			FullNamedExpression fullNamedExpression = mc.LookupNamespaceAlias(this.alias);
			if (fullNamedExpression == null)
			{
				if (errors == mc.Module.Compiler.Report.Errors)
				{
					mc.Module.Compiler.Report.Error(432, this.loc, "Alias `{0}' not found", this.alias);
				}
				return null;
			}
			return fullNamedExpression;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00080446 File Offset: 0x0007E646
		public override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments)
		{
			this.expr = this.CreateExpressionFromAlias(mc);
			if (this.expr == null)
			{
				return null;
			}
			return base.ResolveAsTypeOrNamespace(mc, allowUnboundTypeArguments);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00080467 File Offset: 0x0007E667
		protected override Expression DoResolve(ResolveContext rc)
		{
			return this.ResolveAsTypeOrNamespace(rc, false);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00080474 File Offset: 0x0007E674
		public override string GetSignatureForError()
		{
			string str = base.Name;
			if (this.targs != null)
			{
				str = base.Name + "<" + this.targs.GetSignatureForError() + ">";
			}
			return this.alias + "::" + str;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool HasConditionalAccess()
		{
			return false;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000804C2 File Offset: 0x0007E6C2
		public override Expression LookupNameExpression(ResolveContext rc, Expression.MemberLookupRestrictions restrictions)
		{
			if ((restrictions & Expression.MemberLookupRestrictions.InvocableOnly) != Expression.MemberLookupRestrictions.None)
			{
				rc.Module.Compiler.Report.Error(687, this.loc, "The namespace alias qualifier `::' cannot be used to invoke a method. Consider using `.' instead", this.GetSignatureForError());
				return null;
			}
			return this.DoResolve(rc);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000804FD File Offset: 0x0007E6FD
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00080506 File Offset: 0x0007E706
		// Note: this type is marked as 'beforefieldinit'.
		static QualifiedAliasMember()
		{
		}

		// Token: 0x040009E6 RID: 2534
		private readonly string alias;

		// Token: 0x040009E7 RID: 2535
		public static readonly string GlobalAlias = "global";
	}
}
