using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to initialize application configuration properties.</summary>
	// Token: 0x02000085 RID: 133
	[ComVisible(false)]
	public interface IInternalConfigSystem
	{
		/// <summary>Returns the configuration object based on the specified key.</summary>
		/// <param name="configKey">The configuration key value.</param>
		/// <returns>A configuration object.</returns>
		// Token: 0x0600047E RID: 1150
		object GetSection(string configKey);

		/// <summary>Refreshes the configuration system based on the specified section name.</summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		// Token: 0x0600047F RID: 1151
		void RefreshConfig(string sectionName);

		/// <summary>Gets a value indicating whether the user configuration is supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the user configuration is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000480 RID: 1152
		bool SupportsUserConfig { get; }
	}
}
