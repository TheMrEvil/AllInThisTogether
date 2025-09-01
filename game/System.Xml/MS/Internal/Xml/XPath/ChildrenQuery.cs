using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000620 RID: 1568
	internal class ChildrenQuery : BaseAxisQuery
	{
		// Token: 0x0600403C RID: 16444 RVA: 0x001640A2 File Offset: 0x001622A2
		public ChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x001640BA File Offset: 0x001622BA
		protected ChildrenQuery(ChildrenQuery other) : base(other)
		{
			this._iterator = Query.Clone(other._iterator);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x001640DF File Offset: 0x001622DF
		public override void Reset()
		{
			this._iterator = XPathEmptyIterator.Instance;
			base.Reset();
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x001640F4 File Offset: 0x001622F4
		public override XPathNavigator Advance()
		{
			while (!this._iterator.MoveNext())
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				if (base.NameTest)
				{
					if (base.TypeTest == XPathNodeType.ProcessingInstruction)
					{
						this._iterator = new IteratorFilter(xpathNavigator.SelectChildren(base.TypeTest), base.Name);
					}
					else
					{
						this._iterator = xpathNavigator.SelectChildren(base.Name, base.Namespace);
					}
				}
				else
				{
					this._iterator = xpathNavigator.SelectChildren(base.TypeTest);
				}
				this.position = 0;
			}
			this.position++;
			this.currentNode = this._iterator.Current;
			return this.currentNode;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x001641AC File Offset: 0x001623AC
		public sealed override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context == null || !this.matches(context))
			{
				return null;
			}
			XPathNavigator xpathNavigator = context.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.MoveToParent())
			{
				return this.qyInput.MatchNode(xpathNavigator);
			}
			return null;
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x001641ED File Offset: 0x001623ED
		public override XPathNodeIterator Clone()
		{
			return new ChildrenQuery(this);
		}

		// Token: 0x04002E02 RID: 11778
		private XPathNodeIterator _iterator = XPathEmptyIterator.Instance;
	}
}
