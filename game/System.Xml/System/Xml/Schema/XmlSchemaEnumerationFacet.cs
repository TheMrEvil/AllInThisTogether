using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="enumeration" /> facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies a list of valid values for a simpleType element. Declaration is contained within a <see langword="restriction" /> declaration.</summary>
	// Token: 0x020005B5 RID: 1461
	public class XmlSchemaEnumerationFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" /> class.</summary>
		// Token: 0x06003B14 RID: 15124 RVA: 0x0014E017 File Offset: 0x0014C217
		public XmlSchemaEnumerationFacet()
		{
			base.FacetType = FacetType.Enumeration;
		}
	}
}
