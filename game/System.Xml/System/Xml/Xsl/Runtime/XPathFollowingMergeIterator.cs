using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000457 RID: 1111
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathFollowingMergeIterator
	{
		// Token: 0x06002B4F RID: 11087 RVA: 0x00103C0D File Offset: 0x00101E0D
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.state = XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00103C20 File Offset: 0x00101E20
		public IteratorResult MoveNext(XPathNavigator input)
		{
			switch (this.state)
			{
			case XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent:
				break;
			case XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent:
				if (input == null)
				{
					this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext;
					return this.MoveFirst();
				}
				if (!this.navCurrent.IsDescendant(input))
				{
					this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNeedNext;
					goto IL_64;
				}
				break;
			case XPathFollowingMergeIterator.IteratorState.HaveCurrentNeedNext:
				goto IL_64;
			default:
				if (!this.filter.MoveToFollowing(this.navCurrent, null))
				{
					return this.MoveFailed();
				}
				return IteratorResult.HaveCurrentNode;
			}
			if (input == null)
			{
				return IteratorResult.NoMoreNodes;
			}
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent;
			return IteratorResult.NeedInputNode;
			IL_64:
			if (input == null)
			{
				this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext;
				return this.MoveFirst();
			}
			if (this.navCurrent.ComparePosition(input) != XmlNodeOrder.Unknown)
			{
				return IteratorResult.NeedInputNode;
			}
			this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCurrentHaveNext;
			return this.MoveFirst();
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x00103CEF File Offset: 0x00101EEF
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x00103CF8 File Offset: 0x00101EF8
		private IteratorResult MoveFailed()
		{
			if (this.state == XPathFollowingMergeIterator.IteratorState.HaveCurrentNoNext)
			{
				this.state = XPathFollowingMergeIterator.IteratorState.NeedCandidateCurrent;
				return IteratorResult.NoMoreNodes;
			}
			this.state = XPathFollowingMergeIterator.IteratorState.HaveCandidateCurrent;
			XPathNavigator xpathNavigator = this.navCurrent;
			this.navCurrent = this.navNext;
			this.navNext = xpathNavigator;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00103D39 File Offset: 0x00101F39
		private IteratorResult MoveFirst()
		{
			if (!XPathFollowingIterator.MoveFirst(this.filter, this.navCurrent))
			{
				return this.MoveFailed();
			}
			return IteratorResult.HaveCurrentNode;
		}

		// Token: 0x0400225D RID: 8797
		private XmlNavigatorFilter filter;

		// Token: 0x0400225E RID: 8798
		private XPathFollowingMergeIterator.IteratorState state;

		// Token: 0x0400225F RID: 8799
		private XPathNavigator navCurrent;

		// Token: 0x04002260 RID: 8800
		private XPathNavigator navNext;

		// Token: 0x02000458 RID: 1112
		private enum IteratorState
		{
			// Token: 0x04002262 RID: 8802
			NeedCandidateCurrent,
			// Token: 0x04002263 RID: 8803
			HaveCandidateCurrent,
			// Token: 0x04002264 RID: 8804
			HaveCurrentNeedNext,
			// Token: 0x04002265 RID: 8805
			HaveCurrentHaveNext,
			// Token: 0x04002266 RID: 8806
			HaveCurrentNoNext
		}
	}
}
