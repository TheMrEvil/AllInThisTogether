using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200004D RID: 77
	internal class MachineConfigurationHost : InternalConfigurationHost
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000081D4 File Offset: 0x000063D4
		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			this.map = (ConfigurationFileMap)hostInitParams[0];
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000081E4 File Offset: 0x000063E4
		public override string GetStreamName(string configPath)
		{
			return this.map.MachineConfigFilename;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000081F1 File Offset: 0x000063F1
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			this.map = (ConfigurationFileMap)hostInitConfigurationParams[0];
			locationSubPath = null;
			configPath = null;
			locationConfigPath = null;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00004919 File Offset: 0x00002B19
		public override bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return true;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000081CC File Offset: 0x000063CC
		public MachineConfigurationHost()
		{
		}

		// Token: 0x040000FA RID: 250
		private ConfigurationFileMap map;
	}
}
