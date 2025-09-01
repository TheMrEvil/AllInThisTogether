using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="pattern" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the value entered for a <see langword="simpleType" /> element.</summary>
	// Token: 0x020005B4 RID: 1460
	public class XmlSchemaPatternFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" /> class.</summary>
		// Token: 0x06003B13 RID: 15123 RVA: 0x0014E008 File Offset: 0x0014C208
		public XmlSchemaPatternFacet()
		{
			base.FacetType = FacetType.Pattern;
		}
	}
}
