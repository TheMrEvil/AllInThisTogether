using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002A6 RID: 678
	internal class XmlFacetComparer : IComparer
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x00091C4C File Offset: 0x0008FE4C
		public int Compare(object o1, object o2)
		{
			XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)o1;
			XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)o2;
			return string.Compare(xmlSchemaFacet.GetType().Name + ":" + xmlSchemaFacet.Value, xmlSchemaFacet2.GetType().Name + ":" + xmlSchemaFacet2.Value, StringComparison.Ordinal);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlFacetComparer()
		{
		}
	}
}
