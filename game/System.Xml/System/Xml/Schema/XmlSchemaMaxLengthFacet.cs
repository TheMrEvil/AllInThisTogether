using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="maxLength" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the maximum length of the data value of a <see langword="simpleType" /> element. The length must be less than the value of the <see langword="maxLength" /> element.</summary>
	// Token: 0x020005B3 RID: 1459
	public class XmlSchemaMaxLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" /> class.</summary>
		// Token: 0x06003B12 RID: 15122 RVA: 0x0014DFF9 File Offset: 0x0014C1F9
		public XmlSchemaMaxLengthFacet()
		{
			base.FacetType = FacetType.MaxLength;
		}
	}
}
