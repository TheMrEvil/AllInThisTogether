using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200045E RID: 1118
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NodeRangeIterator
	{
		// Token: 0x06002B62 RID: 11106 RVA: 0x001040B4 File Offset: 0x001022B4
		public void Create(XPathNavigator start, XmlNavigatorFilter filter, XPathNavigator end)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, start);
			this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, end);
			this.filter = filter;
			if (start.IsSamePosition(end))
			{
				this.state = ((!filter.IsFiltered(start)) ? NodeRangeIterator.IteratorState.HaveCurrentNoNext : NodeRangeIterator.IteratorState.NoNext);
				return;
			}
			this.state = ((!filter.IsFiltered(start)) ? NodeRangeIterator.IteratorState.HaveCurrent : NodeRangeIterator.IteratorState.NeedCurrent);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0010411C File Offset: 0x0010231C
		public bool MoveNext()
		{
			switch (this.state)
			{
			case NodeRangeIterator.IteratorState.HaveCurrent:
				this.state = NodeRangeIterator.IteratorState.NeedCurrent;
				return true;
			case NodeRangeIterator.IteratorState.NeedCurrent:
				if (!this.filter.MoveToFollowing(this.navCurrent, this.navEnd))
				{
					if (this.filter.IsFiltered(this.navEnd))
					{
						this.state = NodeRangeIterator.IteratorState.NoNext;
						return false;
					}
					this.navCurrent.MoveTo(this.navEnd);
					this.state = NodeRangeIterator.IteratorState.NoNext;
				}
				return true;
			case NodeRangeIterator.IteratorState.HaveCurrentNoNext:
				this.state = NodeRangeIterator.IteratorState.NoNext;
				return true;
			default:
				return false;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x001041A7 File Offset: 0x001023A7
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002278 RID: 8824
		private XmlNavigatorFilter filter;

		// Token: 0x04002279 RID: 8825
		private XPathNavigator navCurrent;

		// Token: 0x0400227A RID: 8826
		private XPathNavigator navEnd;

		// Token: 0x0400227B RID: 8827
		private NodeRangeIterator.IteratorState state;

		// Token: 0x0200045F RID: 1119
		private enum IteratorState
		{
			// Token: 0x0400227D RID: 8829
			HaveCurrent,
			// Token: 0x0400227E RID: 8830
			NeedCurrent,
			// Token: 0x0400227F RID: 8831
			HaveCurrentNoNext,
			// Token: 0x04002280 RID: 8832
			NoNext
		}
	}
}
