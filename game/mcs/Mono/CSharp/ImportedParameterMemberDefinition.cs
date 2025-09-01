using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017E RID: 382
	internal class ImportedParameterMemberDefinition : ImportedMemberDefinition, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x0004C77D File Offset: 0x0004A97D
		protected ImportedParameterMemberDefinition(MethodBase provider, TypeSpec type, AParametersCollection parameters, MetadataImporter importer) : base(provider, type, importer)
		{
			this.parameters = parameters;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004C77D File Offset: 0x0004A97D
		public ImportedParameterMemberDefinition(PropertyInfo provider, TypeSpec type, AParametersCollection parameters, MetadataImporter importer) : base(provider, type, importer)
		{
			this.parameters = parameters;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004C790 File Offset: 0x0004A990
		public AParametersCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x040007BA RID: 1978
		private readonly AParametersCollection parameters;
	}
}
