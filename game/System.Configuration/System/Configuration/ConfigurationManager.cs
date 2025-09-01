using System;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Text;
using Unity;

namespace System.Configuration
{
	/// <summary>Provides access to configuration files for client applications. This class cannot be inherited.</summary>
	// Token: 0x02000026 RID: 38
	public static class ConfigurationManager
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005DC8 File Offset: 0x00003FC8
		[MonoTODO("Evidence and version still needs work")]
		private static string GetAssemblyInfo(Assembly a)
		{
			object[] customAttributes = a.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			string arg;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				arg = ((AssemblyProductAttribute)customAttributes[0]).Product;
			}
			else
			{
				arg = AppDomain.CurrentDomain.FriendlyName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("evidencehere");
			string arg2 = stringBuilder.ToString();
			customAttributes = a.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
			string path;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				path = ((AssemblyVersionAttribute)customAttributes[0]).Version;
			}
			else
			{
				path = "1.0.0.0";
			}
			return Path.Combine(string.Format("{0}_{1}", arg, arg2), path);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005E64 File Offset: 0x00004064
		internal static Configuration OpenExeConfigurationInternal(ConfigurationUserLevel userLevel, Assembly calling_assembly, string exePath)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			if (userLevel != ConfigurationUserLevel.None)
			{
				if (userLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						goto IL_EA;
					}
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(exeConfigurationFileMap.LocalUserConfigFilename, "user.config");
				}
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(exeConfigurationFileMap.RoamingUserConfigFilename, "user.config");
			}
			if (exePath == null || exePath.Length == 0)
			{
				exeConfigurationFileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			else
			{
				if (!Path.IsPathRooted(exePath))
				{
					exePath = Path.GetFullPath(exePath);
				}
				if (!File.Exists(exePath))
				{
					Exception inner = new ArgumentException("The specified path does not exist.", "exePath");
					throw new ConfigurationErrorsException("Error Initializing the configuration system:", inner);
				}
				exeConfigurationFileMap.ExeConfigFilename = exePath + ".config";
			}
			IL_EA:
			return ConfigurationManager.ConfigurationFactory.Create(typeof(ExeConfigurationHost), new object[]
			{
				exeConfigurationFileMap,
				userLevel
			});
		}

		/// <summary>Opens the configuration file for the current application as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> for which you are opening the configuration.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x0600014C RID: 332 RVA: 0x00005F82 File Offset: 0x00004182
		public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.OpenExeConfigurationInternal(userLevel, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), null);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <param name="exePath">The path of the executable (exe) file.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x0600014D RID: 333 RVA: 0x00005F99 File Offset: 0x00004199
		public static Configuration OpenExeConfiguration(string exePath)
		{
			return ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), exePath);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping and user level.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> object for which you are opening the configuration.</param>
		/// <returns>The configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x0600014E RID: 334 RVA: 0x00005FB0 File Offset: 0x000041B0
		[MonoLimitation("ConfigurationUserLevel parameter is not supported.")]
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.ConfigurationFactory.Create(typeof(ExeConfigurationHost), new object[]
			{
				fileMap,
				userLevel
			});
		}

		/// <summary>Opens the machine configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x0600014F RID: 335 RVA: 0x00005FDC File Offset: 0x000041DC
		public static Configuration OpenMachineConfiguration()
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap();
			return ConfigurationManager.ConfigurationFactory.Create(typeof(MachineConfigurationHost), new object[]
			{
				configurationFileMap
			});
		}

		/// <summary>Opens the machine configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x06000150 RID: 336 RVA: 0x0000600D File Offset: 0x0000420D
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return ConfigurationManager.ConfigurationFactory.Create(typeof(MachineConfigurationHost), new object[]
			{
				fileMap
			});
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000602D File Offset: 0x0000422D
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			get
			{
				return ConfigurationManager.configFactory;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006034 File Offset: 0x00004234
		internal static IInternalConfigSystem ConfigurationSystem
		{
			get
			{
				return ConfigurationManager.configSystem;
			}
		}

		/// <summary>Retrieves a specified configuration section for the current application's default configuration.</summary>
		/// <param name="sectionName">The configuration section path and name. Node names are separated by forward slashes, for example "system.net/mailSettings/smtp".</param>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object, or <see langword="null" /> if the section does not exist.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x06000153 RID: 339 RVA: 0x0000603C File Offset: 0x0000423C
		public static object GetSection(string sectionName)
		{
			object section = ConfigurationManager.ConfigurationSystem.GetSection(sectionName);
			if (section is ConfigurationSection)
			{
				return ((ConfigurationSection)section).GetRuntimeObject();
			}
			return section;
		}

		/// <summary>Refreshes the named section so the next time that it is retrieved it will be re-read from disk.</summary>
		/// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh.</param>
		// Token: 0x06000154 RID: 340 RVA: 0x0000606A File Offset: 0x0000426A
		public static void RefreshSection(string sectionName)
		{
			ConfigurationManager.ConfigurationSystem.RefreshConfig(sectionName);
		}

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> data for the current application's default configuration.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the contents of the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current application's default configuration.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object with the application settings data.</exception>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006077 File Offset: 0x00004277
		public static NameValueCollection AppSettings
		{
			get
			{
				return (NameValueCollection)ConfigurationManager.GetSection("appSettings");
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConnectionStringsSection" /> data for the current application's default configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object that contains the contents of the <see cref="T:System.Configuration.ConnectionStringsSection" /> object for the current application's default configuration.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</exception>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006088 File Offset: 0x00004288
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ((ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings")).ConnectionStrings;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000060A0 File Offset: 0x000042A0
		internal static IInternalConfigSystem ChangeConfigurationSystem(IInternalConfigSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationManager.lockobj;
			IInternalConfigSystem result;
			lock (obj)
			{
				IInternalConfigSystem internalConfigSystem = ConfigurationManager.configSystem;
				ConfigurationManager.configSystem = newSystem;
				result = internalConfigSystem;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000060F4 File Offset: 0x000042F4
		// Note: this type is marked as 'beforefieldinit'.
		static ConfigurationManager()
		{
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping, user level, and preload option.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references the configuration file to use instead of the default application configuration file.</param>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> object for which you are opening the configuration.</param>
		/// <param name="preLoad">
		///   <see langword="true" /> to preload all section groups and sections; otherwise, <see langword="false" />.</param>
		/// <returns>The configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x06000159 RID: 345 RVA: 0x00003527 File Offset: 0x00001727
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0400009A RID: 154
		private static InternalConfigurationFactory configFactory = new InternalConfigurationFactory();

		// Token: 0x0400009B RID: 155
		private static IInternalConfigSystem configSystem = new ClientConfigurationSystem();

		// Token: 0x0400009C RID: 156
		private static object lockobj = new object();
	}
}
