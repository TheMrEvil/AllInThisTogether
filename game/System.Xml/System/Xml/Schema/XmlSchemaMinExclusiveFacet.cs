using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="minExclusive" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a <see langword="simpleType" /> element. The element value must be greater than the value of the <see langword="minExclusive" /> element.</summary>
	// Token: 0x020005B6 RID: 1462
	public class XmlSchemaMinExclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" /> class.</summary>
		// Token: 0x06003B15 RID: 15125 RVA: 0x0014E026 File Offset: 0x0014C226
		public XmlSchemaMinExclusiveFacet()
		{
			base.FacetType = FacetType.MinExclusive;
		}
	}
}
