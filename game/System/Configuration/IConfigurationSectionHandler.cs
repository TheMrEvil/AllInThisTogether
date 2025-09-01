using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles the access to certain configuration sections.</summary>
	// Token: 0x020001B1 RID: 433
	public interface IConfigurationSectionHandler
	{
		/// <summary>Creates a configuration section handler.</summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>The created section handler object.</returns>
		// Token: 0x06000B7D RID: 2941
		object Create(object parent, object configContext, XmlNode section);
	}
}
