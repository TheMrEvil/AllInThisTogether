using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicates if attributes or elements need to be qualified with a namespace prefix.</summary>
	// Token: 0x020005BD RID: 1469
	public enum XmlSchemaForm
	{
		/// <summary>Element and attribute form is not specified in the schema.</summary>
		// Token: 0x04002B5F RID: 11103
		[XmlIgnore]
		None,
		/// <summary>Elements and attributes must be qualified with a namespace prefix.</summary>
		// Token: 0x04002B60 RID: 11104
		[XmlEnum("qualified")]
		Qualified,
		/// <summary>Elements and attributes are not required to be qualified with a namespace prefix.</summary>
		// Token: 0x04002B61 RID: 11105
		[XmlEnum("unqualified")]
		Unqualified
	}
}
