using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000488 RID: 1160
	internal class XmlDecimalSortKey : XmlSortKey
	{
		// Token: 0x06002D66 RID: 11622 RVA: 0x00109930 File Offset: 0x00107B30
		public XmlDecimalSortKey(decimal value, XmlCollation collation)
		{
			this.decVal = (collation.DescendingOrder ? (-value) : value);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00109950 File Offset: 0x00107B50
		public override int CompareTo(object obj)
		{
			XmlDecimalSortKey xmlDecimalSortKey = obj as XmlDecimalSortKey;
			if (xmlDecimalSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			int num = decimal.Compare(this.decVal, xmlDecimalSortKey.decVal);
			if (num == 0)
			{
				return base.BreakSortingTie(xmlDecimalSortKey);
			}
			return num;
		}

		// Token: 0x04002329 RID: 9001
		private decimal decVal;
	}
}
