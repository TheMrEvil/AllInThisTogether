using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to support a configuration root object.</summary>
	// Token: 0x02000083 RID: 131
	[ComVisible(false)]
	public interface IInternalConfigRoot
	{
		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by a configuration path.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by <paramref name="configPath" />.</returns>
		// Token: 0x06000471 RID: 1137
		IInternalConfigRecord GetConfigRecord(string configPath);

		/// <summary>Returns an <see cref="T:System.Object" /> representing the data in a section of a configuration file.</summary>
		/// <param name="section">A string representing a section of a configuration file.</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the data in a section of a configuration file.</returns>
		// Token: 0x06000472 RID: 1138
		object GetSection(string section, string configPath);

		/// <summary>Returns a value representing the file path of the nearest configuration ancestor that has configuration data.</summary>
		/// <param name="configPath">The path of configuration file.</param>
		/// <returns>A string representing the file path of the nearest configuration ancestor that has configuration data.</returns>
		// Token: 0x06000473 RID: 1139
		string GetUniqueConfigPath(string configPath);

		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for given configuration path.</summary>
		/// <param name="configPath">The path of the configuration file.</param>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for a given configuration path.</returns>
		// Token: 0x06000474 RID: 1140
		IInternalConfigRecord GetUniqueConfigRecord(string configPath);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="host">An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</param>
		/// <param name="isDesignTime">
		///   <see langword="true" /> if design time; <see langword="false" /> if run time.</param>
		// Token: 0x06000475 RID: 1141
		void Init(IInternalConfigHost host, bool isDesignTime);

		/// <summary>Returns a value indicating whether the configuration is a design-time configuration.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is a design-time configuration; <see langword="false" /> if the configuration is not a design-time configuration.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000476 RID: 1142
		bool IsDesignTime { get; }

		/// <summary>Finds and removes a configuration record and all its children for a given configuration path.</summary>
		/// <param name="configPath">The path of the configuration file.</param>
		// Token: 0x06000477 RID: 1143
		void RemoveConfig(string configPath);

		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigChanged" /> event of an <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000478 RID: 1144
		// (remove) Token: 0x06000479 RID: 1145
		event InternalConfigEventHandler ConfigChanged;

		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigRemoved" /> event of a <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600047A RID: 1146
		// (remove) Token: 0x0600047B RID: 1147
		event InternalConfigEventHandler ConfigRemoved;
	}
}
