using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the <see langword="&lt;configBuilders&gt;" /> section. This class can't be inherited.</summary>
	// Token: 0x0200008F RID: 143
	public sealed class ConfigurationBuildersSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuildersSection" /> class.</summary>
		// Token: 0x06000495 RID: 1173 RVA: 0x00003518 File Offset: 0x00001718
		public ConfigurationBuildersSection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationBuilderCollection" /> of all <see cref="T:System.Configuration.ConfigurationBuilder" /> objects in all participating configuration files.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> objects in all participating configuration files.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00003527 File Offset: 0x00001727
		public ProviderSettingsCollection Builders
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a <see cref="T:System.Configuration.ConfigurationBuilder" /> object that has the provided configuration builder name.</summary>
		/// <param name="builderName">A configuration builder name or a comma-separated list of names. If <paramref name="builderName" /> is a comma-separated list of <see cref="T:System.Configuration.ConfigurationBuilder" /> names, a special aggregate <see cref="T:System.Configuration.ConfigurationBuilder" /> object that references and applies all named configuration builders is returned.</param>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationBuilder" /> object that has the provided configuration <paramref name="builderName" />.</returns>
		/// <exception cref="T:System.Exception">A configuration provider type can't be instantiated under a partially trusted security policy (<see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> is not present on the target assembly).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">ConfigurationBuilders.IgnoreLoadFailure is disabled by default. If a bin-deployed configuration builder can't be found or instantiated for one of the sections read from the configuration file, a <see cref="T:System.IO.FileNotFoundException" /> is trapped and reported. If you wish to ignore load failures, enable ConfigurationBuilders.IgnoreLoadFailure.</exception>
		/// <exception cref="T:System.TypeLoadException">ConfigurationBuilders.IgnoreLoadFailure is disabled by default. While loading a configuration builder if a <see cref="T:System.TypeLoadException" /> occurs for one of the sections read from the configuration file, a <see cref="T:System.TypeLoadException" /> is trapped and reported. If you wish to ignore load failures, enable ConfigurationBuilders.IgnoreLoadFailure.</exception>
		// Token: 0x06000497 RID: 1175 RVA: 0x00003527 File Offset: 0x00001727
		public ConfigurationBuilder GetBuilderFromName(string builderName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
