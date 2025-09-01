using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000627 RID: 1575
	internal class DescendantQuery : DescendantBaseQuery
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x00164624 File Offset: 0x00162824
		internal DescendantQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type, matchSelf, abbrAxis)
		{
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x00164635 File Offset: 0x00162835
		public DescendantQuery(DescendantQuery other) : base(other)
		{
			this._nodeIterator = Query.Clone(other._nodeIterator);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0016464F File Offset: 0x0016284F
		public override void Reset()
		{
			this._nodeIterator = null;
			base.Reset();
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x00164660 File Offset: 0x00162860
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this._nodeIterator == null)
				{
					this.position = 0;
					XPathNavigator xpathNavigator = this.qyInput.Advance();
					if (xpathNavigator == null)
					{
						break;
					}
					if (base.NameTest)
					{
						if (base.TypeTest == XPathNodeType.ProcessingInstruction)
						{
							this._nodeIterator = new IteratorFilter(xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf), base.Name);
						}
						else
						{
							this._nodeIterator = xpathNavigator.SelectDescendants(base.Name, base.Namespace, this.matchSelf);
						}
					}
					else
					{
						this._nodeIterator = xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf);
					}
				}
				if (this._nodeIterator.MoveNext())
				{
					goto Block_4;
				}
				this._nodeIterator = null;
			}
			return null;
			Block_4:
			this.position++;
			this.currentNode = this._nodeIterator.Current;
			return this.currentNode;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x0016473C File Offset: 0x0016293C
		public override XPathNodeIterator Clone()
		{
			return new DescendantQuery(this);
		}

		// Token: 0x04002E0E RID: 11790
		private XPathNodeIterator _nodeIterator;
	}
}
