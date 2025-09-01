using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicator of how the attribute is used.</summary>
	// Token: 0x020005E5 RID: 1509
	public enum XmlSchemaUse
	{
		/// <summary>Attribute use not specified.</summary>
		// Token: 0x04002BDD RID: 11229
		[XmlIgnore]
		None,
		/// <summary>Attribute is optional.</summary>
		// Token: 0x04002BDE RID: 11230
		[XmlEnum("optional")]
		Optional,
		/// <summary>Attribute cannot be used.</summary>
		// Token: 0x04002BDF RID: 11231
		[XmlEnum("prohibited")]
		Prohibited,
		/// <summary>Attribute must appear once.</summary>
		// Token: 0x04002BE0 RID: 11232
		[XmlEnum("required")]
		Required
	}
}
