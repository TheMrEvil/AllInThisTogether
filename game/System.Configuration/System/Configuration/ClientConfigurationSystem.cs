using System;
using System.Configuration.Internal;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x0200000E RID: 14
	internal class ClientConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002050 File Offset: 0x00000250
		public ClientConfigurationSystem()
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002344 File Offset: 0x00000544
		private Configuration Configuration
		{
			get
			{
				if (this.cfg == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					try
					{
						this.cfg = ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, entryAssembly, null);
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException("Error Initializing the configuration system.", inner);
					}
				}
				return this.cfg;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002394 File Offset: 0x00000594
		object IInternalConfigSystem.GetSection(string configKey)
		{
			ConfigurationSection section = this.Configuration.GetSection(configKey);
			if (section == null)
			{
				return null;
			}
			return section.GetRuntimeObject();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023B9 File Offset: 0x000005B9
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023BB File Offset: 0x000005BB
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000034 RID: 52
		private Configuration cfg;
	}
}
