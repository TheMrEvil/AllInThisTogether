using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000489 RID: 1161
	internal class XmlIntegerSortKey : XmlSortKey
	{
		// Token: 0x06002D68 RID: 11624 RVA: 0x0010998D File Offset: 0x00107B8D
		public XmlIntegerSortKey(long value, XmlCollation collation)
		{
			this.longVal = (collation.DescendingOrder ? (~value) : value);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x001099A8 File Offset: 0x00107BA8
		public override int CompareTo(object obj)
		{
			XmlIntegerSortKey xmlIntegerSortKey = obj as XmlIntegerSortKey;
			if (xmlIntegerSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			if (this.longVal == xmlIntegerSortKey.longVal)
			{
				return base.BreakSortingTie(xmlIntegerSortKey);
			}
			if (this.longVal >= xmlIntegerSortKey.longVal)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0400232A RID: 9002
		private long longVal;
	}
}
