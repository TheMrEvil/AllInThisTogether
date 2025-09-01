using System;

namespace Mono.CSharp
{
	// Token: 0x02000199 RID: 409
	internal sealed class DocumentationMemberContext : IMemberContext, IModuleContext
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x0006A3DA File Offset: 0x000685DA
		public DocumentationMemberContext(MemberCore host, MemberName contextName)
		{
			this.host = host;
			this.contextName = contextName;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0006A3F0 File Offset: 0x000685F0
		public TypeSpec CurrentType
		{
			get
			{
				return this.host.CurrentType;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0006A3FD File Offset: 0x000685FD
		public TypeParameters CurrentTypeParameters
		{
			get
			{
				return this.contextName.TypeParameters;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0006A40A File Offset: 0x0006860A
		public MemberCore CurrentMemberDefinition
		{
			get
			{
				return this.host.CurrentMemberDefinition;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsObsolete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0006A417 File Offset: 0x00068617
		public bool IsUnsafe
		{
			get
			{
				return this.host.IsStatic;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0006A417 File Offset: 0x00068617
		public bool IsStatic
		{
			get
			{
				return this.host.IsStatic;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0006A424 File Offset: 0x00068624
		public ModuleContainer Module
		{
			get
			{
				return this.host.Module;
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0006A431 File Offset: 0x00068631
		public string GetSignatureForError()
		{
			return this.host.GetSignatureForError();
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000055E7 File Offset: 0x000037E7
		public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
		{
			return null;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0006A440 File Offset: 0x00068640
		public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			if (arity == 0)
			{
				TypeParameters currentTypeParameters = this.CurrentTypeParameters;
				if (currentTypeParameters != null)
				{
					for (int i = 0; i < currentTypeParameters.Count; i++)
					{
						TypeParameter typeParameter = currentTypeParameters[i];
						if (typeParameter.Name == name)
						{
							typeParameter.Type.DeclaredPosition = i;
							return new TypeParameterExpr(typeParameter, loc);
						}
					}
				}
			}
			return this.host.Parent.LookupNamespaceOrType(name, arity, mode, loc);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public FullNamedExpression LookupNamespaceAlias(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000934 RID: 2356
		private readonly MemberCore host;

		// Token: 0x04000935 RID: 2357
		private MemberName contextName;
	}
}
