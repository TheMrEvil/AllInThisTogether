using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="length" /> facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the length of a <see langword="simpleType" /> element on the data type.</summary>
	// Token: 0x020005B1 RID: 1457
	public class XmlSchemaLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" /> class.</summary>
		// Token: 0x06003B10 RID: 15120 RVA: 0x0014DFDB File Offset: 0x0014C1DB
		public XmlSchemaLengthFacet()
		{
			base.FacetType = FacetType.Length;
		}
	}
}
