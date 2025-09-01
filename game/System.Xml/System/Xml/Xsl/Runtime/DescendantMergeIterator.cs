using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000451 RID: 1105
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DescendantMergeIterator
	{
		// Token: 0x06002B3F RID: 11071 RVA: 0x00103933 File Offset: 0x00101B33
		public void Create(XmlNavigatorFilter filter, bool orSelf)
		{
			this.filter = filter;
			this.state = DescendantMergeIterator.IteratorState.NoPrevious;
			this.orSelf = orSelf;
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0010394C File Offset: 0x00101B4C
		public IteratorResult MoveNext(XPathNavigator input)
		{
			if (this.state != DescendantMergeIterator.IteratorState.NeedDescendant)
			{
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				if (this.state != DescendantMergeIterator.IteratorState.NoPrevious && this.navRoot.IsDescendant(input))
				{
					return IteratorResult.NeedInputNode;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				this.navRoot = XmlQueryRuntime.SyncToNavigator(this.navRoot, input);
				this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, input);
				this.navEnd.MoveToNonDescendant();
				this.state = DescendantMergeIterator.IteratorState.NeedDescendant;
				if (this.orSelf && !this.filter.IsFiltered(input))
				{
					return IteratorResult.HaveCurrentNode;
				}
			}
			if (this.filter.MoveToFollowing(this.navCurrent, this.navEnd))
			{
				return IteratorResult.HaveCurrentNode;
			}
			this.state = DescendantMergeIterator.IteratorState.NeedCurrent;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x00103A03 File Offset: 0x00101C03
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002249 RID: 8777
		private XmlNavigatorFilter filter;

		// Token: 0x0400224A RID: 8778
		private XPathNavigator navCurrent;

		// Token: 0x0400224B RID: 8779
		private XPathNavigator navRoot;

		// Token: 0x0400224C RID: 8780
		private XPathNavigator navEnd;

		// Token: 0x0400224D RID: 8781
		private DescendantMergeIterator.IteratorState state;

		// Token: 0x0400224E RID: 8782
		private bool orSelf;

		// Token: 0x02000452 RID: 1106
		private enum IteratorState
		{
			// Token: 0x04002250 RID: 8784
			NoPrevious,
			// Token: 0x04002251 RID: 8785
			NeedCurrent,
			// Token: 0x04002252 RID: 8786
			NeedDescendant
		}
	}
}
