using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000447 RID: 1095
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct IntersectIterator
	{
		// Token: 0x06002B23 RID: 11043 RVA: 0x001033BF File Offset: 0x001015BF
		public void Create(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
			this.state = IntersectIterator.IteratorState.InitLeft;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x001033D0 File Offset: 0x001015D0
		public SetIteratorResult MoveNext(XPathNavigator nestedNavigator)
		{
			switch (this.state)
			{
			case IntersectIterator.IteratorState.InitLeft:
				this.navLeft = nestedNavigator;
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.InitRightIterator;
			case IntersectIterator.IteratorState.NeedLeft:
				this.navLeft = nestedNavigator;
				break;
			case IntersectIterator.IteratorState.NeedRight:
				this.navRight = nestedNavigator;
				break;
			case IntersectIterator.IteratorState.NeedLeftAndRight:
				this.navLeft = nestedNavigator;
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			case IntersectIterator.IteratorState.HaveCurrent:
				this.state = IntersectIterator.IteratorState.NeedLeftAndRight;
				return SetIteratorResult.NeedLeftNode;
			}
			if (this.navLeft == null || this.navRight == null)
			{
				return SetIteratorResult.NoMoreNodes;
			}
			int num = this.runtime.ComparePosition(this.navLeft, this.navRight);
			if (num < 0)
			{
				this.state = IntersectIterator.IteratorState.NeedLeft;
				return SetIteratorResult.NeedLeftNode;
			}
			if (num > 0)
			{
				this.state = IntersectIterator.IteratorState.NeedRight;
				return SetIteratorResult.NeedRightNode;
			}
			this.state = IntersectIterator.IteratorState.HaveCurrent;
			return SetIteratorResult.HaveCurrentNode;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x00103487 File Offset: 0x00101687
		public XPathNavigator Current
		{
			get
			{
				return this.navLeft;
			}
		}

		// Token: 0x04002220 RID: 8736
		private XmlQueryRuntime runtime;

		// Token: 0x04002221 RID: 8737
		private XPathNavigator navLeft;

		// Token: 0x04002222 RID: 8738
		private XPathNavigator navRight;

		// Token: 0x04002223 RID: 8739
		private IntersectIterator.IteratorState state;

		// Token: 0x02000448 RID: 1096
		private enum IteratorState
		{
			// Token: 0x04002225 RID: 8741
			InitLeft,
			// Token: 0x04002226 RID: 8742
			NeedLeft,
			// Token: 0x04002227 RID: 8743
			NeedRight,
			// Token: 0x04002228 RID: 8744
			NeedLeftAndRight,
			// Token: 0x04002229 RID: 8745
			HaveCurrent
		}
	}
}
