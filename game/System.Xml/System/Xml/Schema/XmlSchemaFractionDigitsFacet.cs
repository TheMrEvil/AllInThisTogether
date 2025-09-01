using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies a restriction on the number of digits that can be entered for the fraction value of a simpleType element. The value of fractionDigits must be a positive integer. Represents the World Wide Web Consortium (W3C) <see langword="fractionDigits" /> facet.</summary>
	// Token: 0x020005BB RID: 1467
	public class XmlSchemaFractionDigitsFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" /> class.</summary>
		// Token: 0x06003B1A RID: 15130 RVA: 0x0014E074 File Offset: 0x0014C274
		public XmlSchemaFractionDigitsFacet()
		{
			base.FacetType = FacetType.FractionDigits;
		}
	}
}
