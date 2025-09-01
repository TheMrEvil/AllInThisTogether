using System;

namespace System.Xml
{
	/// <summary>Specifies the type of validation to perform.</summary>
	// Token: 0x0200005F RID: 95
	public enum ValidationType
	{
		/// <summary>No validation is performed. This setting creates an XML 1.0 compliant non-validating parser.</summary>
		// Token: 0x040006A4 RID: 1700
		None,
		/// <summary>Validates if DTD or schema information is found.</summary>
		// Token: 0x040006A5 RID: 1701
		[Obsolete("Validation type should be specified as DTD or Schema.")]
		Auto,
		/// <summary>Validates according to the DTD.</summary>
		// Token: 0x040006A6 RID: 1702
		DTD,
		/// <summary>Validate according to XML-Data Reduced (XDR) schemas, including inline XDR schemas. XDR schemas are recognized using the <see langword="x-schema" /> namespace prefix or the <see cref="P:System.Xml.XmlValidatingReader.Schemas" /> property.</summary>
		// Token: 0x040006A7 RID: 1703
		[Obsolete("XDR Validation through XmlValidatingReader is obsoleted")]
		XDR,
		/// <summary>Validate according to XML Schema definition language (XSD) schemas, including inline XML Schemas. XML Schemas are associated with namespace URIs either by using the <see langword="schemaLocation" /> attribute or the provided <see langword="Schemas" /> property.</summary>
		// Token: 0x040006A8 RID: 1704
		Schema
	}
}
