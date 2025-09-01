using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides access to a configuration file. This type supports the .NET Framework configuration infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x020001B9 RID: 441
	public class NameValueFileSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <param name="parent">The parent object.</param>
		/// <param name="configContext">The configuration context object.</param>
		/// <param name="section">The section XML node.</param>
		/// <returns>A configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The file specified in the <see langword="file" /> attribute of <paramref name="section" /> exists but cannot be loaded.  
		/// -or-
		///  The <see langword="name" /> attribute of <paramref name="section" /> does not match the root element of the file specified in the <see langword="file" /> attribute.</exception>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x000313E4 File Offset: 0x0002F5E4
		public object Create(object parent, object configContext, XmlNode section)
		{
			XmlNode xmlNode = null;
			if (section.Attributes != null)
			{
				xmlNode = section.Attributes.RemoveNamedItem("file");
			}
			NameValueCollection nameValueCollection = ConfigHelper.GetNameValueCollection(parent as NameValueCollection, section, "key", "value");
			if (xmlNode != null && xmlNode.Value != string.Empty)
			{
				string text = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(((IConfigXmlNode)section).Filename)), xmlNode.Value);
				if (!File.Exists(text))
				{
					return nameValueCollection;
				}
				ConfigXmlDocument configXmlDocument = new ConfigXmlDocument();
				configXmlDocument.Load(text);
				if (configXmlDocument.DocumentElement.Name != section.Name)
				{
					throw new ConfigurationException("Invalid root element", configXmlDocument.DocumentElement);
				}
				nameValueCollection = ConfigHelper.GetNameValueCollection(nameValueCollection, configXmlDocument.DocumentElement, "key", "value");
			}
			return nameValueCollection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NameValueFileSectionHandler" /> class.</summary>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0000219B File Offset: 0x0000039B
		public NameValueFileSectionHandler()
		{
		}
	}
}
