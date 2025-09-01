using System;
using System.Configuration.Provider;
using System.Xml;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents the base class to be extended by custom configuration builder implementations.</summary>
	// Token: 0x0200008A RID: 138
	public abstract class ConfigurationBuilder : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilder" /> class.</summary>
		// Token: 0x0600048D RID: 1165 RVA: 0x00003518 File Offset: 0x00001718
		protected ConfigurationBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Accepts a <see cref="T:System.Configuration.ConfigurationSection" /> object from the configuration system and returns a modified or new <see cref="T:System.Configuration.ConfigurationSection" /> object for further use.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		// Token: 0x0600048E RID: 1166 RVA: 0x00003527 File Offset: 0x00001727
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Accepts an <see cref="T:System.Xml.XmlNode" /> representing the raw configuration section from a config file and returns a modified or new <see cref="T:System.Xml.XmlNode" /> for further use.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x0600048F RID: 1167 RVA: 0x00003527 File Offset: 0x00001727
		public virtual XmlNode ProcessRawXml(XmlNode rawXml)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
