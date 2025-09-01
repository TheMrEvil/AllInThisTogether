using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000445 RID: 1093
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct UnionIterator
	{
		// Token: 0x06002B1F RID: 11039 RVA: 0x0010328F File Offset: 0x0010148F
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = UnionIterator.IteratorState.InitLeft;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x001032A0 File Offset: 0x001014A0
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case UnionIterator.IteratorState.InitLeft:
				this.navOther = nestedNavigator;
				this.state = UnionIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case UnionIterator.IteratorState.NeedLeft:
				this.navCurr = nestedNavigator;
				this.state = UnionIterator.IteratorState.LeftIsCurrent;
				break;
			case UnionIterator.IteratorState.NeedRight:
				this.navCurr = nestedNavigator;
				this.state = UnionIterator.IteratorState.RightIsCurrent;
				break;
			case UnionIterator.IteratorState.LeftIsCurrent:
				this.state = UnionIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			case UnionIterator.IteratorState.RightIsCurrent:
				this.state = UnionIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			}
			if (this.navCurr == null)
			{
				if (this.navOther == null)
				{
					return SetIteratorResult.NoMoreNodes;
				}
				this.Swap();
			}
			else if (this.navOther != null)
			{
				int num = this.runtime.ComparePosition(this.navOther, this.navCurr);
				if (num == 0)
				{
					if (this.state == UnionIterator.IteratorState.LeftIsCurrent)
					{
						this.state = UnionIterator.IteratorState.NeedLeft;
						return SetIteratorResult.NeedLeftNode;
					}
					this.state = UnionIterator.IteratorState.NeedRight;
					return SetIteratorResult.NeedRightNode;
				}
				else if (num < 0)
				{
					this.Swap();
				}
			}
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00103375 File Offset: 0x00101575
		public XPathNavigator Current
		{
			get
			{
				return this.navCurr;
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x00103380 File Offset: 0x00101580
		private void Swap()
		{
			XPathNavigator xpathNavigator = this.navCurr;
			this.navCurr = this.navOther;
			this.navOther = xpathNavigator;
			if (this.state == UnionIterator.IteratorState.LeftIsCurrent)
			{
				this.state = UnionIterator.IteratorState.RightIsCurrent;
				return;
			}
			this.state = UnionIterator.IteratorState.LeftIsCurrent;
		}

		// Token: 0x04002216 RID: 8726
		private XmlQueryRuntime runtime;

		// Token: 0x04002217 RID: 8727
		private XPathNavigator navCurr;

		// Token: 0x04002218 RID: 8728
		private XPathNavigator navOther;

		// Token: 0x04002219 RID: 8729
		private UnionIterator.IteratorState state;

		// Token: 0x02000446 RID: 1094
		private enum IteratorState
		{
			// Token: 0x0400221B RID: 8731
			InitLeft,
			// Token: 0x0400221C RID: 8732
			NeedLeft,
			// Token: 0x0400221D RID: 8733
			NeedRight,
			// Token: 0x0400221E RID: 8734
			LeftIsCurrent,
			// Token: 0x0400221F RID: 8735
			RightIsCurrent
		}
	}
}
