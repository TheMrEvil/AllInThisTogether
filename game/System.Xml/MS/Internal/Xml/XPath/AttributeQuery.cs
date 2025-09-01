using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000617 RID: 1559
	internal sealed class AttributeQuery : BaseAxisQuery
	{
		// Token: 0x06003FEE RID: 16366 RVA: 0x001635D7 File Offset: 0x001617D7
		public AttributeQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x001635E4 File Offset: 0x001617E4
		private AttributeQuery(AttributeQuery other) : base(other)
		{
			this._onAttribute = other._onAttribute;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x001635F9 File Offset: 0x001617F9
		public override void Reset()
		{
			this._onAttribute = false;
			base.Reset();
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00163608 File Offset: 0x00161808
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this._onAttribute)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this._onAttribute = this.currentNode.MoveToFirstAttribute();
				}
				else
				{
					this._onAttribute = this.currentNode.MoveToNextAttribute();
				}
				if (this._onAttribute && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x001636A0 File Offset: 0x001618A0
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Attribute && this.matches(context))
			{
				XPathNavigator xpathNavigator = context.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return this.qyInput.MatchNode(xpathNavigator);
				}
			}
			return null;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x001636DF File Offset: 0x001618DF
		public override XPathNodeIterator Clone()
		{
			return new AttributeQuery(this);
		}

		// Token: 0x04002DD7 RID: 11735
		private bool _onAttribute;
	}
}
