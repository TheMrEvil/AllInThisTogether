using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017F RID: 383
	internal class ImportedMethodDefinition : ImportedParameterMemberDefinition, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x0004C798 File Offset: 0x0004A998
		public ImportedMethodDefinition(MethodBase provider, TypeSpec type, AParametersCollection parameters, MetadataImporter importer) : base(provider, type, parameters, importer)
		{
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0004C7A5 File Offset: 0x0004A9A5
		MethodBase IMethodDefinition.Metadata
		{
			get
			{
				return (MethodBase)this.provider;
			}
		}
	}
}
