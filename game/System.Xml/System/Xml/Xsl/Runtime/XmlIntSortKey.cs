using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048A RID: 1162
	internal class XmlIntSortKey : XmlSortKey
	{
		// Token: 0x06002D6A RID: 11626 RVA: 0x001099EE File Offset: 0x00107BEE
		public XmlIntSortKey(int value, XmlCollation collation)
		{
			this.intVal = (collation.DescendingOrder ? (~value) : value);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x00109A0C File Offset: 0x00107C0C
		public override int CompareTo(object obj)
		{
			XmlIntSortKey xmlIntSortKey = obj as XmlIntSortKey;
			if (xmlIntSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			if (this.intVal == xmlIntSortKey.intVal)
			{
				return base.BreakSortingTie(xmlIntSortKey);
			}
			if (this.intVal >= xmlIntSortKey.intVal)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0400232B RID: 9003
		private int intVal;
	}
}
