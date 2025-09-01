using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides information about the validation mode of <see langword="any" /> and <see langword="anyAttribute" /> element replacements.</summary>
	// Token: 0x020005A6 RID: 1446
	public enum XmlSchemaContentProcessing
	{
		/// <summary>Document items are not validated.</summary>
		// Token: 0x04002B15 RID: 11029
		[XmlIgnore]
		None,
		/// <summary>Document items must consist of well-formed XML and are not validated by the schema.</summary>
		// Token: 0x04002B16 RID: 11030
		[XmlEnum("skip")]
		Skip,
		/// <summary>If the associated schema is found, the document items will be validated. No errors will be thrown otherwise.</summary>
		// Token: 0x04002B17 RID: 11031
		[XmlEnum("lax")]
		Lax,
		/// <summary>The schema processor must find a schema associated with the indicated namespace to validate the document items.</summary>
		// Token: 0x04002B18 RID: 11032
		[XmlEnum("strict")]
		Strict
	}
}
