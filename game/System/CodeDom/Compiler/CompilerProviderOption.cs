using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035A RID: 858
	internal sealed class CompilerProviderOption : ConfigurationElement
	{
		// Token: 0x06001C78 RID: 7288 RVA: 0x0006732C File Offset: 0x0006552C
		static CompilerProviderOption()
		{
			CompilerProviderOption.properties.Add(CompilerProviderOption.nameProp);
			CompilerProviderOption.properties.Add(CompilerProviderOption.valueProp);
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0006739F File Offset: 0x0006559F
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x000673B1 File Offset: 0x000655B1
		[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[CompilerProviderOption.nameProp];
			}
			set
			{
				base[CompilerProviderOption.nameProp] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000673BF File Offset: 0x000655BF
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x000673D1 File Offset: 0x000655D1
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Value
		{
			get
			{
				return (string)base[CompilerProviderOption.valueProp];
			}
			set
			{
				base[CompilerProviderOption.valueProp] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000673DF File Offset: 0x000655DF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOption.properties;
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00031238 File Offset: 0x0002F438
		public CompilerProviderOption()
		{
		}

		// Token: 0x04000E84 RID: 3716
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000E85 RID: 3717
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000E86 RID: 3718
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
