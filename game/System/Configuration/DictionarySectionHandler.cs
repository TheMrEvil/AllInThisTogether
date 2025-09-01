using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides key/value pair configuration information from a configuration section.</summary>
	// Token: 0x020001AE RID: 430
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>A configuration object.</returns>
		// Token: 0x06000B74 RID: 2932 RVA: 0x000311D5 File Offset: 0x0002F3D5
		public virtual object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetDictionary(parent as IDictionary, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A string value containing the name of the key attribute.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000311EF File Offset: 0x0002F3EF
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A string value containing the name of the value attribute.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000311F6 File Offset: 0x0002F3F6
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DictionarySectionHandler" /> class.</summary>
		// Token: 0x06000B77 RID: 2935 RVA: 0x0000219B File Offset: 0x0000039B
		public DictionarySectionHandler()
		{
		}
	}
}
