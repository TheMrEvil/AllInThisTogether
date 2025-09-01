using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000646 RID: 1606
	internal sealed class PrecedingQuery : BaseAxisQuery
	{
		// Token: 0x06004147 RID: 16711 RVA: 0x00166AE7 File Offset: 0x00164CE7
		public PrecedingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this._ancestorStk = new ClonableStack<XPathNavigator>();
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00166AFF File Offset: 0x00164CFF
		private PrecedingQuery(PrecedingQuery other) : base(other)
		{
			this._workIterator = Query.Clone(other._workIterator);
			this._ancestorStk = other._ancestorStk.Clone();
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x00166B2A File Offset: 0x00164D2A
		public override void Reset()
		{
			this._workIterator = null;
			this._ancestorStk.Clear();
			base.Reset();
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x00166B44 File Offset: 0x00164D44
		public override XPathNavigator Advance()
		{
			if (this._workIterator == null)
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				do
				{
					xpathNavigator2.MoveTo(xpathNavigator);
				}
				while ((xpathNavigator = this.qyInput.Advance()) != null);
				if (xpathNavigator2.NodeType == XPathNodeType.Attribute || xpathNavigator2.NodeType == XPathNodeType.Namespace)
				{
					xpathNavigator2.MoveToParent();
				}
				do
				{
					this._ancestorStk.Push(xpathNavigator2.Clone());
				}
				while (xpathNavigator2.MoveToParent());
				this._workIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			}
			while (this._workIterator.MoveNext())
			{
				this.currentNode = this._workIterator.Current;
				if (this.currentNode.IsSamePosition(this._ancestorStk.Peek()))
				{
					this._ancestorStk.Pop();
					if (this._ancestorStk.Count == 0)
					{
						this.currentNode = null;
						this._workIterator = null;
						return null;
					}
				}
				else if (this.matches(this.currentNode))
				{
					this.position++;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x00166C54 File Offset: 0x00164E54
		public override XPathNodeIterator Clone()
		{
			return new PrecedingQuery(this);
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x00166C5C File Offset: 0x00164E5C
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x04002E6D RID: 11885
		private XPathNodeIterator _workIterator;

		// Token: 0x04002E6E RID: 11886
		private ClonableStack<XPathNavigator> _ancestorStk;
	}
}
