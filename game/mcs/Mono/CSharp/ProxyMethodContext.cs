using System;

namespace Mono.CSharp
{
	// Token: 0x02000273 RID: 627
	internal struct ProxyMethodContext : IMemberContext, IModuleContext
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x00097468 File Offset: 0x00095668
		public ProxyMethodContext(TypeContainer container)
		{
			this.container = container;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public TypeSpec CurrentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public TypeParameters CurrentTypeParameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public MemberCore CurrentMemberDefinition
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsObsolete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public bool IsUnsafe
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00097471 File Offset: 0x00095671
		public ModuleContainer Module
		{
			get
			{
				return this.container.Module;
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public string GetSignatureForError()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public FullNamedExpression LookupNamespaceAlias(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B59 RID: 2905
		private readonly TypeContainer container;
	}
}
