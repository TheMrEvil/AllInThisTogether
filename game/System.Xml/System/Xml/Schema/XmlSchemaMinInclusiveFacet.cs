using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="minInclusive" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than or equal to the value of the <see langword="minInclusive" /> element.</summary>
	// Token: 0x020005B7 RID: 1463
	public class XmlSchemaMinInclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" /> class.</summary>
		// Token: 0x06003B16 RID: 15126 RVA: 0x0014E035 File Offset: 0x0014C235
		public XmlSchemaMinInclusiveFacet()
		{
			base.FacetType = FacetType.MinInclusive;
		}
	}
}
