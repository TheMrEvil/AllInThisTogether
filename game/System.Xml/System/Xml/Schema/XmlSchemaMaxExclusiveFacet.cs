using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="maxExclusive" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the maximum value of a <see langword="simpleType" /> element. The element value must be less than the value of the <see langword="maxExclusive" /> element.</summary>
	// Token: 0x020005B8 RID: 1464
	public class XmlSchemaMaxExclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMaxExclusiveFacet" /> class.</summary>
		// Token: 0x06003B17 RID: 15127 RVA: 0x0014E044 File Offset: 0x0014C244
		public XmlSchemaMaxExclusiveFacet()
		{
			base.FacetType = FacetType.MaxExclusive;
		}
	}
}
