using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000358 RID: 856
	internal sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00066DB8 File Offset: 0x00064FB8
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
			Compiler.properties.Add(Compiler.providerOptionsProp);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00031238 File Offset: 0x0002F438
		internal Compiler()
		{
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00066EF6 File Offset: 0x000650F6
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00066F23 File Offset: 0x00065123
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x00066F35 File Offset: 0x00065135
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler.compilerOptionsProp];
			}
			internal set
			{
				base[Compiler.compilerOptionsProp] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00066F43 File Offset: 0x00065143
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00066F55 File Offset: 0x00065155
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler.extensionProp];
			}
			internal set
			{
				base[Compiler.extensionProp] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00066F63 File Offset: 0x00065163
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00066F75 File Offset: 0x00065175
		[ConfigurationProperty("language", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Language
		{
			get
			{
				return (string)base[Compiler.languageProp];
			}
			internal set
			{
				base[Compiler.languageProp] = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00066F83 File Offset: 0x00065183
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x00066F95 File Offset: 0x00065195
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[Compiler.typeProp];
			}
			internal set
			{
				base[Compiler.typeProp] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00066FA3 File Offset: 0x000651A3
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x00066FB5 File Offset: 0x000651B5
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler.warningLevelProp];
			}
			internal set
			{
				base[Compiler.warningLevelProp] = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00066FC8 File Offset: 0x000651C8
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00066FDA File Offset: 0x000651DA
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CompilerProviderOptionsCollection ProviderOptions
		{
			get
			{
				return (CompilerProviderOptionsCollection)base[Compiler.providerOptionsProp];
			}
			internal set
			{
				base[Compiler.providerOptionsProp] = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00066FE8 File Offset: 0x000651E8
		public Dictionary<string, string> ProviderOptionsDictionary
		{
			get
			{
				return this.ProviderOptions.ProviderOptions;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00066FF5 File Offset: 0x000651F5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x04000E78 RID: 3704
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");

		// Token: 0x04000E79 RID: 3705
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "");

		// Token: 0x04000E7A RID: 3706
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000E7B RID: 3707
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000E7C RID: 3708
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x04000E7D RID: 3709
		private static ConfigurationProperty providerOptionsProp = new ConfigurationProperty("", typeof(CompilerProviderOptionsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000E7E RID: 3710
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
