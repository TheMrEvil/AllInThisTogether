using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200004C RID: 76
	internal class ExeConfigurationHost : InternalConfigurationHost
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00007F04 File Offset: 0x00006104
		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			this.map = (ExeConfigurationFileMap)hostInitParams[0];
			this.level = (ConfigurationUserLevel)hostInitParams[1];
			ExeConfigurationHost.CheckFileMap(this.level, this.map);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007F34 File Offset: 0x00006134
		private static void CheckFileMap(ConfigurationUserLevel level, ExeConfigurationFileMap map)
		{
			if (level != ConfigurationUserLevel.None)
			{
				if (level != ConfigurationUserLevel.PerUserRoaming)
				{
					if (level != ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						return;
					}
					if (string.IsNullOrEmpty(map.LocalUserConfigFilename))
					{
						throw new ArgumentException("The 'LocalUserConfigFilename' argument cannot be null.");
					}
				}
				if (string.IsNullOrEmpty(map.RoamingUserConfigFilename))
				{
					throw new ArgumentException("The 'RoamingUserConfigFilename' argument cannot be null.");
				}
			}
			if (string.IsNullOrEmpty(map.ExeConfigFilename))
			{
				throw new ArgumentException("The 'ExeConfigFilename' argument cannot be null.");
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007F98 File Offset: 0x00006198
		public override string GetStreamName(string configPath)
		{
			if (configPath == "exe")
			{
				return this.map.ExeConfigFilename;
			}
			if (configPath == "local")
			{
				return this.map.LocalUserConfigFilename;
			}
			if (configPath == "roaming")
			{
				return this.map.RoamingUserConfigFilename;
			}
			if (configPath == "machine")
			{
				return this.map.MachineConfigFilename;
			}
			ConfigurationUserLevel configurationUserLevel = this.level;
			if (configurationUserLevel == ConfigurationUserLevel.None)
			{
				return this.map.ExeConfigFilename;
			}
			if (configurationUserLevel == ConfigurationUserLevel.PerUserRoaming)
			{
				return this.map.RoamingUserConfigFilename;
			}
			if (configurationUserLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
			{
				return this.map.MachineConfigFilename;
			}
			return this.map.LocalUserConfigFilename;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008050 File Offset: 0x00006250
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			this.map = (ExeConfigurationFileMap)hostInitConfigurationParams[0];
			if (hostInitConfigurationParams.Length > 1 && hostInitConfigurationParams[1] is ConfigurationUserLevel)
			{
				this.level = (ConfigurationUserLevel)hostInitConfigurationParams[1];
			}
			ExeConfigurationHost.CheckFileMap(this.level, this.map);
			if (locationSubPath == null)
			{
				ConfigurationUserLevel configurationUserLevel = this.level;
				if (configurationUserLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (configurationUserLevel == ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						if (this.map.LocalUserConfigFilename == null)
						{
							throw new ArgumentException("LocalUserConfigFilename must be set correctly");
						}
						locationSubPath = "local";
					}
				}
				else
				{
					if (this.map.RoamingUserConfigFilename == null)
					{
						throw new ArgumentException("RoamingUserConfigFilename must be set correctly");
					}
					locationSubPath = "roaming";
				}
			}
			if (locationSubPath == "exe" || (locationSubPath == null && this.map.ExeConfigFilename != null))
			{
				configPath = "exe";
				locationSubPath = "machine";
				locationConfigPath = this.map.ExeConfigFilename;
				return;
			}
			if (locationSubPath == "local" && this.map.LocalUserConfigFilename != null)
			{
				configPath = "local";
				locationSubPath = "roaming";
				locationConfigPath = this.map.LocalUserConfigFilename;
				return;
			}
			if (locationSubPath == "roaming" && this.map.RoamingUserConfigFilename != null)
			{
				configPath = "roaming";
				locationSubPath = "exe";
				locationConfigPath = this.map.RoamingUserConfigFilename;
				return;
			}
			if (locationSubPath == "machine" && this.map.MachineConfigFilename != null)
			{
				configPath = "machine";
				locationSubPath = null;
				locationConfigPath = null;
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000081CC File Offset: 0x000063CC
		public ExeConfigurationHost()
		{
		}

		// Token: 0x040000F8 RID: 248
		private ExeConfigurationFileMap map;

		// Token: 0x040000F9 RID: 249
		private ConfigurationUserLevel level;
	}
}
