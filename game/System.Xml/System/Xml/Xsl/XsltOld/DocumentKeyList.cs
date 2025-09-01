using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A7 RID: 935
	internal struct DocumentKeyList
	{
		// Token: 0x06002631 RID: 9777 RVA: 0x000E58C4 File Offset: 0x000E3AC4
		public DocumentKeyList(XPathNavigator rootNav, Hashtable keyTable)
		{
			this.rootNav = rootNav;
			this.keyTable = keyTable;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000E58D4 File Offset: 0x000E3AD4
		public XPathNavigator RootNav
		{
			get
			{
				return this.rootNav;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000E58DC File Offset: 0x000E3ADC
		public Hashtable KeyTable
		{
			get
			{
				return this.keyTable;
			}
		}

		// Token: 0x04001DD9 RID: 7641
		private XPathNavigator rootNav;

		// Token: 0x04001DDA RID: 7642
		private Hashtable keyTable;
	}
}
