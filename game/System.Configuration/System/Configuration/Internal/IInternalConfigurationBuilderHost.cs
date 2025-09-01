using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Configuration.Internal
{
	/// <summary>Defines the supplemental interface to <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> for configuration hosts that wish to support the application of <see cref="T:System.Configuration.ConfigurationBuilder" /> objects.</summary>
	// Token: 0x0200008C RID: 140
	[ComVisible(false)]
	public interface IInternalConfigurationBuilderHost
	{
		/// <summary>Processes a <see cref="T:System.Configuration.ConfigurationSection" /> object using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="configSection" />.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		// Token: 0x06000490 RID: 1168
		ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder);

		/// <summary>Processes the markup of a configuration section using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="rawXml" />.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x06000491 RID: 1169
		XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder);
	}
}
