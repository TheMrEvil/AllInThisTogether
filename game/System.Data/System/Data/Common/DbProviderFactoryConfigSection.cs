using System;

namespace System.Data.Common
{
	// Token: 0x020003D7 RID: 983
	internal class DbProviderFactoryConfigSection
	{
		// Token: 0x06002F54 RID: 12116 RVA: 0x000CB40C File Offset: 0x000C960C
		public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
		{
			try
			{
				this.factType = FactoryType;
				this.name = FactoryName;
				this.invariantName = this.factType.Namespace.ToString();
				this.description = FactoryDescription;
				this.assemblyQualifiedName = this.factType.AssemblyQualifiedName.ToString();
			}
			catch
			{
				this.factType = null;
				this.name = string.Empty;
				this.invariantName = string.Empty;
				this.description = string.Empty;
				this.assemblyQualifiedName = string.Empty;
			}
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000CB4A8 File Offset: 0x000C96A8
		public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
		{
			this.factType = null;
			this.name = FactoryName;
			this.invariantName = FactoryInvariantName;
			this.description = FactoryDescription;
			this.assemblyQualifiedName = FactoryAssemblyQualifiedName;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000CB4D4 File Offset: 0x000C96D4
		public bool IsNull()
		{
			return this.factType == null && this.invariantName == string.Empty;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x000CB4F9 File Offset: 0x000C96F9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000CB501 File Offset: 0x000C9701
		public string InvariantName
		{
			get
			{
				return this.invariantName;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000CB509 File Offset: 0x000C9709
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000CB511 File Offset: 0x000C9711
		public string AssemblyQualifiedName
		{
			get
			{
				return this.assemblyQualifiedName;
			}
		}

		// Token: 0x04001C86 RID: 7302
		private Type factType;

		// Token: 0x04001C87 RID: 7303
		private string name;

		// Token: 0x04001C88 RID: 7304
		private string invariantName;

		// Token: 0x04001C89 RID: 7305
		private string description;

		// Token: 0x04001C8A RID: 7306
		private string assemblyQualifiedName;
	}
}
