using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000436 RID: 1078
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ContentMergeIterator
	{
		// Token: 0x06002ADA RID: 10970 RVA: 0x00102535 File Offset: 0x00100735
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.navStack.Reset();
			this.state = ContentMergeIterator.IteratorState.NeedCurrent;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00102550 File Offset: 0x00100750
		public IteratorResult MoveNext(XPathNavigator input)
		{
			return this.MoveNext(input, true);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0010255C File Offset: 0x0010075C
		internal IteratorResult MoveNext(XPathNavigator input, bool isContent)
		{
			switch (this.state)
			{
			case ContentMergeIterator.IteratorState.NeedCurrent:
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				if (isContent ? this.filter.MoveToContent(this.navCurrent) : this.filter.MoveToFollowingSibling(this.navCurrent))
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
				}
				return IteratorResult.NeedInputNode;
			case ContentMergeIterator.IteratorState.HaveCurrentNeedNext:
				if (input == null)
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentNoNext;
					return IteratorResult.HaveCurrentNode;
				}
				this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
				if (isContent ? this.filter.MoveToContent(this.navNext) : this.filter.MoveToFollowingSibling(this.navNext))
				{
					this.state = ContentMergeIterator.IteratorState.HaveCurrentHaveNext;
					return this.DocOrderMerge();
				}
				return IteratorResult.NeedInputNode;
			case ContentMergeIterator.IteratorState.HaveCurrentNoNext:
			case ContentMergeIterator.IteratorState.HaveCurrentHaveNext:
				if (isContent ? (!this.filter.MoveToNextContent(this.navCurrent)) : (!this.filter.MoveToFollowingSibling(this.navCurrent)))
				{
					if (this.navStack.IsEmpty)
					{
						if (this.state == ContentMergeIterator.IteratorState.HaveCurrentNoNext)
						{
							return IteratorResult.NoMoreNodes;
						}
						this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, this.navNext);
						this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
						return IteratorResult.NeedInputNode;
					}
					else
					{
						this.navCurrent = this.navStack.Pop();
					}
				}
				if (this.state == ContentMergeIterator.IteratorState.HaveCurrentNoNext)
				{
					return IteratorResult.HaveCurrentNode;
				}
				return this.DocOrderMerge();
			default:
				return IteratorResult.NoMoreNodes;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x001026B6 File Offset: 0x001008B6
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x001026C0 File Offset: 0x001008C0
		private IteratorResult DocOrderMerge()
		{
			XmlNodeOrder xmlNodeOrder = this.navCurrent.ComparePosition(this.navNext);
			if (xmlNodeOrder == XmlNodeOrder.Before || xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				return IteratorResult.HaveCurrentNode;
			}
			if (xmlNodeOrder == XmlNodeOrder.After)
			{
				this.navStack.Push(this.navCurrent);
				this.navCurrent = this.navNext;
				this.navNext = null;
			}
			this.state = ContentMergeIterator.IteratorState.HaveCurrentNeedNext;
			return IteratorResult.NeedInputNode;
		}

		// Token: 0x040021BB RID: 8635
		private XmlNavigatorFilter filter;

		// Token: 0x040021BC RID: 8636
		private XPathNavigator navCurrent;

		// Token: 0x040021BD RID: 8637
		private XPathNavigator navNext;

		// Token: 0x040021BE RID: 8638
		private XmlNavigatorStack navStack;

		// Token: 0x040021BF RID: 8639
		private ContentMergeIterator.IteratorState state;

		// Token: 0x02000437 RID: 1079
		private enum IteratorState
		{
			// Token: 0x040021C1 RID: 8641
			NeedCurrent,
			// Token: 0x040021C2 RID: 8642
			HaveCurrentNeedNext,
			// Token: 0x040021C3 RID: 8643
			HaveCurrentNoNext,
			// Token: 0x040021C4 RID: 8644
			HaveCurrentHaveNext
		}
	}
}
