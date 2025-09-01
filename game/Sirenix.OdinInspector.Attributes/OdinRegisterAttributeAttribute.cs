using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000093 RID: 147
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class OdinRegisterAttributeAttribute : Attribute
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x000045EB File Offset: 0x000027EB
		public OdinRegisterAttributeAttribute(Type attributeType, string category, string description, bool isEnterprise)
		{
			this.AttributeType = attributeType;
			this.Categories = category;
			this.Description = description;
			this.IsEnterprise = isEnterprise;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004610 File Offset: 0x00002810
		public OdinRegisterAttributeAttribute(Type attributeType, string category, string description, bool isEnterprise, string url)
		{
			this.AttributeType = attributeType;
			this.Categories = category;
			this.Description = description;
			this.IsEnterprise = isEnterprise;
			this.DocumentationUrl = url;
		}

		// Token: 0x04000294 RID: 660
		public Type AttributeType;

		// Token: 0x04000295 RID: 661
		public string Categories;

		// Token: 0x04000296 RID: 662
		public string Description;

		// Token: 0x04000297 RID: 663
		public string DocumentationUrl;

		// Token: 0x04000298 RID: 664
		public bool IsEnterprise;
	}
}
