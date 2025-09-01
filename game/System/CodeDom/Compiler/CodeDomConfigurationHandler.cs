using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000357 RID: 855
	internal sealed class CodeDomConfigurationHandler : ConfigurationSection
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x00066D0C File Offset: 0x00064F0C
		static CodeDomConfigurationHandler()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
			CodeDomConfigurationHandler.properties = new ConfigurationPropertyCollection();
			CodeDomConfigurationHandler.properties.Add(CodeDomConfigurationHandler.compilersProp);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public CodeDomConfigurationHandler()
		{
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00066D5A File Offset: 0x00064F5A
		protected override void InitializeDefault()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00066D7A File Offset: 0x00064F7A
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000075E1 File Offset: 0x000057E1
		protected override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00066D82 File Offset: 0x00064F82
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00066D94 File Offset: 0x00064F94
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				CompilerCollection compilerCollection = (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
				if (compilerCollection == null)
				{
					return null;
				}
				return compilerCollection.CompilerInfos;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00066DB1 File Offset: 0x00064FB1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeDomConfigurationHandler.properties;
			}
		}

		// Token: 0x04000E75 RID: 3701
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000E76 RID: 3702
		private static ConfigurationProperty compilersProp;

		// Token: 0x04000E77 RID: 3703
		private static CompilerCollection default_compilers = new CompilerCollection();
	}
}
