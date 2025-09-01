using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200048B RID: 1163
	internal class XmlStringSortKey : XmlSortKey
	{
		// Token: 0x06002D6C RID: 11628 RVA: 0x00109A52 File Offset: 0x00107C52
		public XmlStringSortKey(SortKey sortKey, bool descendingOrder)
		{
			this.sortKey = sortKey;
			this.descendingOrder = descendingOrder;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x00109A68 File Offset: 0x00107C68
		public XmlStringSortKey(byte[] sortKey, bool descendingOrder)
		{
			this.sortKeyBytes = sortKey;
			this.descendingOrder = descendingOrder;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00109A80 File Offset: 0x00107C80
		public override int CompareTo(object obj)
		{
			XmlStringSortKey xmlStringSortKey = obj as XmlStringSortKey;
			if (xmlStringSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			int num;
			if (this.sortKey != null)
			{
				num = SortKey.Compare(this.sortKey, xmlStringSortKey.sortKey);
			}
			else
			{
				int num2 = (this.sortKeyBytes.Length < xmlStringSortKey.sortKeyBytes.Length) ? this.sortKeyBytes.Length : xmlStringSortKey.sortKeyBytes.Length;
				for (int i = 0; i < num2; i++)
				{
					if (this.sortKeyBytes[i] < xmlStringSortKey.sortKeyBytes[i])
					{
						num = -1;
						goto IL_BC;
					}
					if (this.sortKeyBytes[i] > xmlStringSortKey.sortKeyBytes[i])
					{
						num = 1;
						goto IL_BC;
					}
				}
				if (this.sortKeyBytes.Length < xmlStringSortKey.sortKeyBytes.Length)
				{
					num = -1;
				}
				else if (this.sortKeyBytes.Length > xmlStringSortKey.sortKeyBytes.Length)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			IL_BC:
			if (num == 0)
			{
				return base.BreakSortingTie(xmlStringSortKey);
			}
			if (!this.descendingOrder)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x0400232C RID: 9004
		private SortKey sortKey;

		// Token: 0x0400232D RID: 9005
		private byte[] sortKeyBytes;

		// Token: 0x0400232E RID: 9006
		private bool descendingOrder;
	}
}
