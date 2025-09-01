using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) <see langword="whiteSpace" /> facet.</summary>
	// Token: 0x020005BC RID: 1468
	public class XmlSchemaWhiteSpaceFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" /> class.</summary>
		// Token: 0x06003B1B RID: 15131 RVA: 0x0014E084 File Offset: 0x0014C284
		public XmlSchemaWhiteSpaceFacet()
		{
			base.FacetType = FacetType.Whitespace;
		}
	}
}
