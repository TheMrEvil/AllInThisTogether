using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces that allow the internal .NET Framework infrastructure to customize configuration.</summary>
	// Token: 0x0200007F RID: 127
	[ComVisible(false)]
	public interface IInternalConfigClientHost
	{
		/// <summary>Returns the path to the application configuration file.</summary>
		/// <returns>A string representing the path to the application configuration file.</returns>
		// Token: 0x06000438 RID: 1080
		string GetExeConfigPath();

		/// <summary>Returns a string representing the path to the known local user configuration file.</summary>
		/// <returns>A string representing the path to the known local user configuration file.</returns>
		// Token: 0x06000439 RID: 1081
		string GetLocalUserConfigPath();

		/// <summary>Returns a string representing the path to the known roaming user configuration file.</summary>
		/// <returns>A string representing the path to the known roaming user configuration file.</returns>
		// Token: 0x0600043A RID: 1082
		string GetRoamingUserConfigPath();

		/// <summary>Returns a value indicating whether a configuration file path is the same as a currently known application configuration file path.</summary>
		/// <param name="configPath">A string representing the path to the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to the application configuration file; <see langword="false" /> if a string representing a configuration path is not the same as a path to the application configuration file.</returns>
		// Token: 0x0600043B RID: 1083
		bool IsExeConfig(string configPath);

		/// <summary>Returns a value indicating whether a configuration file path is the same as the configuration file path for the currently known local user.</summary>
		/// <param name="configPath">A string representing the path to the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to a known local user configuration file; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600043C RID: 1084
		bool IsLocalUserConfig(string configPath);

		/// <summary>Returns a value indicating whether a configuration file path is the same as the configuration file path for the currently known roaming user.</summary>
		/// <param name="configPath">A string representing the path to an application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to a known roaming user configuration file; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600043D RID: 1085
		bool IsRoamingUserConfig(string configPath);
	}
}
