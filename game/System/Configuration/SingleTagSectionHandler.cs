using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles configuration sections that are represented by a single XML tag in the .config file.</summary>
	// Token: 0x020001D9 RID: 473
	public class SingleTagSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Used internally to create a new instance of this object.</summary>
		/// <param name="parent">The parent of this object.</param>
		/// <param name="context">The context of this object.</param>
		/// <param name="section">The <see cref="T:System.Xml.XmlNode" /> object in the configuration.</param>
		/// <returns>The created object handler.</returns>
		// Token: 0x06000C58 RID: 3160 RVA: 0x00032770 File Offset: 0x00030970
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable();
			}
			else
			{
				hashtable = (Hashtable)parent;
			}
			if (section.HasChildNodes)
			{
				throw new ConfigurationException("Child Nodes not allowed.");
			}
			XmlAttributeCollection attributes = section.Attributes;
			for (int i = 0; i < attributes.Count; i++)
			{
				hashtable.Add(attributes[i].Name, attributes[i].Value);
			}
			return hashtable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SingleTagSectionHandler" /> class.</summary>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0000219B File Offset: 0x0000039B
		public SingleTagSectionHandler()
		{
		}
	}
}
