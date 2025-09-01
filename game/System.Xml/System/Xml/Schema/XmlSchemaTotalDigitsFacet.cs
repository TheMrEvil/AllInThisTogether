using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="totalDigits" /> facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the number of digits that can be entered for the value of a <see langword="simpleType" /> element. That value of <see langword="totalDigits" /> must be a positive integer.</summary>
	// Token: 0x020005BA RID: 1466
	public class XmlSchemaTotalDigitsFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" /> class.</summary>
		// Token: 0x06003B19 RID: 15129 RVA: 0x0014E064 File Offset: 0x0014C264
		public XmlSchemaTotalDigitsFacet()
		{
			base.FacetType = FacetType.TotalDigits;
		}
	}
}
