using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to initialize configuration properties.</summary>
	// Token: 0x0200007E RID: 126
	[ComVisible(false)]
	public interface IConfigurationManagerInternal
	{
		/// <summary>Gets the configuration file name related to the application path.</summary>
		/// <returns>A string value representing a configuration file name.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600042D RID: 1069
		string ApplicationConfigUri { get; }

		/// <summary>Gets the local configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string representing the local configuration directory.</returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600042E RID: 1070
		string ExeLocalConfigDirectory { get; }

		/// <summary>Gets the local configuration path of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the local configuration path of the application.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600042F RID: 1071
		string ExeLocalConfigPath { get; }

		/// <summary>Gets the product name of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product name of the application.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000430 RID: 1072
		string ExeProductName { get; }

		/// <summary>Gets the product version of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product version of the application.</returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000431 RID: 1073
		string ExeProductVersion { get; }

		/// <summary>Gets the roaming configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the roaming configuration directory of the application.</returns>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000432 RID: 1074
		string ExeRoamingConfigDirectory { get; }

		/// <summary>Gets the roaming user's configuration path based on the application's entry assembly.</summary>
		/// <returns>A string value representing the roaming user's configuration path.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000433 RID: 1075
		string ExeRoamingConfigPath { get; }

		/// <summary>Gets the configuration path for the Machine.config file.</summary>
		/// <returns>A string value representing the path of the Machine.config file.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000434 RID: 1076
		string MachineConfigPath { get; }

		/// <summary>Gets a value representing the configuration system's status.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration system is in the process of being initialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000435 RID: 1077
		bool SetConfigurationSystemInProgress { get; }

		/// <summary>Gets a value that specifies whether user configuration settings are supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration system supports user configuration settings; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000436 RID: 1078
		bool SupportsUserConfig { get; }

		/// <summary>Gets the name of the file used to store user configuration settings.</summary>
		/// <returns>A string specifying the name of the file used to store user configuration.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000437 RID: 1079
		string UserConfigFilename { get; }
	}
}
