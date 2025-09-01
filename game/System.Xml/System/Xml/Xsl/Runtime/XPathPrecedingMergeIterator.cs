using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200045C RID: 1116
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingMergeIterator
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x00103F4F File Offset: 0x0010214F
		public void Create(XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.state = XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00103F60 File Offset: 0x00102160
		public IteratorResult MoveNext(XPathNavigator input)
		{
			XPathPrecedingMergeIterator.IteratorState iteratorState = this.state;
			if (iteratorState != XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent)
			{
				if (iteratorState == XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent)
				{
					if (input == null)
					{
						this.state = XPathPrecedingMergeIterator.IteratorState.HaveCurrentNoNext;
					}
					else
					{
						if (this.navCurrent.ComparePosition(input) != XmlNodeOrder.Unknown)
						{
							this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
							return IteratorResult.NeedInputNode;
						}
						this.navNext = XmlQueryRuntime.SyncToNavigator(this.navNext, input);
						this.state = XPathPrecedingMergeIterator.IteratorState.HaveCurrentHaveNext;
					}
					this.PushAncestors();
				}
				if (!this.navStack.IsEmpty)
				{
					while (!this.filter.MoveToFollowing(this.navCurrent, this.navStack.Peek()))
					{
						this.navCurrent.MoveTo(this.navStack.Pop());
						if (this.navStack.IsEmpty)
						{
							goto IL_CF;
						}
					}
					return IteratorResult.HaveCurrentNode;
				}
				IL_CF:
				if (this.state == XPathPrecedingMergeIterator.IteratorState.HaveCurrentNoNext)
				{
					this.state = XPathPrecedingMergeIterator.IteratorState.NeedCandidateCurrent;
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, this.navNext);
				this.state = XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent;
				return IteratorResult.HaveCurrentNode;
			}
			else
			{
				if (input == null)
				{
					return IteratorResult.NoMoreNodes;
				}
				this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
				this.state = XPathPrecedingMergeIterator.IteratorState.HaveCandidateCurrent;
				return IteratorResult.NeedInputNode;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0010406D File Offset: 0x0010226D
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x00104075 File Offset: 0x00102275
		private void PushAncestors()
		{
			this.navStack.Reset();
			do
			{
				this.navStack.Push(this.navCurrent.Clone());
			}
			while (this.navCurrent.MoveToParent());
			this.navStack.Pop();
		}

		// Token: 0x0400226E RID: 8814
		private XmlNavigatorFilter filter;

		// Token: 0x0400226F RID: 8815
		private XPathPrecedingMergeIterator.IteratorState state;

		// Token: 0x04002270 RID: 8816
		private XPathNavigator navCurrent;

		// Token: 0x04002271 RID: 8817
		private XPathNavigator navNext;

		// Token: 0x04002272 RID: 8818
		private XmlNavigatorStack navStack;

		// Token: 0x0200045D RID: 1117
		private enum IteratorState
		{
			// Token: 0x04002274 RID: 8820
			NeedCandidateCurrent,
			// Token: 0x04002275 RID: 8821
			HaveCandidateCurrent,
			// Token: 0x04002276 RID: 8822
			HaveCurrentHaveNext,
			// Token: 0x04002277 RID: 8823
			HaveCurrentNoNext
		}
	}
}
