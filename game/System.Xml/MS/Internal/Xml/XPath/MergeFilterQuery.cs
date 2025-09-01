using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200063B RID: 1595
	internal sealed class MergeFilterQuery : CacheOutputQuery
	{
		// Token: 0x06004106 RID: 16646 RVA: 0x001661CA File Offset: 0x001643CA
		public MergeFilterQuery(Query input, Query child) : base(input)
		{
			this._child = child;
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001661DA File Offset: 0x001643DA
		private MergeFilterQuery(MergeFilterQuery other) : base(other)
		{
			this._child = Query.Clone(other._child);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x001661F4 File Offset: 0x001643F4
		public override void SetXsltContext(XsltContext xsltContext)
		{
			base.SetXsltContext(xsltContext);
			this._child.SetXsltContext(xsltContext);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0016620C File Offset: 0x0016440C
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.Evaluate(nodeIterator);
			while (this.input.Advance() != null)
			{
				this._child.Evaluate(this.input);
				XPathNavigator nav;
				while ((nav = this._child.Advance()) != null)
				{
					Query.Insert(this.outputBuffer, nav);
				}
			}
			return this;
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x00166264 File Offset: 0x00164464
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			XPathNavigator xpathNavigator = this._child.MatchNode(current);
			if (xpathNavigator == null)
			{
				return null;
			}
			xpathNavigator = this.input.MatchNode(xpathNavigator);
			if (xpathNavigator == null)
			{
				return null;
			}
			this.Evaluate(new XPathSingletonIterator(xpathNavigator.Clone(), true));
			for (XPathNavigator xpathNavigator2 = this.Advance(); xpathNavigator2 != null; xpathNavigator2 = this.Advance())
			{
				if (xpathNavigator2.IsSamePosition(current))
				{
					return xpathNavigator;
				}
			}
			return null;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x001662C7 File Offset: 0x001644C7
		public override XPathNodeIterator Clone()
		{
			return new MergeFilterQuery(this);
		}

		// Token: 0x04002E4C RID: 11852
		private Query _child;
	}
}
