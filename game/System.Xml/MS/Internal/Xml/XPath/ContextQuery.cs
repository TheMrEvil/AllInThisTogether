using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000625 RID: 1573
	internal class ContextQuery : Query
	{
		// Token: 0x0600405C RID: 16476 RVA: 0x00164514 File Offset: 0x00162714
		public ContextQuery()
		{
			this.count = 0;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x00164523 File Offset: 0x00162723
		protected ContextQuery(ContextQuery other) : base(other)
		{
			this.contextNode = other.contextNode;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x00163CE4 File Offset: 0x00161EE4
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x00164538 File Offset: 0x00162738
		public override XPathNavigator Current
		{
			get
			{
				return this.contextNode;
			}
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x00164540 File Offset: 0x00162740
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current;
			this.count = 0;
			return this;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x00164556 File Offset: 0x00162756
		public override XPathNavigator Advance()
		{
			if (this.count == 0)
			{
				this.count = 1;
				return this.contextNode;
			}
			return null;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x0000206B File Offset: 0x0000026B
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			return current;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x0016456F File Offset: 0x0016276F
		public override XPathNodeIterator Clone()
		{
			return new ContextQuery(this);
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x00163D61 File Offset: 0x00161F61
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x0001222F File Offset: 0x0001042F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x0012B969 File Offset: 0x00129B69
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04002E0B RID: 11787
		protected XPathNavigator contextNode;
	}
}
