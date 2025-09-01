using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000653 RID: 1619
	internal sealed class UnionExpr : Query
	{
		// Token: 0x060041AA RID: 16810 RVA: 0x00168460 File Offset: 0x00166660
		public UnionExpr(Query query1, Query query2)
		{
			this.qy1 = query1;
			this.qy2 = query2;
			this._advance1 = true;
			this._advance2 = true;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x00168484 File Offset: 0x00166684
		private UnionExpr(UnionExpr other) : base(other)
		{
			this.qy1 = Query.Clone(other.qy1);
			this.qy2 = Query.Clone(other.qy2);
			this._advance1 = other._advance1;
			this._advance2 = other._advance2;
			this._currentNode = Query.Clone(other._currentNode);
			this._nextNode = Query.Clone(other._nextNode);
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x001684F4 File Offset: 0x001666F4
		public override void Reset()
		{
			this.qy1.Reset();
			this.qy2.Reset();
			this._advance1 = true;
			this._advance2 = true;
			this._nextNode = null;
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x00168521 File Offset: 0x00166721
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qy1.SetXsltContext(xsltContext);
			this.qy2.SetXsltContext(xsltContext);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x0016853B File Offset: 0x0016673B
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qy1.Evaluate(context);
			this.qy2.Evaluate(context);
			this._advance1 = true;
			this._advance2 = true;
			this._nextNode = null;
			base.ResetCount();
			return this;
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00168574 File Offset: 0x00166774
		private XPathNavigator ProcessSamePosition(XPathNavigator result)
		{
			this._currentNode = result;
			this._advance1 = (this._advance2 = true);
			return result;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x00168599 File Offset: 0x00166799
		private XPathNavigator ProcessBeforePosition(XPathNavigator res1, XPathNavigator res2)
		{
			this._nextNode = res2;
			this._advance2 = false;
			this._advance1 = true;
			this._currentNode = res1;
			return res1;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x001685B8 File Offset: 0x001667B8
		private XPathNavigator ProcessAfterPosition(XPathNavigator res1, XPathNavigator res2)
		{
			this._nextNode = res1;
			this._advance1 = false;
			this._advance2 = true;
			this._currentNode = res2;
			return res2;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x001685D8 File Offset: 0x001667D8
		public override XPathNavigator Advance()
		{
			XPathNavigator xpathNavigator;
			if (this._advance1)
			{
				xpathNavigator = this.qy1.Advance();
			}
			else
			{
				xpathNavigator = this._nextNode;
			}
			XPathNavigator xpathNavigator2;
			if (this._advance2)
			{
				xpathNavigator2 = this.qy2.Advance();
			}
			else
			{
				xpathNavigator2 = this._nextNode;
			}
			if (xpathNavigator != null && xpathNavigator2 != null)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(xpathNavigator, xpathNavigator2);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					return this.ProcessBeforePosition(xpathNavigator, xpathNavigator2);
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					return this.ProcessAfterPosition(xpathNavigator, xpathNavigator2);
				}
				return this.ProcessSamePosition(xpathNavigator);
			}
			else
			{
				if (xpathNavigator2 == null)
				{
					this._advance1 = true;
					this._advance2 = false;
					this._currentNode = xpathNavigator;
					this._nextNode = null;
					return xpathNavigator;
				}
				this._advance1 = false;
				this._advance2 = true;
				this._currentNode = xpathNavigator2;
				this._nextNode = null;
				return xpathNavigator2;
			}
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x00168690 File Offset: 0x00166890
		public override XPathNavigator MatchNode(XPathNavigator xsltContext)
		{
			if (xsltContext == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.qy1.MatchNode(xsltContext);
			if (xpathNavigator != null)
			{
				return xpathNavigator;
			}
			return this.qy2.MatchNode(xsltContext);
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x001686C0 File Offset: 0x001668C0
		public override XPathNodeIterator Clone()
		{
			return new UnionExpr(this);
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x001686C8 File Offset: 0x001668C8
		public override XPathNavigator Current
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override int CurrentPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04002E98 RID: 11928
		internal Query qy1;

		// Token: 0x04002E99 RID: 11929
		internal Query qy2;

		// Token: 0x04002E9A RID: 11930
		private bool _advance1;

		// Token: 0x04002E9B RID: 11931
		private bool _advance2;

		// Token: 0x04002E9C RID: 11932
		private XPathNavigator _currentNode;

		// Token: 0x04002E9D RID: 11933
		private XPathNavigator _nextNode;
	}
}
