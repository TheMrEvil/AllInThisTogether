using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000180 RID: 384
	internal class ImportedGenericMethodDefinition : ImportedMethodDefinition, IGenericMethodDefinition, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x0004C7B2 File Offset: 0x0004A9B2
		public ImportedGenericMethodDefinition(MethodInfo provider, TypeSpec type, AParametersCollection parameters, TypeParameterSpec[] tparams, MetadataImporter importer) : base(provider, type, parameters, importer)
		{
			this.tparams = tparams;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0004C7C7 File Offset: 0x0004A9C7
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return this.tparams;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004C7CF File Offset: 0x0004A9CF
		public int TypeParametersCount
		{
			get
			{
				return this.tparams.Length;
			}
		}

		// Token: 0x040007BB RID: 1979
		private readonly TypeParameterSpec[] tparams;
	}
}
