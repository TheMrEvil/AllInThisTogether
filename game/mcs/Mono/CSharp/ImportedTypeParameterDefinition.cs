using System;

namespace Mono.CSharp
{
	// Token: 0x02000182 RID: 386
	internal class ImportedTypeParameterDefinition : ImportedDefinition, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0004C7D9 File Offset: 0x0004A9D9
		public ImportedTypeParameterDefinition(Type type, MetadataImporter importer) : base(type, importer)
		{
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public IAssemblyDefinition DeclaringAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000055E7 File Offset: 0x000037E7
		public string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000022F4 File Offset: 0x000004F4
		public int TypeParametersCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x000055E7 File Offset: 0x000037E7
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000055E7 File Offset: 0x000037E7
		public TypeSpec GetAttributeCoClass()
		{
			return null;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0000225C File Offset: 0x0000045C
		public string GetAttributeDefaultMember()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0000225C File Offset: 0x0000045C
		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00023DF4 File Offset: 0x00021FF4
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			throw new NotImplementedException();
		}
	}
}
