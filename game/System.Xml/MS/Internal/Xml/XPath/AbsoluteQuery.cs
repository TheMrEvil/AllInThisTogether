using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000614 RID: 1556
	internal sealed class AbsoluteQuery : ContextQuery
	{
		// Token: 0x06003FE6 RID: 16358 RVA: 0x00163588 File Offset: 0x00161788
		public AbsoluteQuery()
		{
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x00163590 File Offset: 0x00161790
		private AbsoluteQuery(AbsoluteQuery other) : base(other)
		{
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x00163599 File Offset: 0x00161799
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current.Clone();
			this.contextNode.MoveToRoot();
			this.count = 0;
			return this;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x001635BF File Offset: 0x001617BF
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Root)
			{
				return context;
			}
			return null;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x001635CF File Offset: 0x001617CF
		public override XPathNodeIterator Clone()
		{
			return new AbsoluteQuery(this);
		}
	}
}
