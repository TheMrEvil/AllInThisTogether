using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017B RID: 379
	public class ImportedModuleDefinition
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x0004C3E8 File Offset: 0x0004A5E8
		public ImportedModuleDefinition(Module module)
		{
			this.module = module;
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0004C3F7 File Offset: 0x0004A5F7
		public bool IsCLSCompliant
		{
			get
			{
				return this.cls_compliant;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004C3FF File Offset: 0x0004A5FF
		public string Name
		{
			get
			{
				return this.module.Name;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0004C40C File Offset: 0x0004A60C
		public void ReadAttributes()
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(this.module))
			{
				Type declaringType = customAttributeData.Constructor.DeclaringType;
				if (declaringType.Name == "CLSCompliantAttribute" && !(declaringType.Namespace != "System"))
				{
					this.cls_compliant = (bool)customAttributeData.ConstructorArguments[0].Value;
				}
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004C4A8 File Offset: 0x0004A6A8
		public List<Attribute> ReadAssemblyAttributes()
		{
			Type type = this.module.GetType(AssemblyAttributesPlaceholder.GetGeneratedName(this.Name));
			if (type == null)
			{
				return null;
			}
			type.GetField(AssemblyAttributesPlaceholder.AssemblyFieldName, BindingFlags.Static | BindingFlags.NonPublic);
			return null;
		}

		// Token: 0x040007B2 RID: 1970
		private readonly Module module;

		// Token: 0x040007B3 RID: 1971
		private bool cls_compliant;
	}
}
