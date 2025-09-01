using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000486 RID: 1158
	internal abstract class XmlSortKey : IComparable
	{
		// Token: 0x1700087C RID: 2172
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x00109858 File Offset: 0x00107A58
		public int Priority
		{
			set
			{
				for (XmlSortKey xmlSortKey = this; xmlSortKey != null; xmlSortKey = xmlSortKey.nextKey)
				{
					xmlSortKey.priority = value;
				}
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x0010987A File Offset: 0x00107A7A
		public XmlSortKey AddSortKey(XmlSortKey sortKey)
		{
			if (this.nextKey != null)
			{
				this.nextKey.AddSortKey(sortKey);
			}
			else
			{
				this.nextKey = sortKey;
			}
			return this;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0010989B File Offset: 0x00107A9B
		protected int BreakSortingTie(XmlSortKey that)
		{
			if (this.nextKey != null)
			{
				return this.nextKey.CompareTo(that.nextKey);
			}
			if (this.priority >= that.priority)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x001098C8 File Offset: 0x00107AC8
		protected int CompareToEmpty(object obj)
		{
			if (!(obj as XmlEmptySortKey).IsEmptyGreatest)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06002D61 RID: 11617
		public abstract int CompareTo(object that);

		// Token: 0x06002D62 RID: 11618 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlSortKey()
		{
		}

		// Token: 0x04002326 RID: 8998
		private int priority;

		// Token: 0x04002327 RID: 8999
		private XmlSortKey nextKey;
	}
}
