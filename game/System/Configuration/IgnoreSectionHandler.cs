using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a legacy section-handler definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	// Token: 0x020001B6 RID: 438
	public class IgnoreSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds the specified configuration object to the section-handler collection.</summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section.</param>
		/// <param name="configContext">The virtual path for which the configuration section handler computes configuration values. Normally this parameter is reserved and is <see langword="null" />.</param>
		/// <param name="section">An <see cref="T:System.Xml.XmlNode" /> that contains the configuration information to be handled. Provides direct access to the XML contents of the configuration section.</param>
		/// <returns>The created configuration handler object.</returns>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IgnoreSectionHandler" /> class.</summary>
		// Token: 0x06000B90 RID: 2960 RVA: 0x0000219B File Offset: 0x0000039B
		public IgnoreSectionHandler()
		{
		}
	}
}
