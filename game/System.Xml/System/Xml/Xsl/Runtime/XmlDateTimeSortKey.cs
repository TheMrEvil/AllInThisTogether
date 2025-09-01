using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048D RID: 1165
	internal class XmlDateTimeSortKey : XmlIntegerSortKey
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x00109C65 File Offset: 0x00107E65
		public XmlDateTimeSortKey(DateTime value, XmlCollation collation) : base(value.Ticks, collation)
		{
		}
	}
}
