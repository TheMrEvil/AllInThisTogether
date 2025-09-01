using System;

namespace Mono.CSharp
{
	// Token: 0x02000262 RID: 610
	public class UsingAliasNamespace : UsingNamespace
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x0009368E File Offset: 0x0009188E
		public UsingAliasNamespace(SimpleMemberName alias, ATypeNameExpression expr, Location loc) : base(expr, loc)
		{
			this.alias = alias;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0009369F File Offset: 0x0009189F
		public override SimpleMemberName Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000936A7 File Offset: 0x000918A7
		public override void Define(NamespaceContainer ctx)
		{
			this.resolved = (base.NamespaceExpression.ResolveAsTypeOrNamespace(new UsingAliasNamespace.AliasContext(ctx), false) ?? new TypeExpression(InternalType.ErrorType, base.NamespaceExpression.Location));
		}

		// Token: 0x04000B2C RID: 2860
		private readonly SimpleMemberName alias;

		// Token: 0x020003D4 RID: 980
		public struct AliasContext : IMemberContext, IModuleContext
		{
			// Token: 0x0600277B RID: 10107 RVA: 0x000BC3E9 File Offset: 0x000BA5E9
			public AliasContext(NamespaceContainer ns)
			{
				this.ns = ns;
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x0600277C RID: 10108 RVA: 0x000055E7 File Offset: 0x000037E7
			public TypeSpec CurrentType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x0600277D RID: 10109 RVA: 0x000055E7 File Offset: 0x000037E7
			public TypeParameters CurrentTypeParameters
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x0600277E RID: 10110 RVA: 0x000055E7 File Offset: 0x000037E7
			public MemberCore CurrentMemberDefinition
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x0600277F RID: 10111 RVA: 0x000022F4 File Offset: 0x000004F4
			public bool IsObsolete
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06002780 RID: 10112 RVA: 0x00023DF4 File Offset: 0x00021FF4
			public bool IsUnsafe
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06002781 RID: 10113 RVA: 0x00023DF4 File Offset: 0x00021FF4
			public bool IsStatic
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06002782 RID: 10114 RVA: 0x000BC3F2 File Offset: 0x000BA5F2
			public ModuleContainer Module
			{
				get
				{
					return this.ns.Module;
				}
			}

			// Token: 0x06002783 RID: 10115 RVA: 0x00023DF4 File Offset: 0x00021FF4
			public string GetSignatureForError()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002784 RID: 10116 RVA: 0x000055E7 File Offset: 0x000037E7
			public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
			{
				return null;
			}

			// Token: 0x06002785 RID: 10117 RVA: 0x000BC400 File Offset: 0x000BA600
			public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
			{
				FullNamedExpression fullNamedExpression = this.ns.NS.LookupTypeOrNamespace(this.ns, name, arity, mode, loc);
				if (fullNamedExpression != null)
				{
					return fullNamedExpression;
				}
				fullNamedExpression = this.ns.LookupExternAlias(name);
				if (fullNamedExpression != null || this.ns.MemberName == null)
				{
					return fullNamedExpression;
				}
				Namespace parent = this.ns.NS.Parent;
				MemberName left = this.ns.MemberName.Left;
				while (left != null)
				{
					fullNamedExpression = parent.LookupTypeOrNamespace(this, name, arity, mode, loc);
					if (fullNamedExpression != null)
					{
						return fullNamedExpression;
					}
					left = left.Left;
					parent = parent.Parent;
				}
				if (this.ns.Parent != null)
				{
					return this.ns.Parent.LookupNamespaceOrType(name, arity, mode, loc);
				}
				return null;
			}

			// Token: 0x06002786 RID: 10118 RVA: 0x000BC4C2 File Offset: 0x000BA6C2
			public FullNamedExpression LookupNamespaceAlias(string name)
			{
				return this.ns.LookupNamespaceAlias(name);
			}

			// Token: 0x040010F0 RID: 4336
			private readonly NamespaceContainer ns;
		}
	}
}
