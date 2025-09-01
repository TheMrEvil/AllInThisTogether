using System;

namespace System.Xml
{
	/// <summary>Specifies the amount of input or output checking that <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Xml.XmlWriter" /> objects perform.</summary>
	// Token: 0x0200002E RID: 46
	public enum ConformanceLevel
	{
		/// <summary>The <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.Xml.XmlWriter" /> object automatically detects whether document-level or fragment-level checking should be performed, and does the appropriate checking. If you're wrapping another <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.Xml.XmlWriter" /> object, the outer object doesn't do any additional conformance checking. Conformance checking is left up to the underlying object.See the <see cref="P:System.Xml.XmlReaderSettings.ConformanceLevel" /> and <see cref="P:System.Xml.XmlWriterSettings.ConformanceLevel" /> properties for details on how the compliance level is determined.</summary>
		// Token: 0x040005E0 RID: 1504
		Auto,
		/// <summary>The XML data is a well-formed XML fragment, as defined by the W3C.</summary>
		// Token: 0x040005E1 RID: 1505
		Fragment,
		/// <summary>The XML data complies with the rules for a well-formed XML 1.0 document, as defined by the W3C.</summary>
		// Token: 0x040005E2 RID: 1506
		Document
	}
}
