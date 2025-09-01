using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200035E RID: 862
	internal class Sort
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x000DC2CD File Offset: 0x000DA4CD
		public Sort(int sortkey, string xmllang, XmlDataType datatype, XmlSortOrder xmlorder, XmlCaseOrder xmlcaseorder)
		{
			this.select = sortkey;
			this.lang = xmllang;
			this.dataType = datatype;
			this.order = xmlorder;
			this.caseOrder = xmlcaseorder;
		}

		// Token: 0x04001C96 RID: 7318
		internal int select;

		// Token: 0x04001C97 RID: 7319
		internal string lang;

		// Token: 0x04001C98 RID: 7320
		internal XmlDataType dataType;

		// Token: 0x04001C99 RID: 7321
		internal XmlSortOrder order;

		// Token: 0x04001C9A RID: 7322
		internal XmlCaseOrder caseOrder;
	}
}
