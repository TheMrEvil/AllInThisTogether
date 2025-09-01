using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000487 RID: 1159
	internal class XmlEmptySortKey : XmlSortKey
	{
		// Token: 0x06002D63 RID: 11619 RVA: 0x001098DA File Offset: 0x00107ADA
		public XmlEmptySortKey(XmlCollation collation)
		{
			this.isEmptyGreatest = (collation.EmptyGreatest != collation.DescendingOrder);
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x001098F9 File Offset: 0x00107AF9
		public bool IsEmptyGreatest
		{
			get
			{
				return this.isEmptyGreatest;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00109904 File Offset: 0x00107B04
		public override int CompareTo(object obj)
		{
			XmlEmptySortKey xmlEmptySortKey = obj as XmlEmptySortKey;
			if (xmlEmptySortKey == null)
			{
				return -(obj as XmlSortKey).CompareTo(this);
			}
			return base.BreakSortingTie(xmlEmptySortKey);
		}

		// Token: 0x04002328 RID: 9000
		private bool isEmptyGreatest;
	}
}
