using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000449 RID: 1097
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DifferenceIterator
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x0010348F File Offset: 0x0010168F
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = DifferenceIterator.IteratorState.InitLeft;
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x001034A0 File Offset: 0x001016A0
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case DifferenceIterator.IteratorState.InitLeft:
				this.navLeft = nestedNavigator;
				this.state = DifferenceIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case DifferenceIterator.IteratorState.NeedLeft:
				this.navLeft = nestedNavigator;
				break;
			case DifferenceIterator.IteratorState.NeedRight:
				this.navRight = nestedNavigator;
				break;
			case DifferenceIterator.IteratorState.NeedLeftAndRight:
				this.navLeft = nestedNavigator;
				this.state = DifferenceIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			case DifferenceIterator.IteratorState.HaveCurrent:
				this.state = DifferenceIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			}
			if (this.navLeft == null)
			{
				return SetIteratorResult.NoMoreNodes;
			}
			if (this.navRight != null)
			{
				int num = this.runtime.ComparePosition(this.navLeft, this.navRight);
				if (num == 0)
				{
					this.state = DifferenceIterator.IteratorState.NeedLeftAndRight;
					return SetIteratorResult.NeedLeftNode;
				}
				if (num > 0)
				{
					this.state = DifferenceIterator.IteratorState.NeedRight;
					return SetIteratorResult.NeedRightNode;
				}
			}
			this.state = DifferenceIterator.IteratorState.HaveCurrent;
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x00103556 File Offset: 0x00101756
		public XPathNavigator Current
		{
			get
			{
				return this.navLeft;
			}
		}

		// Token: 0x0400222A RID: 8746
		private XmlQueryRuntime runtime;

		// Token: 0x0400222B RID: 8747
		private XPathNavigator navLeft;

		// Token: 0x0400222C RID: 8748
		private XPathNavigator navRight;

		// Token: 0x0400222D RID: 8749
		private DifferenceIterator.IteratorState state;

		// Token: 0x0200044A RID: 1098
		private enum IteratorState
		{
			// Token: 0x0400222F RID: 8751
			InitLeft,
			// Token: 0x04002230 RID: 8752
			NeedLeft,
			// Token: 0x04002231 RID: 8753
			NeedRight,
			// Token: 0x04002232 RID: 8754
			NeedLeftAndRight,
			// Token: 0x04002233 RID: 8755
			HaveCurrent
		}
	}
}
