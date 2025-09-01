using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017D RID: 381
	internal class ImportedMemberDefinition : ImportedDefinition
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x0004C764 File Offset: 0x0004A964
		public ImportedMemberDefinition(MemberInfo member, TypeSpec type, MetadataImporter importer) : base(member, importer)
		{
			this.type = type;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004C775 File Offset: 0x0004A975
		public TypeSpec MemberType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040007B9 RID: 1977
		private readonly TypeSpec type;
	}
}
