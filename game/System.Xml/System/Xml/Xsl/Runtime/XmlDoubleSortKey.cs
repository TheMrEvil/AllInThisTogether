using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048C RID: 1164
	internal class XmlDoubleSortKey : XmlSortKey
	{
		// Token: 0x06002D6F RID: 11631 RVA: 0x00109B60 File Offset: 0x00107D60
		public XmlDoubleSortKey(double value, XmlCollation collation)
		{
			if (double.IsNaN(value))
			{
				this.isNaN = true;
				this.dblVal = ((collation.EmptyGreatest != collation.DescendingOrder) ? double.PositiveInfinity : double.NegativeInfinity);
				return;
			}
			this.dblVal = (collation.DescendingOrder ? (-value) : value);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00109BC0 File Offset: 0x00107DC0
		public override int CompareTo(object obj)
		{
			XmlDoubleSortKey xmlDoubleSortKey = obj as XmlDoubleSortKey;
			if (xmlDoubleSortKey == null)
			{
				if (this.isNaN)
				{
					return base.BreakSortingTie(obj as XmlSortKey);
				}
				return base.CompareToEmpty(obj);
			}
			else if (this.dblVal == xmlDoubleSortKey.dblVal)
			{
				if (this.isNaN)
				{
					if (xmlDoubleSortKey.isNaN)
					{
						return base.BreakSortingTie(xmlDoubleSortKey);
					}
					if (this.dblVal != double.NegativeInfinity)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					if (!xmlDoubleSortKey.isNaN)
					{
						return base.BreakSortingTie(xmlDoubleSortKey);
					}
					if (xmlDoubleSortKey.dblVal != double.NegativeInfinity)
					{
						return -1;
					}
					return 1;
				}
			}
			else
			{
				if (this.dblVal >= xmlDoubleSortKey.dblVal)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x0400232F RID: 9007
		private double dblVal;

		// Token: 0x04002330 RID: 9008
		private bool isNaN;
	}
}
